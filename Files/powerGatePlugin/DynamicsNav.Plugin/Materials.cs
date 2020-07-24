using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using DynamicsNav.Plugin.SOAP.ItemCard;
using DynamicsNav.Plugin.SOAP.NoSeriesLines;
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
        public string SearchDescription { get; set; }

        //dynamicsnav://localhost:7046/DynamicsNAV100/CRONUS%20International%20Ltd./runpage?page=30&personalization=30&bookmark=21%3BGwAAAAJ7BTcAMAAxADAAMA%3D%3D&mode=Edit
        //public string GoToLink { get; set; }
    }

    public class Materials : ServiceMethod<Material>
    {
        public override string Name => "Materials";

        public override IEnumerable<Material> Query(IExpression<Material> expression)
        {
            var results = new List<Material>();

            var endpoint = WebService.GetServiceEndpoint<ItemCard_PortChannel>();
            var client = new ItemCard_PortClient(endpoint.Binding, endpoint.Address);

            var filterArray = new List<ItemCard_Filter>();
            foreach (var w in expression.Where)
            {
                var ciPrefix = "";
                if (new [] {"Number", "Description", "Category", "Shelf", "SearchDescription" }.Contains(w.PropertyName))
                    ciPrefix = "@";

                var fieldEnum = w.PropertyName.ToItemCardFieldEnum();
                if (fieldEnum == null) continue;

                var criteria = w.ToSearchCriteria();
                if (criteria == null) continue;

                filterArray.Add(new ItemCard_Filter
                {
                    Field = fieldEnum.Value, 
                    Criteria = ciPrefix + criteria
                });
            }
            var list = client.ReadMultiple(filterArray.ToArray(), null, expression.TopCount);
            foreach (var item in list)
                results.Add(item.ToPowerGateObject());

            return results;
        }

        public static List<ItemCard> GetItemsByNumbers(string[] numbers)
        {
            var results = new List<ItemCard>();

            var endpoint = WebService.GetServiceEndpoint<ItemCard_PortChannel>();
            var client = new ItemCard_PortClient(endpoint.Binding, endpoint.Address);

            foreach (var number in numbers)
            {
                var item = client.Read(number);
                if (item != null)
                    results.Add(item);
            }

            return results;
        }

        public override void Update(Material entity)
        {
            var endpoint = WebService.GetServiceEndpoint<ItemCard_PortChannel>();
            var client = new ItemCard_PortClient(endpoint.Binding, endpoint.Address);

            var item = client.Read(entity.Number);
            item = entity.ToErpObject(item);
            client.Update(ref item);
        }

        public override void Create(Material entity)
        {
            var endpoint = WebService.GetServiceEndpoint<ItemCard_PortChannel>();
            var client = new ItemCard_PortClient(endpoint.Binding, endpoint.Address);

            if (entity.Number.Equals("*"))
                entity.Number = GetNextNumber();

            var item = entity.ToErpObject(new ItemCard());
            client.Create(ref item);
        }

        public string GetNextNumber()
        {
            var endpoint = WebService.GetServiceEndpoint<NoSeriesLines_PortChannel>();
            var client = new NoSeriesLines_PortClient(endpoint.Binding, endpoint.Address);

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

        public override void Delete(Material entity)
        {
            var endpoint = WebService.GetServiceEndpoint<ItemCard_PortChannel>();
            var client = new ItemCard_PortClient(endpoint.Binding, endpoint.Address);

            var item = client.Read(entity.Number);
            client.Delete(item.Key);
        }
    }

    public static partial class ObjectMappingMethods
    {
        public static string ToSearchCriteria<T>(this IWhereToken<T> whereToken)
        {
            var op = whereToken.Operator;
            var value = whereToken.Value;

            switch (op)
            {
                case OperatorType.Equals: return value.ToString();
                case OperatorType.EndsWith: return "*" + value;
                case OperatorType.StartsWith: return "" + value + "*";
                case OperatorType.Contains: return "*" + value + "*";
                case OperatorType.DoesNotContain: return null;
                case OperatorType.DoesNotStartWith: return null;
                case OperatorType.DoesNotEndsWith: return null;
                case OperatorType.NotEquals: return null;
                case OperatorType.Empty: return null;
                case OperatorType.NotEmpty: return null;
                case OperatorType.GreatherThan: return null;
                case OperatorType.GreatherThanOrEquals: return null;
                case OperatorType.LessThan: return null;
                case OperatorType.LessThanOrEquals: return null;
                case null: return null;
                default: return null;
            }
        }

        public static ItemCard_Fields? ToItemCardFieldEnum(this string propertyName)
        {
            switch (propertyName)
            {
                case "Number": return ItemCard_Fields.No;
                case "Description": return ItemCard_Fields.Description;
                case "ModifiedDate": return ItemCard_Fields.Last_Date_Modified;
                case "UnitOfMeasure": return ItemCard_Fields.Base_Unit_of_Measure;
                case "Type": return ItemCard_Fields.Type;
                case "IsBlocked": return ItemCard_Fields.Blocked;
                case "Category": return ItemCard_Fields.Item_Category_Code;
                case "Shelf": return ItemCard_Fields.Shelf_No;
                case "SearchDescription": return ItemCard_Fields.Search_Description;
                default: return null;
            }
        }

        public static Material ToPowerGateObject(this ItemCard item)
        {
            return new Material
            {
                Number = item.No,
                Description = item.Description,
                ModifiedDate = item.Last_Date_Modified,
                UnitOfMeasure = item.Base_Unit_of_Measure,
                Type = item.Type == SOAP.ItemCard.Type.Inventory ? "Inventory" : "Service",
                IsBlocked = item.Blocked,
                Category = item.Item_Category_Code,
                Shelf = item.Shelf_No,
                SearchDescription = item.Search_Description
            };
        }

        public static ItemCard ToErpObject(this Material material, ItemCard item)
        {
            item.No = material.Number;
            item.Description = material.Description;
            //item.Last_Date_Modified = material.ModifiedDate;
            item.Base_Unit_of_Measure = material.UnitOfMeasure;
            item.TypeSpecified = true;
            item.Type = material.Type == "Inventory" ? SOAP.ItemCard.Type.Inventory : SOAP.ItemCard.Type.Service;
            item.Blocked = material.IsBlocked;
            item.Item_Category_Code = material.Category;
            item.Shelf_No = material.Shelf;
            item.Search_Description = material.SearchDescription;
            return item;
        }
    }
}