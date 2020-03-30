using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using DynamicsNav.Plugin.Helper;
using DynamicsNav.Plugin.SOAP.ItemCard;
using powerGateServer.SDK;

namespace DynamicsNav.Plugin
{
    [DataServiceKey("Number")]
    [DataServiceEntity]
    public class Material
    {
        public string Number { get; set; }
        public string Description { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Type { get; set; }
        public bool IsBlocked { get; set; }
        public string Category { get; set; }
        public string Shelf { get; set; }
        public decimal Weight { get; set; }
        public string Dimensions { get; set; }
        public bool IsVendorSpecified { get; set; }
        public string VendorNumber { get; set; }
        public string VendorName { get; set; }
        public string VendorItemNumber { get; set; }
        public decimal Cost { get; set; }
    }

    public class Materials : ServiceMethod<Material>
    {
        public override string Name => "Materials";

        public string GetNextNumber()
        {
            var client = new SOAP.NoSeriesLines.NoSeriesLines_PortClient();
            var series = client.Read(WebService.NoSeriesCode);

            var number = "";
            var input = series.Last_No_Used;
            if (string.IsNullOrEmpty(input))
                number = series.Starting_No;
            else
            {
                var blocks = new List<string> { string.Empty };
                for (var i = 0; i < input.Length; i++)
                {
                    blocks[blocks.Count - 1] += input[i];
                    if (i + 1 < input.Length && char.IsNumber(input[i]) != char.IsNumber(input[i + 1]))
                        blocks.Add(string.Empty);
                }

                long index = 0;
                for (var i = 0; i < blocks.Count; i++)
                {
                    if (long.TryParse(blocks[i], out _))
                        index = i;
                }

                for (var i = 0; i < blocks.Count; i++)
                {
                    if (index == i)
                    {
                        var num = long.Parse(blocks[i]);
                        number += (num + series.Increment_by_No).ToString().PadLeft(blocks[i].Length, '0');
                    }
                    else
                        number += blocks[i];
                }
            }

            series.Last_No_Used = number;
            //series.Last_Date_Used = System.DateTime.Today;
            client.Update(ref series);
            
            return number;
        }

        public override IEnumerable<Material> Query(IExpression<Material> expression)
        {
            var results = new List<Material>();
            var filterArray = new List<ItemCard_Filter>();
            foreach (var w in expression.Where)
            {
                var fieldEnum = GetFieldEnum(w.PropertyName);
                if (fieldEnum == null) continue;

                filterArray.Add(new ItemCard_Filter
                {
                    Field = fieldEnum.Value, 
                    Criteria = GetCriteriaByOperator(w.Operator, w.Value)
                });
            }

            var top = expression.TopCount;
            if (top > 100) top = 100;

            var endpoint = WebService.GetServiceEndpoint<ItemCard_PortChannel>();
            var client = new ItemCard_PortClient(endpoint.Binding, endpoint.Address);
            var list = client.ReadMultiple(filterArray.ToArray(), null, top);
            foreach (var item in list)
                results.Add(item.ToPowerGateObject());

            return results;
        }

        private ItemCard_Fields? GetFieldEnum(string propertyName)
        {
            switch (propertyName)
            {
                case "Description": return ItemCard_Fields.Description;
                case "Number": return ItemCard_Fields.No;
                default: return null;
            }
        }

        private string GetCriteriaByOperator(OperatorType? op, object value)
        {
            switch (op)
            {
                case OperatorType.Equals: return value.ToString();
                case OperatorType.EndsWith: return "*" + value;
                case OperatorType.StartsWith: return value + "*"; 
                case OperatorType.Contains: return "*" + value + "*";
                default: return "*" + value + "*";
            }
        }

        public override void Update(Material entity)
        {
            var item = entity.ToErpObject();
            var endpoint = WebService.GetServiceEndpoint<ItemCard_PortChannel>();
            var client = new ItemCard_PortClient(endpoint.Binding, endpoint.Address);
            client.Update(ref item);
        }

        public override void Create(Material entity)
        {
            if (entity.Number.Equals("*"))
                entity.Number = GetNextNumber();

            var item = entity.ToErpObject();
            var endpoint = WebService.GetServiceEndpoint<ItemCard_PortChannel>();
            var client = new ItemCard_PortClient(endpoint.Binding, endpoint.Address);
            client.Create(ref item);
        }

        public override void Delete(Material entity)
        {
            var item = entity.ToErpObject();
            var endpoint = WebService.GetServiceEndpoint<ItemCard_PortChannel>();
            var client = new ItemCard_PortClient(endpoint.Binding, endpoint.Address);
            client.Delete(item.Key);
        }
    }
}