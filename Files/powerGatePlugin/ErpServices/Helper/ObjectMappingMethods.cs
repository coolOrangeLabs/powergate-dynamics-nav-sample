namespace DynamicsNav.Plugin.Helper
{
    public static class ObjectMappingMethods
    {
        public static Material ToPowerGateObject(this SOAP.ItemCard.ItemCard item)
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
                Weight = item.Net_Weight,
                Dimensions = "",
                VendorNumber = item.Vendor_No,
                IsVendorSpecified = !string.IsNullOrEmpty(item.Vendor_No),
                VendorName = !string.IsNullOrEmpty(item.Vendor_No) ? Vendors.GetVendor(item.Vendor_No).Name : "",
                VendorItemNumber = item.Vendor_Item_No,
                Cost = item.Unit_Cost
            };
        }

        public static SOAP.ItemCard.ItemCard ToErpObject(this Material material)
        {
            var item = new SOAP.ItemCard.ItemCard
            {
                No = material.Number,
                Description = material.Description,
                Last_Date_Modified = material.ModifiedDate,
                Base_Unit_of_Measure = material.UnitOfMeasure,
                Type = material.Type == "Inventory" ? SOAP.ItemCard.Type.Inventory : SOAP.ItemCard.Type.Service,
                Blocked = material.IsBlocked,
                Item_Category_Code = material.Category,
                Shelf_No = material.Shelf,
                Net_Weight = material.Weight,
                Vendor_No = material.VendorNumber,
                Vendor_Item_No = material.VendorItemNumber,
                Unit_Cost = material.Cost
            };

            return item;
        }

        public static Vendor ToPowerGateObject(this SOAP.Vendors.Vendors vendor)
        {
            return new Vendor
            {
                Number = vendor.No,
                Name = vendor.Name
            };
        }

        public static UnitOfMeasure ToPowerGateObject(this SOAP.UnitsOfMeasure.UnitsOfMeasure unit)
        {
            return new UnitOfMeasure
            {
                Key = unit.Code,
                Value = unit.Description
            };
        }

        public static Category ToPowerGateObject(this SOAP.ItemCategoryCard.ItemCategoryCard category)
        {
            return new Category
            {
                Key = category.Code,
                Value = category.Description
            };
        }
    }
}