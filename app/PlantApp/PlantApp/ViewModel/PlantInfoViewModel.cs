using Newtonsoft.Json;
using PlantApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace PlantApp.ViewModel
{
    class PlantInfoViewModel : Bindable
    {
        public Plant Plant { get; set; }
        private Temperature temperature;
        public Temperature Temperature { get { return temperature; } set { temperature = value; OnPropertyChanged(); } }
        private AirHumidity airHumidity;
        public AirHumidity AirHumidity { get { return airHumidity; } set { airHumidity = value; OnPropertyChanged(); } }
        private SoilHumidity soilHumidity;
        public SoilHumidity SoilHumidity { get { return soilHumidity; } set { soilHumidity = value; OnPropertyChanged(); } }
        public PlantInfoViewModel(Plant selectedPlant)
        {
            this.Plant = selectedPlant;
            
            GetAirTemperature();
            GetAirHumidity();
            GetSoilHumidity();
        }

        public async void GetAirHumidity()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://plantprojectapi.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("airhumidity/latest/" + Plant.Id);
            Console.WriteLine("Besked fra server " + response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                AirHumidity =  await response.Content.ReadAsAsync<AirHumidity>();
                //AirHumidity = JsonConvert.DeserializeObject<AirHumidity>(humidity);
            }
            else
            {
                Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        public async void GetAirTemperature()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://plantprojectapi.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("temperature/latest/" + Plant.Id);
            Console.WriteLine("Besked fra server " + response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                Temperature = await response.Content.ReadAsAsync<Temperature>();
                //Temperature = JsonConvert.DeserializeObject<Temperature>(temp);
            }
            else
            {
                Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        public async void GetSoilHumidity()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://plantprojectapi.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("soilhumidity/latest/" + Plant.Id);
            Console.WriteLine("Besked fra server " + response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                SoilHumidity = await response.Content.ReadAsAsync<SoilHumidity>();
                //SoilHumidity = JsonConvert.DeserializeObject<SoilHumidity>(humidity);
            }
            else
            {
                Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }
    }
}
