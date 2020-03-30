using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using DynamicsNav.Plugin.Helper;
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
            var top = expression.TopCount;
            if (top > 100) top = 100;

            var endpoint = WebService.GetServiceEndpoint<ItemCategoryCard_PortChannel>();
            var client = new ItemCategoryCard_PortClient(endpoint.Binding, endpoint.Address);
            var units = client.ReadMultiple(null, null, top);
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
}