using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBase
{
    public class Products
    {
        ActualDB mongoDB = new ActualDB();

        [BsonId]
        public ObjectId objectId { get; set; }
        public long ean { get; set; }
        public string name { get; set; }
        public string producer { get; set; }
        public List<string> type { get; set; }
        public float price { get; set; }
        public int vat { get; set; }
        public int quantity { get; set; }

        public Products(string name, long ean, string producer, int quantity, float price, int vat)
        {
            this.name = name;
            this.ean = TakeEAN(ean);
            this.producer = producer;
            this.quantity = quantity;
            this.price = price;
            this.vat = vat;
        }
        public Products(string name, long ean, string producer, List<string> type, int quantity, float price, int vat)
        {
            this.ean = TakeEAN(ean);
            this.name = name;
            this.producer = producer;
            this.type = type;
            this.quantity = quantity;
            this.price = price;
            this.vat = vat;
        }
        public Products(long ean, string name, string producer, List<string> type, int quantity, float price, int vat)
        {
            this.ean = ean;
            this.name = name;
            this.producer = producer;
            this.type = type;
            this.quantity = quantity;
            this.price = price;
            this.vat = vat;
        }

        public Products()
        {

        }

        public long TakeEAN(long EAN)
        {
            var count = EAN.ToString().Length;
            if ((count >= 8) || (count <= 14))
            {
                if (mongoDB.CheckThisEANExists(EAN) == false)
                {
                    return EAN;
                }
                else
                {
                    throw new Exception("Taki numer EAN już istnieje w bazie!/This EAN Number exist in database");
                }

            }
            throw new Exception("Numer EAN jest niepoprawny/EAN number isn't correct");
        }

        public long ReturnEAN()
        {
            return ean;
        }
    }
}
