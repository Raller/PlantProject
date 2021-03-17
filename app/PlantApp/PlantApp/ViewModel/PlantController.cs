using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PlantApp.Model;

namespace PlantApp.ViewModel
{
    class PlantController
    {

        private List<Plant> plantsList = new List<Plant>();

        public List<Plant> GenerateCustomerListAsync()
        {

            var listOfBtn = new List<Plant>();
            
           

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://plantprojectapi.azurewebsites.net/");
            //client.DefaultRequestHeaders.Add("appkey", "myapp_key");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response1 = client.GetAsync("plant").Result;
            Console.WriteLine("Besked fra server " + response1.StatusCode);
            if (response1.IsSuccessStatusCode)
            {
                var plants = response1.Content.ReadAsAsync<IEnumerable<Plant>>().Result;
                foreach (var item in plants)
                {
                    Console.WriteLine(item.name);
                    listOfBtn.Add(item);
                }
            }
            else
            {
                //Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }


            return listOfBtn;
        }

        public List<AirHumidity> GetAirHumidity()
        {

            var listOfAirHum = new List<AirHumidity>();


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://plantprojectapi.azurewebsites.net/");
            //client.DefaultRequestHeaders.Add("appkey", "myapp_key");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response1 = client.GetAsync("airhumidity").Result;
            Console.WriteLine("Besked fra server " + response1.StatusCode);
            if (response1.IsSuccessStatusCode)
            {
                var airHum = response1.Content.ReadAsAsync<IEnumerable<AirHumidity>>().Result;
                foreach (var item in airHum)
                {
                    listOfAirHum.Add(item);
                    Console.WriteLine(" this is huuuummm id:  " + item.HumId);
                }
            }
            else
            {
                Console.WriteLine("Error Code" + response1.StatusCode + " : Message - " + response1.ReasonPhrase);
            }


            return listOfAirHum;
        }

        public List<Temperature> GetAirTemperature()
        {

            var listOfAirTemp = new List<Temperature>();


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://plantprojectapi.azurewebsites.net/");
            //client.DefaultRequestHeaders.Add("appkey", "myapp_key");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response1 = client.GetAsync("temperature").Result;
            Console.WriteLine("Besked fra server " + response1.StatusCode);
            if (response1.IsSuccessStatusCode)
            {
                var airTemp = response1.Content.ReadAsAsync<IEnumerable<Temperature>>().Result;
                foreach (var item in airTemp)
                {
                    listOfAirTemp.Add(item);
                }
            }
            else
            {
                Console.WriteLine("Error Code" + response1.StatusCode + " : Message - " + response1.ReasonPhrase);
            }


            return listOfAirTemp;
        }

        public List<SoilHumidity> GetSoilHumidity()
        {

            var listOfSoil = new List<SoilHumidity>();


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://plantprojectapi.azurewebsites.net/");
            //client.DefaultRequestHeaders.Add("appkey", "myapp_key");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response1 = client.GetAsync("soilhumidity").Result;
            Console.WriteLine("Besked fra server " + response1.StatusCode);
            if (response1.IsSuccessStatusCode)
            {
                var soilHum = response1.Content.ReadAsAsync<IEnumerable<SoilHumidity>>().Result;
                foreach (var item in soilHum)
                {
                    listOfSoil.Add(item);
                }
            }
            else
            {
                Console.WriteLine("Error Code" + response1.StatusCode + " : Message - " + response1.ReasonPhrase);
            }


            return listOfSoil;
        }
    }
}
