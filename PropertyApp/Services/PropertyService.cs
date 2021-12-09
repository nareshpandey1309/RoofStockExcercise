using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using PropertyAPI.Services;
using PropertyAPI.DTOs;
using PropertyAPI.DTO;
using PropertyAPI.Extensions;


namespace PropertyAPI.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly HttpClient _client;
        private readonly string _connectionString = @"Data Source=EXDEVVM060\SQLEXPRESS;Initial Catalog=DevTest;Integrated Security=True";


        public PropertyService(HttpClient client)
        {
            _client = client ?? new HttpClient();
            //_client.BaseAddress = new Uri(string.Empty);
        }

        public async Task<IEnumerable<PropertyContent>> GetProperties()
        {
            List<PropertyContent> result = new List<PropertyContent>();

            var urlString = @"https://samplerspubcontent.blob.core.windows.net/public/properties.json";

            var request = new HttpRequestMessage(HttpMethod.Get, urlString);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await _client.SendAsync(request))
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                byte[] jsonBytes = await response.Content.ReadAsByteArrayAsync();
                var jsonDoc = JsonDocument.Parse(jsonBytes);
                var root = jsonDoc.RootElement;
                var myStringList = root.GetProperty("properties");

                for (var i = 0; i < myStringList.GetArrayLength(); i++)
                {
                    var address = myStringList[i].GetProperty("address").ToObject<Address>();

                    var sampleContent = new PropertyContent();
                    sampleContent.Id = Convert.ToInt32(myStringList[i].GetProperty("id").ToString());
                    sampleContent.Address = address;

                    var financialData = myStringList[i].GetProperty("financial");
                    var physicalData = myStringList[i].GetProperty("physical");
                    ;

                    if (financialData.ValueKind.ToString() != "Null")
                    {
                        decimal listPriceValue = 0;
                        decimal monthlyRentValue = 0;

                        sampleContent.FinancialData = new Financial();

                        if (financialData.GetProperty("listPrice").ValueKind.ToString() != "Null")
                            decimal.TryParse(financialData.GetProperty("listPrice").ToString(), out listPriceValue);

                        if (financialData.GetProperty("monthlyRent").ValueKind.ToString() != "Null")
                            decimal.TryParse(financialData.GetProperty("monthlyRent").ToString(), out monthlyRentValue);

                        sampleContent.FinancialData.ListPrice = listPriceValue;
                        sampleContent.FinancialData.MonthlyRent = monthlyRentValue;
                    }

                    if (physicalData.ValueKind.ToString() != "Null")
                    {
                        sampleContent.PhysicalData = new Physical();

                        if (physicalData.GetProperty("yearBuilt").ValueKind.ToString() != "Null")
                            sampleContent.PhysicalData.YearBuilt = int.Parse(physicalData.GetProperty("yearBuilt").ToString());
                    }

                    result.Add(sampleContent);
                }

                return result;
            }
        }

        public async Task<Response> SaveRecord(PropertyContent newRecord)
        {
            int noOfRowsAffected = 0;
            var recordInsertQuery =
                @"If Not Exists(select * from Properties where Property_Id=@Property_Id)
                Begin
                INSERT INTO Properties(Property_Id
	                                        ,Address1
	                                        ,Address2
	                                        ,City
	                                        ,Country
	                                        ,County
	                                        ,District
	                                        ,State
	                                        ,Zip
	                                        ,ZipPlus4
	                                        ,YearBuilt
	                                        ,ListPrice
	                                        ,MonthlyRent
	                                        ,GrossYield )
                                    VALUES(@Property_Id
	                                        ,@Address1
	                                        ,@Address2
	                                        ,@City
	                                        ,@Country
	                                        ,@County
	                                        ,@District
	                                        ,@State
	                                        ,@Zip
	                                        ,@ZipPlus4
	                                        ,@YearBuilt
	                                        ,@ListPrice
	                                        ,@MonthlyRent
	                                        ,@GrossYield )
                End";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                noOfRowsAffected = await connection.ExecuteAsync(recordInsertQuery, new
                {
                    Property_Id = newRecord.Id,
                    Address1 = newRecord.Address.address1,
                    Address2 = newRecord.Address.address2,
                    City = newRecord.Address.city,
                    Country = newRecord.Address.country,
                    County = newRecord.Address.county,
                    District = newRecord.Address.district,
                    State = newRecord.Address.state,
                    Zip = newRecord.Address.zip,
                    ZipPlus4 = newRecord.Address.zipPlus4,
                    YearBuilt = newRecord.PhysicalData.YearBuilt,
                    ListPrice = newRecord.FinancialData.ListPrice,
                    MonthlyRent = newRecord.FinancialData.MonthlyRent,
                    GrossYield = newRecord.GrossYield
                });
            }

            if (noOfRowsAffected > 0)
                return new Response() { Id = newRecord.Id.ToString(), IsSuccess = true, Comments = "Property Record saved successfully." };
            else
                return new Response() { Id = newRecord.Id.ToString(), IsSuccess = false, Comments = "Property record already exist in db." };

        }
    }
}
