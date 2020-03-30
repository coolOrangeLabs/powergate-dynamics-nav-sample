using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using DynamicsNav.Plugin.Helper;
using DynamicsNav.Plugin.SOAP.UnitsOfMeasure;
using powerGateServer.SDK;

namespace DynamicsNav.Plugin
{
    [DataServiceKey("Key")]
    [DataServiceEntity]
    public class UnitOfMeasure
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class UnitsOfMeasure : ServiceMethod<UnitOfMeasure>
    {
        public override string Name => "UnitsOfMeasure";

        public override IEnumerable<UnitOfMeasure> Query(IExpression<UnitOfMeasure> expression)
        {
            var results = new List<UnitOfMeasure>();
            var top = expression.TopCount;
            if (top > 100) top = 100;

            var endpoint = WebService.GetServiceEndpoint<UnitsOfMeasure_PortChannel>();
            var client = new UnitsOfMeasure_PortClient(endpoint.Binding, endpoint.Address);
            var units = client.ReadMultiple(null, null, top);
            foreach (var unit in units)
                results.Add(unit.ToPowerGateObject());

            return results;
        }

        public override void Update(UnitOfMeasure entity)
        {
            throw new NotSupportedException();
        }

        public override void Create(UnitOfMeasure entity)
        {
            throw new NotSupportedException();
        }

        public override void Delete(UnitOfMeasure entity)
        {
            throw new NotSupportedException();
        }
    }
}