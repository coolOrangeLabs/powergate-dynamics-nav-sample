using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicsNav.WebserviceTests
{
    [TestClass]
    public class BasicSOAPTests
    {
        [TestMethod]
        public void TestGetItemCategoryCards()
        {
            var client = new SOAP.ItemCategoryCard.ItemCategoryCard_PortClient();
            var categoryCards = client.ReadMultiple(null, null, 100);

            foreach (var categoryCard in categoryCards)
                Debug.WriteLine(categoryCard.Code + ": " + categoryCard.Description);
        }

        [TestMethod]
        public void TestGetVendor()
        {
            var client = new SOAP.Vendors.Vendors_PortClient();
            var vendor = client.Read("61000");
            Debug.WriteLine(vendor.No + ": " + vendor.Name);
        }

        [TestMethod]
        public void TestGetVendors()
        {
            var client = new SOAP.Vendors.Vendors_PortClient();
            var vendors = client.ReadMultiple(null, null, 100);

            foreach (var vendor in vendors)
                Debug.WriteLine(vendor.No + ": " + vendor.Name);
        }

        [TestMethod]
        public void TestGetItemCard()
        {
            var client = new SOAP.ItemCard.ItemCard_PortClient();
            var item = client.Read("1000");
            Debug.WriteLine(item.No + ": " + item.Description);
        }

        [TestMethod]
        public void TestGetItemCards()
        {
            var client = new SOAP.ItemCard.ItemCard_PortClient();
            var items = client.ReadMultiple(null, null, 100);

            foreach (var item in items)
                Debug.WriteLine(item.No + ": " + item.Description);
        }

        [TestMethod]
        public void TestSearchItemCards()
        {
            var filter = new SOAP.ItemCard.ItemCard_Filter();
            filter.Field = filter.Field = SOAP.ItemCard.ItemCard_Fields.Description;
            filter.Criteria = "test*";

            var client = new SOAP.ItemCard.ItemCard_PortClient();
            var items = client.ReadMultiple(new [] { filter }, null, 100);

            foreach (var item in items)
                Debug.WriteLine(item.No + ": " + item.Description);
        }

        [TestMethod]
        public void TestGetUnitsOfMeasures()
        {
            var client = new SOAP.UnitsOfMeasure.UnitsOfMeasure_PortClient();
            var uoms = client.ReadMultiple(null, null, 200);

            foreach (var uom in uoms)
                Debug.WriteLine(uom.Code + ": " + uom.Description);
        }

        [TestMethod]
        public void TestGetNewNumber()
        {
            var client = new SOAP.NoSeriesLines.NoSeriesLines_PortClient();
            var series = client.Read("ARTIKEL1");

            var result = "";
            var input = series.Last_No_Used;
            if (string.IsNullOrEmpty(input))
                result = series.Starting_No;
            else
            {
                var blocks = new List<string> {string.Empty};
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
                        var number = long.Parse(blocks[i]);
                        result += (number + series.Increment_by_No).ToString().PadLeft(blocks[i].Length, '0');
                    }
                    else
                        result += blocks[i];
                }
            }

            series.Last_No_Used = result;
            //series.Last_Date_Used = System.DateTime.Today;
            client.Update(ref series);

            Debug.WriteLine(result);
        }

        [TestMethod]
        public void TestGetBom()
        {
            var client = new SOAP.BOMs.BOMs_PortClient();
            var item = client.Read("1000");
            Debug.WriteLine(item.No + ": " + item.Description);
            if (item.ProdBOMLine != null)
            {
                foreach (var line in item.ProdBOMLine)
                    Debug.WriteLine( line.Position + " - " + line.No);
            }
        }
    }
}