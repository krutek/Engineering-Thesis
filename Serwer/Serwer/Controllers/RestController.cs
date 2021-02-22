using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Newtonsoft.Json;
using Serwer.Models;
using DataBase;


namespace Serwer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RestController : ControllerBase
    {
        ActualDB mongoDB = new ActualDB();
        JSONhelper jsonHelper = new JSONhelper();
        SoldProducts sp = new SoldProducts();

        [Route("product/InsertOneProductJSON")]
        [HttpPost]
        public void InsertOneProductJSON(Products product)
        {
            mongoDB.InsertProducts(product);
        }

        [Route("product/InsertOneProductWithTypeJSON")]
        [HttpPost]
        public void InsertOneProductWithType(Products product)
        {
            mongoDB.InsertProducts(product);
        }

        [Route("product/SoldProduct")]
        [HttpPost]
        public void soldProduct(Products product, int quantitySold)
        {
            mongoDB.SoldOneProduct(product, quantitySold);
        }

        [Route("product/removeProductFromDatabaseByEan")]
        [HttpPost]
        public void removeProductFromDatabaseByEan(long ean)
        {
            mongoDB.removeProductFromDatabaseByEan(ean);
        }

        [Route("sold/InsertProducts")]
        [HttpPost]
        public void soldOne(Products product)
        {
            sp.InsertProducts(product);
        }
        
        [Route("product/ModifyPriceProduct")]
        [HttpPost]
        public void ModifyPriceProduct(Products oldProduct, float price)
        {
            mongoDB.ModifyPriceProduct(oldProduct, price);
        }

        [Route("product/ModifyNameProduct")]
        [HttpPost]
        public void ModifyNameProduct(Products oldProduct, string name)
        {
            mongoDB.ModifyNameProduct(oldProduct, name);
        }

        [Route("product/ModifyProducerProduct")]
        [HttpPost]
        public void ModifyProducerProduct(Products oldProduct, string producer)
        {
            mongoDB.ModifyProducerProduct(oldProduct, producer);
        }

        [Route("product/ModifyVatProduct")]
        [HttpPost]
        public void ModifyVatProduct(Products oldProduct, int vat)
        {
            mongoDB.ModifyVatProduct(oldProduct, vat);
        }

        [Route("product/ModifyAddTypeProduct")]
        [HttpPost]
        public void ModifyAddTypeProduct(Products oldProduct, string type)
        {
            mongoDB.ModifyAddTypeProduct(oldProduct, type);
        }

        [Route("product/ModifyAddTypeProduct")]
        [HttpPost]
        public void ModifyRemoveTypeProduct(Products oldProduct, string type)
        {
            mongoDB.ModifyRemoveTypeProduct(oldProduct, type);
        }

        [Route("product/ReturnSoldAllTransaction")]
        [HttpGet]
        public int ReturnSoldAllTransaction()
        {
            return sp.ReturnSoldAllTransaction();
        }

        [Route("product/ReturnSoldCountAll")]
        [HttpGet]
        public int ReturnSoldCountAll()
        {
            return sp.ReturnSoldCountAll();
        }

        [Route("returnSumOfPricesToToday")]
        [HttpGet]
        public float returnSumOfPricesToToday()
        {
            return sp.returnSumOfPricesToToday();
        }

        [Route("FindWithOutType")]
        [HttpGet]
        public string FindWithOutType()
        {
            return jsonHelper.serializeToJSON(mongoDB.FindWithoutType());
        }

        [Route("FindByType")]
        [HttpGet]
        public string FindByType(List<string> type)
        {
            return jsonHelper.serializeToJSON(mongoDB.FindByTypes(type));
        }
        [Route("FindByOneType")]
        [HttpGet]
        public string FindByType(string type)
        {
            return jsonHelper.serializeToJSON(mongoDB.FindByType(type));
        }

        [Route("GetAllProducts")]
        [HttpGet]
        public List<Products> GetAllProducts()
        {
            return mongoDB.ReturnListOfAllProducts();
        }

        [Route("sold/GetAllSoldProducts")]
        [HttpGet]
        public string GetAllSoldProducts()
        {
            return jsonHelper.serializeSoldToJSON(sp.ReturnListOfAllProducts());
        }
        
        [Route("ReturnProductsWithPriceEqualOrBiggerThan")]
        [HttpGet]
        public string ReturnProductsWithPriceEqualOrBiggerThan(float price)
        {
            return jsonHelper.serializeToJSON(mongoDB.FindWithPriceEqualOrBiggerThan(price));
        }

        [Route("ReturnProductsWithPriceEqualOrLessThan")]
        [HttpGet]
        public string ReturnProductsWithPriceEqualOrLessThan(float price)
        {
            return jsonHelper.serializeToJSON(mongoDB.FindWithPriceEqualOrLessThan(price));
        }

        [Route("ReturnProductsWithPriceBiggerThan")]
        [HttpGet]
        public string ReturnProductsWithPriceBiggerThan(float price)
        {
            return jsonHelper.serializeToJSON(mongoDB.FindWithPriceBiggerThan(price));
        }

        [Route("ReturnProductsWithPriceLessThan")]
        [HttpGet]
        public string ReturnProductsWithPriceLessThan(float price)
        {
            return jsonHelper.serializeToJSON(mongoDB.FindWithPriceLessThan(price));
        }

        [Route("FindByProducer")]
        [HttpGet]
        public string FindByProducer(string producer)
        {
            return jsonHelper.serializeToJSON(mongoDB.FindByProducer(producer));
        }

        [Route("FindByEAN")]
        [HttpGet]
        public string FindByEAN(long ean)
        {
            return jsonHelper.serializeToJSON(mongoDB.FindByEAN(ean));
        }

        [Route("FindByName")]
        [HttpGet]
        public string FindByName(string name)
        {
            return jsonHelper.serializeToJSON(mongoDB.FindByName(name));
        }

        [Route("ReturnAllTypes")]
        [HttpGet]
        public string ReturnAllTypes()
        {

            return jsonHelper.serializeToJSONfromListOfString(mongoDB.ReturnAllTypes());
        }

        [Route("sold/returnSumOfPricesInMonth")]
        [HttpGet]
        public float returnSumOfPricesInMonth(DateTime dataTime)
        {
            return sp.returnSumOfPrices(dataTime);
        }

        [Route("sold/returnSumOfPricesNow")]
        [HttpGet]
        public float returnSumOfPricesNow()
        {
            return sp.returnSumOfPrices(DateTime.Now);
        }

        [Route("sold/returnSumOfPricesInMonthBetween")]
        [HttpGet]
        public double returnSumOfPricesInMonthBetween(DateTime dataTimeFirst, DateTime dataTimeLast)
        {  
            return Convert.ToDouble(sp.returnSumOfPricesBetween(dataTimeFirst, dataTimeLast));
        }
    }
}
