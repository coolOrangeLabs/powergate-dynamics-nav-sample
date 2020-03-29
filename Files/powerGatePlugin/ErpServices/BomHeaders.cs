using System;
using System.Collections.Generic;
using System.Data.Services.Common;
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
        public DateTime ModifiedDate { get; set; }

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
            throw new NotImplementedException();
        }

        public override void Update(BomHeader entity)
        {

        }

        public override void Create(BomHeader entity)
        {

        }

        public override void Delete(BomHeader entity)
        {

        }
    }
}