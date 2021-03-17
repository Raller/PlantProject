using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PlantApp.Model
{
    public class Plant
    {
        public Plant()
        {
         
        }
        [JsonProperty("_id")]
        public string id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("imageUrl")]
        public string imageUrl { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("longitude")]
        public string longitude { get; set; }
        [JsonProperty("latitude")]
        public string latitude { get; set; }

        [JsonProperty("date")]
        public string date { get; set; }

        [JsonProperty("__v")]
        public int v { get; set; }

        public string allInfo => $"{name}"+ " - " + $"{type}";

        public void test()
        {
            Console.WriteLine("Woooow ");
        }

    }

 
}
