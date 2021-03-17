using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PlantApp.Model
{
    public class Temperature
    {
        [JsonProperty("_id")]
        public string TempId { get; set; }

        [JsonProperty("temperature")]
        public string Temp { get; set; }

        [JsonProperty("plantId")]
        public string PlantId { get; set; }

        [JsonProperty("date")]
        public DateTime Date  { get; set; }

     
    }
}
