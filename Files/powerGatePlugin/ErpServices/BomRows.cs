using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using powerGateServer.SDK;

namespace DynamicsNav.Plugin
{
    [DataServiceKey("ParentNumber","ChildNumber","Position")]
    [DataServiceEntity]
    [IgnoreProperties("Id")]
    public class BomRow
    {
        public string ParentNumber { get; set; }
        public string ChildNumber { get; set; }
        public int Position { get; set; }
        public string Type { get; set; }
        public double Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public string Description { get; set; }
        public DateTime ModifiedDate { get; set; }

        public string Id => $"{ParentNumber}+{ChildNumber}+{Position.ToString()}";
    }

    public class BomRows : ServiceMethod<BomRow>
    {
        public override string Name => "BomRows";

        public override IEnumerable<BomRow> Query(IExpression<BomRow> expression)
        {
            throw new NotImplementedException();
        }

        public override void Update(BomRow entity)
        {

        }

        public override void Create(BomRow entity)
        {

        }

        public override void Delete(BomRow entity)
        {

        }
    }
}