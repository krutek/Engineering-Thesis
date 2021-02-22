using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBase
    {
    public class ActualDB
    {
        private IMongoDatabase db;
        SoldProducts sp = new SoldProducts();
        public ActualDB()
        {
            var client = new MongoClient();
            db = client.GetDatabase("ServerDataBase");
        }
        public List<Products> ReturnListOfAllProducts()
        {
            var collection = db.GetCollection<Products>("Products");
            var ToReturn = collection.Find(x => true).ToList();
            return ToReturn;
        }
        public bool CheckThisEANExists(long ean)
        {
            List<Products> products = new List<Products>();
            var collection = db.GetCollection<Products>("Products");
            foreach (var item in collection.AsQueryable().ToList())
            {
                if (item.ReturnEAN() == ean)
                {
                    return true;
                }
            }
            return false;
        }
        public void removeProductFromDatabase(ObjectId objectId)
        {
            var collection = db.GetCollection<Products>("Products");
            collection.DeleteOne(x => x.objectId == objectId);
        }
        public void removeProductFromDatabaseByEan(long ean)
        {
            var collection = db.GetCollection<Products>("Products");
            collection.DeleteOne(x => x.ean == ean);
        }
        public void InsertProducts<Products>(Products record)
        {
            var collection = db.GetCollection<Products>("Products");
            collection.InsertOne(record);
        }
        public List<Products> FindByEAN(long ean)
        {
            List<Products> products = new List<Products>();
            var collection = db.GetCollection<Products>("Products");
            foreach (var item in collection.AsQueryable())
            {
                if (item.ReturnEAN() == ean)
                {
                    products.Add(item);
                }
            }
            return products;
        }
        public List<Products> FindByProducer(string producer)
        {
            var collection = db.GetCollection<Products>("Products");
            var products = collection.AsQueryable().Where(x => x.producer == producer).ToList();
            return products;
        }
        public List<Products> FindByName(string name)
        {
            var collection = db.GetCollection<Products>("Products");
            var products = collection.AsQueryable().Where(x => x.name == name).ToList();
            return products;
        }
        public List<string> ReturnAllTypes()
        {
            List<Products> ToReturn = new List<Products>();
            List<string> types = new List<string>();
            var collection = db.GetCollection<Products>("Products");
            var products = collection.AsQueryable();
            foreach (var item in products.ToList())
            {
                foreach (var inside in item.type)
                {
                    types.Add(inside);
                }
            }
            return types;
        }
        public List<Products> FindByTypes(List<string> type)
        {
            var collection = db.GetCollection<Products>("Products");
            var products = collection.AsQueryable();
            List<Products> finded = new List<Products>();
            foreach (var item in products.ToList())
            {
                foreach (var inside in item.type)
                {
                    foreach (var three in type)
                    {
                        if (inside == three)
                        {
                            finded.Add(item);
                        }
                    }
                }
            }
            finded.Distinct();
            return finded;
        }
        public List<Products> FindByType(string type)
        {
            var collection = db.GetCollection<Products>("Products");
            var products = collection.AsQueryable();
            List<Products> finded = new List<Products>();
            foreach (var item in products.ToList())
            {
                foreach (var inside in item.type)
                {
                    if (inside == type)
                    {
                        finded.Add(item);
                    }
                }
            }
            finded.Distinct();
            return finded;
        }
        public List<Products> FindWithoutType()
        {
            var collection = db.GetCollection<Products>("Products");
            var products = collection.AsQueryable().Where(x => x.type == null).ToList();
            return products;
        }
        public List<Products> FindWithPriceEqualOrBiggerThan(float price)
        {
            List<Products> products = new List<Products>();
            var collection = db.GetCollection<Products>("Products").AsQueryable();
            var FindedProducts = collection.Where(x => x.price >= price);
            foreach (var item in FindedProducts)
            {
                products.Add(item);
            }
            return products;
        }
        public List<Products> FindWithPriceEqualOrLessThan(float price)
        {
            List<Products> products = new List<Products>();
            var collection = db.GetCollection<Products>("Products").AsQueryable();
            var FindedProducts = collection.Where(x => x.price <= price);
            foreach (var item in FindedProducts)
            {
                products.Add(item);
            }
            return products;
        }
        public List<Products> FindWithPriceBiggerThan(float price)
        {
            List<Products> products = new List<Products>();
            var collection = db.GetCollection<Products>("Products").AsQueryable();
            var FindedProducts = collection.Where(x => x.price > price);
            foreach (var item in FindedProducts)
            {
                products.Add(item);
            }
            return products;
        }
        public List<Products> FindWithPriceLessThan(float price)
        {
            List<Products> products = new List<Products>();
            var collection = db.GetCollection<Products>("Products").AsQueryable();
            var FindedProducts = collection.Where(x => x.price < price);
            foreach (var item in FindedProducts)
            {
                products.Add(item);
            }
            return products;
        }
        public void SoldOneProduct(Products product, int quantity)
        {
            if (quantity == 0)
            {
                quantity = 1;
            }
            var collection = db.GetCollection<Products>("Products").AsQueryable();
            var FindedProducts = collection.Where(x => x.name == product.name && x.producer == product.producer).FirstOrDefault();
            removeProductFromDatabase(FindedProducts.objectId);
            FindedProducts.quantity = FindedProducts.quantity - quantity;
            InsertProducts(FindedProducts);
            FindedProducts.quantity = quantity;
            SoldProductModel p = new SoldProductModel(DateTime.Now, FindedProducts.name, FindedProducts.ean, FindedProducts.producer, FindedProducts.type, FindedProducts.quantity, FindedProducts.price, FindedProducts.vat, true);
            sp.InsertProducts(p);
        }
        public void ModifyPriceProduct(Products oldProduct, float price)
        {
            var collection = db.GetCollection<Products>("Products").AsQueryable();
            var FindedProducts = collection.Where(x => x.name == oldProduct.name && x.producer == oldProduct.producer).FirstOrDefault();
            removeProductFromDatabase(FindedProducts.objectId);
            FindedProducts.price = price;
            InsertProducts(FindedProducts);
        }
        public void ModifyNameProduct(Products oldProduct, string name)
        {
            var collection = db.GetCollection<Products>("Products").AsQueryable();
            var FindedProducts = collection.Where(x => x.name == oldProduct.name && x.producer == oldProduct.producer).FirstOrDefault();
            removeProductFromDatabase(FindedProducts.objectId);
            FindedProducts.name = name;
            InsertProducts(FindedProducts);
        }
        public void ModifyProducerProduct(Products oldProduct, string producer)
        {
            var collection = db.GetCollection<Products>("Products").AsQueryable();
            var FindedProducts = collection.Where(x => x.name == oldProduct.name && x.producer == oldProduct.producer).FirstOrDefault();
            removeProductFromDatabase(FindedProducts.objectId);
            FindedProducts.producer = producer;
            InsertProducts(FindedProducts);
        }
        public void ModifyVatProduct(Products oldProduct, int vat)
        {
            var collection = db.GetCollection<Products>("Products").AsQueryable();
            var FindedProducts = collection.Where(x => x.name == oldProduct.name && x.producer == oldProduct.producer).FirstOrDefault();
            removeProductFromDatabase(FindedProducts.objectId);
            FindedProducts.vat = vat;
            InsertProducts(FindedProducts);
        }
        public void ModifyAddTypeProduct(Products oldProduct, string type)
        {
            var collection = db.GetCollection<Products>("Products").AsQueryable();
            var FindedProducts = collection.Where(x => x.name == oldProduct.name && x.producer == oldProduct.producer).FirstOrDefault();
            removeProductFromDatabase(FindedProducts.objectId);
            FindedProducts.type.Add(type);
            InsertProducts(FindedProducts);
        }
        public void ModifyRemoveTypeProduct(Products oldProduct, string type)
        {
            var collection = db.GetCollection<Products>("Products").AsQueryable();
            var FindedProducts = collection.Where(x => x.name == oldProduct.name && x.producer == oldProduct.producer).FirstOrDefault();
            removeProductFromDatabase(FindedProducts.objectId);
            FindedProducts.type.Remove(type);
            InsertProducts(FindedProducts);
        }
    }
}
