using PlantApp.ViewModel;
using RateABeer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PlantApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapView : ContentPage
    {


        public MapView()
        {
            GetLocation();
            InitializeComponent();
        }

        async void GetLocation()
        {
            var position = await Geolocation.GetLocationAsync();
            map.MoveToRegion(new MapSpan(new Position(position.Latitude, position.Longitude), 0.01, 0.01));

        }

        private void Map_MapClicked(object sender, MapClickedEventArgs e)
        {
            map.Pins.Clear();
            Console.WriteLine($"MapClick: {e.Position.Latitude}, {e.Position.Longitude}");
            Pin pin = new Pin
            {
                Label = "Plantens lokation",
                Type = PinType.Generic,
                Position = new Position(e.Position.Latitude, e.Position.Longitude)
            };
            map.Pins.Add(pin);
            NewPlantViewModel.position = new Location(e.Position.Latitude, e.Position.Longitude);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}