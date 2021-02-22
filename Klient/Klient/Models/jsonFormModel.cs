using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Klient.Models
{
    public class jsonFormModel
    {
        public string serializeJSON(ProductsModel product)
        {
            string jsonString = JsonConvert.SerializeObject(product, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            return jsonString;
        }
    }
}
