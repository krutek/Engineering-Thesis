using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DataBase;

namespace Serwer.Models
{
    public class JSONhelper
    {

        public string serializeToJSON(List<Products> toJson)
        {
            string jsonString = JsonConvert.SerializeObject(toJson, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            return jsonString;
        }
        public string serializeSoldToJSON(List<SoldProductModel> toJson)
        {
            string jsonString = JsonConvert.SerializeObject(toJson, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            return jsonString;
        }
        public string serializeToJSONfromListOfString(List<string> toJson)
        {
            string jsonString = JsonConvert.SerializeObject(toJson, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            });
            return jsonString;
        }


    }
}
