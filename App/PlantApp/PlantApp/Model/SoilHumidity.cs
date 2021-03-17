using System;
using System.Collections.Generic;
using System.Text;

namespace PlantApp.Model
{
    class SoilHumidity
    {
        private int soilId;
        private string humidity;
        private DateTime date;

        public int SoilId { get => soilId; set => soilId = value; }
        public string Humidity { get => humidity; set => humidity = value; }
        public DateTime Date { get => date; set => date = value; }
    }
}
