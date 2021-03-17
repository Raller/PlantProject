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

            string test;
            var listOfBtn = new List<Plant>();
            Plant plante; 

            IEnumerable<Plant> plantList = null;

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

        public async Task<Plant> GetProductAsync()
        {
            HttpClient client = new HttpClient();
            Plant product = null;
            HttpResponseMessage response = await client.GetAsync("https://plantprojectapi.azurewebsites.net/plant");
            if (response.IsSuccessStatusCode)
            {
                
                product = await response.Content.ReadAsAsync<Plant>();
                Console.WriteLine("Det virker " + product);
            }
            return product;
        }
    }
}
