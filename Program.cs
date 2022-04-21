using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
namespace CountryCode
{
    class Program
    {
        


            static void Main(string[] args)
            {
                Console.WriteLine(getPhoneNumbers("Afghanistan", "656445445"));
                Console.WriteLine(getPhoneNumbers("Puerto Rico", "564593986"));
                Console.WriteLine(getPhoneNumbers("Oceania", "987574876"));
            }

            public class CountryDataResponse
            {
              
                public Country[] Data { get; set; }

            }

            public class Country
            {
              
                public string[] CallingCodes { get; set; }
            }

            public const string countryURL = "https://jsonmock.hackerrank.com/api/countries?name=";

            public static string getPhoneNumbers(string country, string phoneNumber)
            {
                string result = "-1";

                try
                {
                    string response = HttpClientService.GetResult($"{countryURL}{country}");
                    var countryResponse = JObject.Parse(response).ToObject<CountryDataResponse>();

                    if (countryResponse.Data != null && countryResponse.Data.Any())
                    {
                        result = $"+{countryResponse.Data.FirstOrDefault().CallingCodes.LastOrDefault()} {phoneNumber}";
                    }
                }

                catch (Exception ex)
                {
                    CountryLogger.CreateCountryLogs(ex);
                }
                return result;
            }

            public class HttpClientService
            {
                static readonly HttpClient http_Client_service = new HttpClient();
                public static string GetResult(string APIurl)
                {
                    try
                    {
                        HttpResponseMessage response = http_Client_service.GetAsync(APIurl).GetAwaiter().GetResult();
                        return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            public class CountryLogger
            {
                public static void CreateCountryLogs(Exception ex)
                {
                    Console.WriteLine($"Exception : {ex.Message}");
                }
            }
        }
    }

