using PlantApp.Model;
using PlantApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PlantApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlantInfoTabbedPage : TabbedPage
    {
        public PlantInfoTabbedPage()
        {
            InitializeComponent();
        }

        Plant plant;

        public PlantInfoTabbedPage(Plant selectedPlant)
        {
            plant = selectedPlant;
            InitializeComponent();
            infoPage.BindingContext = new PlantInfoViewModel(selectedPlant);
            historyPage.BindingContext = new PlantHistoryViewModel(selectedPlant);

            string lat = selectedPlant.Latitude;
            string lgt = selectedPlant.Longitude;

            /*if (selectedPlant.Latitude.Contains(',') || selectedPlant.Longitude.Contains(','))
            {
                lat = selectedPlant.Latitude.Replace(',', '.');
                lgt = selectedPlant.Longitude.Replace(',', '.');
            }*/

            try
            {
                Pin pin = new Pin
                {
                    Label = "Plantens lokation",
                    Type = PinType.Place,
                    Position = new Position(double.Parse(lat), double.Parse(lgt))
                };
                map.Pins.Add(pin);
                map.MoveToRegion(new MapSpan(new Position(double.Parse(lat), double.Parse(lgt)), 0.01, 0.01));
            } catch (Exception e)
            {
                DisplayMessage();
            }
        }

        private async void DisplayMessage()
        {
            await Application.Current.MainPage.DisplayAlert("Fejl", "Kunne ikke finde placering", "Ok");
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://plantprojectapi.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var answer = await DisplayAlert("Advarsel", "Vil du slette denne plante?", "Ja", "Nej");

            try
            {
                if(answer)
                {
                    await client.DeleteAsync("plant/id/" + plant.Id);
                    await Navigation.PopAsync();
                }
            } catch (Exception ex)
            {
                await DisplayAlert("Fejl", "Kunne ikke slette denne plante", "Ok");
            }

        }
    }
}