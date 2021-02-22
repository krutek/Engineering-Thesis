using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Klient.Models
{
    public static class ApiConnectModel
    {
        public static HttpClient  apiClient { get; set; }
        public static jsonFormModel jsonHelper = new jsonFormModel();
        public static void InitializeClient()
        {
            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri("http://localhost:52775/");//("http://52.137.8.127/");
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public static async void insertProducts(ProductsModel product)
        {
           await apiClient.PostAsync("api/Rest/product/InsertOneProductJSON", new StringContent(jsonHelper.serializeJSON(product),
           System.Text.Encoding.UTF8, "application/json"));
        }
        public static async void SoldProduct(ProductsModel products, int quantity)
        {
            await apiClient.PostAsync("api/Rest/product/SoldProduct?quantitySold=" + quantity, new StringContent(jsonHelper.serializeJSON(products),
            System.Text.Encoding.UTF8, "application/json"));
        }
        public static async void DeleteProduct( long ean)
        {
            await apiClient.PostAsync("api/Rest/product/removeProductFromDatabaseByEan?ean=" + ean, new StringContent("Test",
            System.Text.Encoding.UTF8, "application/json"));
        }
        public static async Task<int> ReturnSoldCountAll()
        {
            HttpResponseMessage response = apiClient.GetAsync("api/Rest/product/ReturnSoldCountAll").Result;
            string responseString = await response.Content.ReadAsStringAsync();
            int toReturn =  int.Parse(responseString,System.Globalization.CultureInfo.InvariantCulture);
            return toReturn;
        }
        public static async Task<int> ReturnSoldAllTransaction()
        {
            HttpResponseMessage response = apiClient.GetAsync("api/Rest/product/ReturnSoldAllTransaction").Result;
            string responseString = await response.Content.ReadAsStringAsync();
            int toReturn = int.Parse(responseString, System.Globalization.CultureInfo.InvariantCulture);
            return toReturn;
        }
        public static async Task<List<ProductsModel>> findWithOutType()
        {
            List<ProductsModel> lpm = new List<ProductsModel>();
            HttpResponseMessage response = apiClient.GetAsync("api/Rest/FindWithOutType").Result;
            string responseString = await response.Content.ReadAsStringAsync();         
            string deserializedString = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
            lpm = JsonConvert.DeserializeObject<List<ProductsModel>>(deserializedString);
            return lpm;
        }
        public static async Task<List<ProductsModel>> FindByOneType(string type)
        {
            List<ProductsModel> lpm = new List<ProductsModel>();
            HttpResponseMessage response = apiClient.GetAsync("api/Rest/FindByOneType?type=" + type).Result;
            string responseString = await response.Content.ReadAsStringAsync();
            string deserializedString = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
            lpm = JsonConvert.DeserializeObject<List<ProductsModel>>(deserializedString);
            return lpm;
        }
        public static async Task<List<ProductsModel>> getAllProducts()
        {
            List<ProductsModel> lpm = new List<ProductsModel>();
            HttpResponseMessage response =  apiClient.GetAsync("api/Rest/GetAllProducts").Result;
            string responseString = await response.Content.ReadAsStringAsync();
            lpm = JsonConvert.DeserializeObject<List<ProductsModel>>(responseString);
            return lpm;
        }
        public static async Task<List<string>> returnAllTypes()
        {
            List<string> lpm = new List<string>();
            HttpResponseMessage response = await apiClient.GetAsync("api/Rest/ReturnAllTypes");
            string responseString = await response.Content.ReadAsStringAsync();
            string deserializedString = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
            lpm = JsonConvert.DeserializeObject<List<string>>(deserializedString);
            return lpm;
        }
        
        public static async Task<string> returnSumOfPricesNow()
        {
            List<ProductsModel> lpm = new List<ProductsModel>();
            HttpResponseMessage response = apiClient.GetAsync("api/Rest/sold/returnSumOfPricesNow").Result;
            string responseString = await response.Content.ReadAsStringAsync();
            return responseString;

        }
        public static async Task<List<ProductsModel>> GetAllSoldProducts()
        {
            List<ProductsModel> lpm = new List<ProductsModel>();
            HttpResponseMessage response = apiClient.GetAsync("api/Rest/sold/GetAllSoldProducts").Result;
            string responseString = await response.Content.ReadAsStringAsync();
            string deserializedString = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
            lpm = JsonConvert.DeserializeObject<List<ProductsModel>>(deserializedString);
            return lpm;
        }

        //public static double returnSumOfPricesInMonthBetween(DateTime dateFirst, DateTime dateLast)
        //{
        //    HttpResponseMessage response;
        //    string responseString;
        //    List<ProductsModel> lpm = new List<ProductsModel>();
        //    response = apiClient.GetAsync("api/Rest/sold/returnSumOfPricesInMonthBetwee?dataTimeFirst ="+dateFirst+"&dataTimeLast="+dateLast).Result;
        //    responseString = response.Content.ReadAsStringAsync().Result; //api/Rest/sold/returnSumOfPricesInMonthBetween?dataTimeFirst=2021-01-08&dataTimeLast=2021-01-09
        //    return Convert.ToDouble(responseString);
        //    //http://localhost:52775/api/Rest/sold/returnSumOfPricesInMonth?dataTime=2013-07-29T21:58:39
        //}

        //public static async Task<string> returnSumOfPricesToToday()
        //{
        //    HttpResponseMessage response = (await apiClient.GetAsync("api/Rest/returnSumOfPricesToToday"));
        //    string responseString = response.Content.ReadAsStringAsync().Result;
        //    return responseString;
        //}
        //public static List<ProductsModel> returnProductsWithPriceEqualOrBiggerThan(float price)
        //{
        //    HttpResponseMessage response;
        //    string responseString;
        //    List<ProductsModel> lpm = new List<ProductsModel>();
        //    response = apiClient.GetAsync("api/Rest/ReturnProductsWithPriceEqualOrBiggerThan?price=" + price ).Result;
        //    responseString = response.Content.ReadAsStringAsync().Result;
        //    string deserializedString = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
        //    lpm = JsonConvert.DeserializeObject<List<ProductsModel>>(deserializedString);
        //    return lpm;
        //}
        //public static List<ProductsModel> returnProductsWithPriceEqualOrLessThan(float price)
        //{
        //    HttpResponseMessage response;
        //    string responseString;
        //    List<ProductsModel> lpm = new List<ProductsModel>();
        //    response = apiClient.GetAsync("api/Rest/returnProductsWithPriceEqualOrLessThan?price=" + price).Result;
        //    responseString = response.Content.ReadAsStringAsync().Result;
        //    string deserializedString = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
        //    lpm = JsonConvert.DeserializeObject<List<ProductsModel>>(deserializedString);
        //    return lpm;
        //}
        //public static List<ProductsModel> findByProducer(string producer)
        //{
        //    HttpResponseMessage response;
        //    string responseString;
        //    List<ProductsModel> lpm = new List<ProductsModel>();
        //    response = apiClient.GetAsync("api/Rest/FindByProducer?producer=" + producer).Result;
        //    responseString = response.Content.ReadAsStringAsync().Result;
        //    string deserializedString = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
        //    lpm = JsonConvert.DeserializeObject<List<ProductsModel>>(deserializedString);
        //    return lpm;
        //}
        //public static List<ProductsModel> findByEAN(long ean)
        //{
        //    HttpResponseMessage response;
        //    string responseString;
        //    List<ProductsModel> lpm = new List<ProductsModel>();
        //    response = apiClient.GetAsync("api/Rest/FindByEAN?ean=" + ean).Result;
        //    responseString = response.Content.ReadAsStringAsync().Result;
        //    string deserializedString = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
        //    lpm = JsonConvert.DeserializeObject<List<ProductsModel>>(deserializedString);
        //    return lpm;
        //}
        //public static string returnSumOfPricesInMonth(DateTime date)
        //{
        //    HttpResponseMessage response;
        //    string responseString;
        //    List<ProductsModel> lpm = new List<ProductsModel>();
        //    response = apiClient.GetAsync("api/Rest/sold/returnSumOfPricesInMonth?dataTime=" + date).Result;
        //    responseString = response.Content.ReadAsStringAsync().Result;
        //    return responseString;
        //    //http://localhost:52775/api/Rest/sold/returnSumOfPricesInMonth?dataTime=2013-07-29T21:58:39
        //}
        //public static List<ProductsModel> findByName(string name)
        //{
        //    HttpResponseMessage response;
        //    string responseString;
        //    List<ProductsModel> lpm = new List<ProductsModel>();
        //    ProductsModel pm = new ProductsModel();
        //    response = apiClient.GetAsync("api/Rest/FindByName?name=" + name).Result;
        //    responseString = response.Content.ReadAsStringAsync().Result;
        //    string deserializedString = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responseString);
        //    lpm = JsonConvert.DeserializeObject<List<ProductsModel>>(deserializedString);
        //    return lpm;
        //}
    }
}
