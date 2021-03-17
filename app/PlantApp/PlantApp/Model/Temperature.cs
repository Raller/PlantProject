using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PlantApp.Model
{
    class Temperature
    {
        [JsonProperty("_id")]
        public string TempId { get; set; }

        [JsonProperty("temperature")]
        public string temp { get; set; }

        [JsonProperty("plantId")]
        public string plantId { get; set; }

        [JsonProperty("date")]
        public DateTime date  { get; set; }

     
    }
}
