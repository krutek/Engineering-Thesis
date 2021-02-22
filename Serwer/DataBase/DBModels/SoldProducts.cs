using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBase
{
    public class SoldProducts
    {
        private IMongoDatabase db;
        public SoldProducts()
        {
            var client = new MongoClient();
            db = client.GetDatabase("ServerDataBase");
        }
        public List<SoldProductModel> ReturnListOfAllProducts()
        {
            var collection = db.GetCollection<SoldProductModel>("Sold");
            var ToReturn = collection.Find(x => true).ToList();
            return ToReturn;
        }
        public void InsertProducts<SoldProductModel>(SoldProductModel record)
        {   
            var collection = db.GetCollection<SoldProductModel>("Sold");
            collection.InsertOne(record);
        }
        public float returnSumOfPices()
        {
            var collection = db.GetCollection<SoldProductModel>("Sold");
            float sum = 0;
            foreach (var item in collection.AsQueryable())
            {
                sum += (float)item.price;
            }
            return sum;
        }
        public float returnSumOfPrices(DateTime Date)
        {
            var collection = db.GetCollection<SoldProductModel>("Sold");
            var collectionInDate = collection.AsQueryable().ToList().Where(x => x.dateOfSold.Month == Date.Month);
            float sum = 0;

            foreach (var item in collectionInDate)
            {
                sum += (float)item.price;
            }
            return sum;
        }
        public float returnSumOfPricesToToday()
        {
            var collection = db.GetCollection<SoldProductModel>("Sold");
            var collectionInDate = collection.AsQueryable().ToList().Where(x => x.dateOfSold.Month <= DateTime.Now.Month);
            float sum = 0;

            foreach (var item in collectionInDate)
            {
                sum += (float)item.price;
            }
            return sum;
        }
        public float returnSumOfPricesBetween(DateTime DateFirst, DateTime DateLast)
        {
            var collection = db.GetCollection<SoldProductModel>("Sold");
            var collectionInDate = collection.AsQueryable().ToList().Where(x => x.dateOfSold.Day >= DateFirst.Day && x.dateOfSold.Day <= DateLast.Day);
            float sum = 0;

            foreach (var item in collectionInDate)
            {
                sum += (float)item.price;
            }
            return sum;
        }
        public int ReturnSoldCountAll()
        {
            var collection = db.GetCollection<SoldProductModel>("Sold");
            var collectionInDate = collection.AsQueryable().ToList();
            var sum = 0;

            foreach (var item in collectionInDate)
            {
                sum += item.quantity;
            }
            return sum;

        }
        public int ReturnSoldAllTransaction()
        {
            var collection = db.GetCollection<SoldProductModel>("Sold");
            var collectionInDate = collection.AsQueryable().ToList();
            var sum = 0;

            foreach (var item in collectionInDate)
            {
                sum++;
            }
            return sum;

        }
    }
}
