using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using System.Web;
using DynamicsNav.Plugin.SOAP.BOMs;
using DynamicsNav.Plugin.SOAP.ItemCard;
using powerGateServer.SDK;

namespace DynamicsNav.Plugin
{
    [DataServiceKey("Number")]
    [DataServiceEntity]
    public class BomHeader
    {
        public string Number { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
        public string UnitOfMeasure { get; set; }
        public DateTime ModifiedDate { get; set; }

        public string Link { get; set; }

        public List<BomRow> BomRows { get; set;  }

        public BomHeader()
        {
            BomRows = new List<BomRow>();
        }
    }

    public class BomHeaders : ServiceMethod<BomHeader>
    {
        public override string Name => "BomHeaders";

        public override IEnumerable<BomHeader> Query(IExpression<BomHeader> expression)
        {
            if (expression.Where.Any(b => b.PropertyName.Equals("Number")))
            {
                var number = expression.Where.FirstOrDefault(w => w.PropertyName.Equals("Number"));
                if (number != null && number.Value != number && number.Value.ToString() != "")
                    return GetBomHeaders(number.Value.ToString());
            }

            return new List<BomHeader>();
        }

        public static List<BomHeader> GetBomHeaders(string number)
        {
            var bomHeaders = new List<BomHeader>();

            var endpoint = WebService.GetServiceEndpoint<BOMs_PortChannel>();
            var client = new BOMs_PortClient(endpoint.Binding, endpoint.Address);

            var bom = client.Read(number);
            if (bom != null)
                bomHeaders.Add(bom.ToPowerGateObject());

            return bomHeaders;
        }

        public override void Update(BomHeader entity)
        {
            var endpoint = WebService.GetServiceEndpoint<BOMs_PortChannel>();
            var client = new BOMs_PortClient(endpoint.Binding, endpoint.Address);

            var items = Materials.GetItemsByNumbers(entity.BomRows.Select(l => l.ChildNumber).ToArray());
            var bom = client.Read(entity.Number);
            bom = entity.ToErpObject(bom, items);
            client.Update(ref bom);
        }

        public override void Create(BomHeader entity)
        {
            var endpoint = WebService.GetServiceEndpoint<BOMs_PortChannel>();
            var client = new BOMs_PortClient(endpoint.Binding, endpoint.Address);

            var items = Materials.GetItemsByNumbers(entity.BomRows.Select(l => l.ChildNumber).ToArray());
            var bom = entity.ToErpObject(new BOMs(), items);
            client.Create(ref bom);
        }

        public override void Delete(BomHeader entity)
        {
            var endpoint = WebService.GetServiceEndpoint<BOMs_PortChannel>();
            var client = new BOMs_PortClient(endpoint.Binding, endpoint.Address);

            var bom = client.Read(entity.Number);
            client.Delete(bom.Key);
        }
    }

    public static partial class ObjectMappingMethods
    {
        public static BomHeader ToPowerGateObject(this BOMs bom)
        {
            return new BomHeader
            {
                Number = bom.No,
                Description = bom.Description,
                State = bom.Status.ToString(),
                UnitOfMeasure = bom.Unit_of_Measure_Code,
                ModifiedDate = bom.Last_Date_Modified,
                Link = GetBomLink(bom.Key),
                BomRows = bom.ProdBOMLine.Select(line => line.ToPowerGateObject(bom.No)).ToList()
            };
        }
        private static string GetBomLink(string key)
        {
            //https://docs.microsoft.com/en-us/dynamics-nav/creating-and-running-hyperlinks
            var endpoint = WebService.GetServiceEndpoint<BOMs_PortChannel>();
            var server = endpoint.Address.Uri.Host;
            var port = 7046;
            var instance = endpoint.Address.Uri.Segments[1].TrimEnd('/');
            var company = Uri.EscapeUriString(WebService.Company);

            var sections = key.Split(new[] { ';' }, 2);
            var valueLength = int.Parse(sections[0]);
            var value = sections[1].Substring(0, valueLength);
            var bytes = Convert.FromBase64String(value);
            var length = bytes.Length + valueLength.ToString().Length + HttpUtility.UrlEncode(";").Length;
            var bookmark =  HttpUtility.UrlEncode(length + ";" + value);

            return $"dynamicsnav://{server}:{port}/{instance}/{company}/runpage?page=99000786&personalization=99000786&bookmark={bookmark}";
        }

        public static BOMs ToErpObject(this BomHeader bomHeader, BOMs bom, IEnumerable<ItemCard> items)
        {
            bom.No = bomHeader.Number;
            bom.Description = bomHeader.Description;
            bom.Unit_of_Measure_Code = bomHeader.UnitOfMeasure;
            bom.Last_Date_Modified = bomHeader.ModifiedDate;

            var lines = new List<Production_BOM_Lines>();
            foreach (var bomRow in bomHeader.BomRows)
            {
                var item = items.First(i => i.No.Equals(bomRow.ChildNumber));
                var line = bom.ProdBOMLine?.FirstOrDefault(p => p.No.Equals(bomRow.ChildNumber) && Convert.ToInt32(p.Position).Equals(bomRow.Position));
                lines.Add(line == null ? bomRow.ToErpObject(new Production_BOM_Lines(), item) : bomRow.ToErpObject(line, item));
            }
            bom.ProdBOMLine = lines.ToArray();

            return bom;
        }
    }
}