using PlantApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Forms;

namespace PlantApp.ViewModel
{
    class PlantHistoryViewModel : Bindable
    {

        private Plant plant;
        public Plant Plant { get { return plant; } set { plant = value; OnPropertyChanged(); } }

        private ObservableCollection<PlantData> plantDataList = new ObservableCollection<PlantData>();
        public ObservableCollection<PlantData> PlantDataList { get { return plantDataList; } set { plantDataList = value; OnPropertyChanged(); } }

        private ObservableCollection<Temperature> tempList = new ObservableCollection<Temperature>();
        private ObservableCollection<AirHumidity> airHumList = new ObservableCollection<AirHumidity>();
        private ObservableCollection<SoilHumidity> soilHumList = new ObservableCollection<SoilHumidity>();


        private int datacount = 0;

        public PlantHistoryViewModel()
        {

        }
        public PlantHistoryViewModel(Plant selectedPlant)
        {
            Plant = selectedPlant;
            GetData();
        }

        private async void GetData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://plantprojectapi.azurewebsites.net/");
            //client.DefaultRequestHeaders.Add("appkey", "myapp_key");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var tempResponse = await client.GetAsync("temperature/plantid/" + Plant.Id);

            if (tempResponse.IsSuccessStatusCode)
            {
                var temps = await tempResponse.Content.ReadAsAsync<IEnumerable<Temperature>>();
                foreach (var item in temps)
                {
                    datacount++;
                    tempList.Add(item);
                    Console.WriteLine("temp: " + item.Temp);
                }
            }
            else
            {
                Console.WriteLine("Error Code" + tempResponse.StatusCode + " : Message - " + tempResponse.ReasonPhrase);
            }


            var airHumResponse = await client.GetAsync("airhumidity/plantid/" + Plant.Id);


            if (airHumResponse.IsSuccessStatusCode)
            {
                var airHums = await airHumResponse.Content.ReadAsAsync<IEnumerable<AirHumidity>>();
                foreach (var item in airHums)
                {
                    airHumList.Add(item);
                    Console.WriteLine("airhum: " + item.Humidity);
                }
            }
            else
            {
                Console.WriteLine("Error Code" + tempResponse.StatusCode + " : Message - " + tempResponse.ReasonPhrase);
            }

            var soilHumResponse = await client.GetAsync("soilhumidity/plantid/" + Plant.Id);

            if (soilHumResponse.IsSuccessStatusCode)
            {
                var soilHums = await soilHumResponse.Content.ReadAsAsync<IEnumerable<SoilHumidity>>();
                foreach (var item in soilHums)
                {
                    soilHumList.Add(item);
                    Console.WriteLine("soilhum: " + item.Humidity);
                }
            }
            else
            {
                Console.WriteLine("Error Code" + tempResponse.StatusCode + " : Message - " + tempResponse.ReasonPhrase);
            }
            PopulateData();
            Console.WriteLine("count: " + datacount);
        }

        private void PopulateData()
        {
            for (int i = 0; i < datacount; i++)
            {
                plantDataList.Add(new PlantData()
                {
                    Date = tempList[i].Date,
                    Temp = tempList[i].Temp,
                    AirHum = airHumList[i].Humidity,
                    SoilHum = soilHumList[i].Humidity
                });
            }
        }

    }




}
