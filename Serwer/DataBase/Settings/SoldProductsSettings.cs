using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase
{ 
    public class SoldProductsSettings : ISoldProductsSettings
    {
        public string BooksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ISoldProductsSettings
    {
        string BooksCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
