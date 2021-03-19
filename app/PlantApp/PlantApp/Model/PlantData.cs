using System;
using System.Collections.Generic;
using System.Text;

namespace PlantApp.Model
{
    class PlantData : Bindable
    {
        private DateTime date;
        public DateTime Date { get { return date; } set { date = value; OnPropertyChanged(); } }

        private string temp;
        public string Temp { get { return temp; } set { temp = value; OnPropertyChanged(); } }

        private string airHum;
        public string AirHum { get { return airHum; } set { airHum = value; OnPropertyChanged(); } }

        private string soilHum;
        public string SoilHum { get { return soilHum; } set { soilHum = value; OnPropertyChanged(); } }



    }
}
