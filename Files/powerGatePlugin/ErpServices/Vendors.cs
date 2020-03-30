﻿using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using DynamicsNav.Plugin.Helper;
using DynamicsNav.Plugin.SOAP.Vendors;
using powerGateServer.SDK;

namespace DynamicsNav.Plugin
{
    [DataServiceKey("Number")]
    [DataServiceEntity]
    public class Vendor
    {
        public string Number { get; set; }
        public string Name { get; set; }
    }

    public class Vendors : ServiceMethod<Vendor>
    {
        public override string Name => "Vendors";

        public override IEnumerable<Vendor> Query(IExpression<Vendor> expression)
        {
            var results = new List<Vendor>();
            var top = expression.TopCount;
            if (top > 100) top = 100;

            var endpoint = WebService.GetServiceEndpoint<Vendors_PortChannel>();
            var client = new Vendors_PortClient(endpoint.Binding, endpoint.Address);
            var vendors = client.ReadMultiple(null, null, top);
            foreach (var vendor in vendors)
                results.Add(vendor.ToPowerGateObject());

            return results;
        }

        public static Vendor GetVendor(string number)
        {
            var endpoint = WebService.GetServiceEndpoint<Vendors_PortChannel>();
            var client = new Vendors_PortClient(endpoint.Binding, endpoint.Address);
            var vendor = client.Read(number);
            return vendor.ToPowerGateObject();
        }

        public override void Update(Vendor entity)
        {
            throw new NotSupportedException();
        }

        public override void Create(Vendor entity)
        {
            throw new NotSupportedException();
        }

        public override void Delete(Vendor entity)
        {
            throw new NotSupportedException();
        }
    }
}