using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using DynamicsNav.Plugin.SOAP.ItemCategoryCard;
using powerGateServer.SDK;

namespace DynamicsNav.Plugin
{
    [DataServiceKey("Key")]
    [DataServiceEntity]
    public class Category
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class Categories : ServiceMethod<Category>
    {
        public override string Name => "Categories";

        public override IEnumerable<Category> Query(IExpression<Category> expression)
        {
            var results = new List<Category>();

            var endpoint = WebService.GetServiceEndpoint<ItemCategoryCard_PortChannel>();
            var client = new ItemCategoryCard_PortClient(endpoint.Binding, endpoint.Address);
            var units = client.ReadMultiple(null, null, expression.TopCount);
            foreach (var unit in units)
                results.Add(unit.ToPowerGateObject());

            return results;
        }

        public override void Update(Category entity)
        {
            throw new NotSupportedException();
        }

        public override void Create(Category entity)
        {
            throw new NotSupportedException();
        }

        public override void Delete(Category entity)
        {
            throw new NotSupportedException();
        }
    }

    public static partial class ObjectMappingMethods
    {
        public static Category ToPowerGateObject(this ItemCategoryCard category)
        {
            return new Category
            {
                Key = category.Code,
                Value = category.Description
            };
        }
    }
}