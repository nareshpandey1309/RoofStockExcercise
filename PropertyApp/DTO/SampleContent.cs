namespace PropertyAPI.DTOs
{
    public class PropertyContent
    {
        public int Id { get; set; }
        public Address Address { get; set; }
        public Physical PhysicalData { get; set; }
        public Financial FinancialData { get; set; }
        public decimal GrossYield
        {
            get
            {
                decimal listPriceValue = 0;
                decimal monthlyRentValue = 0;
                decimal grossYeild = 0;

                if (FinancialData != null && FinancialData.ListPrice > 0 && FinancialData.ListPrice > 0)
                {
                    listPriceValue = FinancialData.ListPrice;
                    monthlyRentValue = FinancialData.MonthlyRent;
                    grossYeild = (listPriceValue > 0 && monthlyRentValue > 0) ? (monthlyRentValue * 12) / listPriceValue : 0;
                }

                return grossYeild;
            }
        }
    }

    public class Address
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string county { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string zipPlus4 { get; set; }
    }

    public class Physical
    {
        public int YearBuilt { get; set; }
    }
    public class Financial
    {
        public decimal ListPrice { get; set; }
        public decimal MonthlyRent { get; set; }
    }

}