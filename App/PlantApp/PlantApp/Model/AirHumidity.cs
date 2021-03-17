using System;
using System.Collections.Generic;
using System.Text;

namespace PlantApp.Model
{
    class AirHumidity
    {
        private int humId;
        private string humidity;
        private DateTime date;

        public int HumId { get => humId; set => humId = value; }
        public string Humidity { get => humidity; set => humidity = value; }
        public DateTime Date { get => date; set => date = value; }
    }
}
