using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataBase
{
    public class SoldProductModel : Products
    {
        [BsonElement]
        public DateTime dateOfSold { get; set; }
        public SoldProductModel(DateTime date, string name, long ean, string producer, List<string> type, int quantity, float price, int vat, bool sold)
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
        public SoldProductModel(DateTime date, string name, long ean, string producer, int quantity, float price, int vat, bool sold)
        {
            this.ean = ean;
            this.name = name;
            this.producer = producer;
            this.quantity = quantity;
            this.price = price;
            this.vat = vat;
            this.dateOfSold = date;
        }
    }
}
