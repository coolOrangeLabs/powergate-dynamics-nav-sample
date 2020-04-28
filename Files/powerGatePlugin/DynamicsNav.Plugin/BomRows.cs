using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using DynamicsNav.Plugin.SOAP.BOMs;
using DynamicsNav.Plugin.SOAP.ItemCard;
using powerGateServer.SDK;

namespace DynamicsNav.Plugin
{
    [DataServiceKey("ParentNumber", "ChildNumber", "Position")]
    [DataServiceEntity]
    [IgnoreProperties("Id")]
    public class BomRow
    {
        public string ParentNumber { get; set; }
        public string ChildNumber { get; set; }
        public int Position { get; set; }
        public string Type { get; set; }
        public decimal Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Description { get; set; }
        public DateTime ModifiedDate { get; set; }

        public string Id => $"{ParentNumber}+{ChildNumber}+{Position}";
    }

    public class BomRows : ServiceMethod<BomRow>
    {
        public override string Name => "BomRows";

        public override IEnumerable<BomRow> Query(IExpression<BomRow> expression)
        {
            if (expression.Where.Any(b => b.PropertyName.Equals("ParentNumber")))
            {
                var number = expression.Where.FirstOrDefault(w => w.PropertyName.Equals("ParentNumber"));
                if (number != null && number.Value != number && number.Value.ToString() != "")
                    return BomHeaders.GetBomHeaders(number.Value.ToString()).SelectMany(b => b.BomRows);
            }

            return new List<BomRow>();
        }

        public override void Update(BomRow entity)
        {
            var endpoint = WebService.GetServiceEndpoint<BOMs_PortChannel>();
            var client = new BOMs_PortClient(endpoint.Binding, endpoint.Address);

            var bom = client.Read(entity.ParentNumber);
            var line = bom?.ProdBOMLine?.FirstOrDefault(p => p.No.Equals(entity.ChildNumber) && Convert.ToInt32(p.Position).Equals(entity.Position));
            if (line != null)
            {
                var item = Materials.GetItemsByNumbers(new[] {line.No}).First();
                var bomList = bom.ProdBOMLine.ToList();
                bomList.Remove(line);
                bomList.Add(entity.ToErpObject(line, item));
                bom.ProdBOMLine = bomList.ToArray();
                client.Update(ref bom);
            }
        }

        public override void Create(BomRow entity)
        {
            var endpoint = WebService.GetServiceEndpoint<BOMs_PortChannel>();
            var client = new BOMs_PortClient(endpoint.Binding, endpoint.Address);

            var bom = client.Read(entity.ParentNumber);
            if (bom == null)
                return;

            var item = Materials.GetItemsByNumbers(new[] { entity.ChildNumber }).First();
            var bomList = bom.ProdBOMLine.ToList();
            bomList.Add(entity.ToErpObject(new Production_BOM_Lines(), item));
            bom.ProdBOMLine = bomList.ToArray();
            client.Update(ref bom);
        }

        public override void Delete(BomRow entity)
        {
            var endpoint = WebService.GetServiceEndpoint<BOMs_PortChannel>();
            var client = new BOMs_PortClient(endpoint.Binding, endpoint.Address);

            var bom = client.Read(entity.ParentNumber);

            var line = bom?.ProdBOMLine?.FirstOrDefault(p =>
                p.No.Equals(entity.ChildNumber) && Convert.ToInt32(p.Position).Equals(entity.Position));
            if (line != null)
                client.Delete_ProdBOMLine(line.Key);
        }
    }

    public static partial class ObjectMappingMethods
    {
        public static BomRow ToPowerGateObject(this Production_BOM_Lines line, string parentNumber)
        {
            return new BomRow
            {
                ParentNumber = parentNumber,
                ChildNumber = line.No,
                Position = Convert.ToInt32(line.Position),
                Type = line.Type == SOAP.BOMs.Type.Item ? "Part" : "Assembly",
                Quantity = line.Quantity_per,
                UnitOfMeasure = line.Unit_of_Measure_Code,
                Description = line.Description,
                ModifiedDate = DateTime.MinValue
            };
        }

        public static Production_BOM_Lines ToErpObject(this BomRow bomRow, Production_BOM_Lines line, ItemCard item)
        {
            line.No = bomRow.ChildNumber;
            line.Position = bomRow.Position.ToString();
            //line.Type = bomRow.Type == "Part" ? SOAP.BOMs.Type.Item : SOAP.BOMs.Type.Production_BOM;
            line.Type = SOAP.BOMs.Type.Item;
            line.TypeSpecified = true;
            line.Quantity_per = Convert.ToDecimal(bomRow.Quantity);
            line.Quantity_perSpecified = true;
            line.Unit_of_Measure_Code = item.Base_Unit_of_Measure;
            line.Description = item.Description;
            return line;
        }
    }
}