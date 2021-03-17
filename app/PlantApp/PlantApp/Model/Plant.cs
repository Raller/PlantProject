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
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("longitude")]
        public string Longitude { get; set; }
        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        public string allInfo => $"{Name}"+ " - " + $"{Type}";

        public void test()
        {
            Console.WriteLine("Woooow ");
        }

    }

 
}
