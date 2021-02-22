using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Klient.Models
{
    public class ProductsModel
    {
        [BsonId]
        ObjectId objectId { get; set; }
        public long ean { get; set; }
        public string name { get; set; }
        public string producer { get; set; }
        public List<string> type { get; set; }
        public float price { get; set; }
        public int vat { get; set; }
        public int quantity { get; set; }

        [BsonElement]
        public DateTime dateOfSold { get; set; }
        public ProductsModel()
        {

        }
        public ProductsModel(DateTime date, string name, long ean, string producer, List<string> type, int quantity, float price, int vat, bool sold)
        {
            this.ean = ean;
            this.name = name;
            this.producer = producer;
            this.type = type;
            this.quantity = quantity;
            this.price = price;
            this.vat = vat;
            this.dateOfSold = date;
        }
        public ProductsModel(DateTime date, string name, long ean, string producer, int quantity, float price, int vat, bool sold)
        {
            this.ean = ean;
            this.name = name;
            this.producer = producer;
            this.quantity = quantity;
            this.price = price;
            this.vat = vat;
            this.dateOfSold = date;
        }
        public ProductsModel( ObjectId objectId ,string name, long ean, string producer, List<string> type, int quantity, float price, int vat)
        {
            this.ean = ean;
            this.name = name;
            this.producer = producer;
            this.type = type;
            this.quantity = quantity;
            this.price = price;
            this.vat = vat;
        }
        public ProductsModel(ObjectId objectId ,string name, long ean, string producer, int quantity, float price, int vat)
        {
            this.name = name;
            this.ean = ean;
            this.producer = producer;
            this.quantity = quantity;
            this.price = price;
            this.vat = vat;
        }
        public ProductsModel(string name, long ean, string producer, int quantity, float price, int vat)
        {
            this.name = name;
            this.ean = ean;
            this.producer = producer;
            this.quantity = quantity;
            this.price = price;
            this.vat = vat;
        }
        public ProductsModel(string name, long ean, string producer, List<string> type, int quantity, float price, int vat)
        {
            this.ean = ean;
            this.name = name;
            this.producer = producer;
            this.type = type;
            this.quantity = quantity;
            this.price = price;
            this.vat = vat;
        }
    }
}
