using System;
using System.Collections.Generic;
using System.Data.Services.Common;
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
        public double Weight { get; set; }
        public string Dimensions { get; set; }
        public bool IsVendorSpecified {
            get => !string.IsNullOrEmpty(VendorNumber);
            set => _ = value;
        }
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
            throw new NotImplementedException();
        }

        public override void Update(Material entity)
        {

        }

        public override void Create(Material entity)
        {
            if (entity.Number.Equals("*"))
                entity.Number = GetNextNumber();


        }

        public override void Delete(Material entity)
        {
            throw new NotSupportedException();
        }
    }
}