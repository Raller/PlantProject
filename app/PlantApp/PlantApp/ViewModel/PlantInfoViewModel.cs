using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using PlantApp.Model;

namespace PlantApp.ViewModel
{
    class PlantInfoViewModel
    {
        public Plant Plant { get; set; }
        public Temperature Temperature { get; set; }
        public AirHumidity AirHumidity { get; set; }
        public SoilHumidity SoilHumidity { get; set; }
        public PlantInfoViewModel(Plant selectedPlant)
        {
            this.Plant = selectedPlant;
            PlantController ctr = new PlantController();
            List<Temperature> tempList = ctr.GetAirTemperature(this.Plant);
            if (tempList.Any())
            {
                this.Temperature = tempList.Last();
            }

            List<AirHumidity> airList = ctr.GetAirHumidity(this.Plant);
            if (airList.Any())
            {
                this.AirHumidity = airList.Last();
            }
        }



        

    }
}
