using PlantApp.Model;
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
    public partial class PlantLocationView : ContentPage
    {
        public PlantLocationView(Plant plant)
        {
            string lat = "";
            string lgt = "";

            if (plant.Latitude.Contains(',') || plant.Longitude.Contains(','))
            {
                lat = plant.Latitude.Replace(',', '.');
                lgt = plant.Longitude.Replace(',', '.');
            }

            InitializeComponent();

            Pin pin = new Pin
            {
                Label = "Plantens lokation",
                Type = PinType.Place,
                Position = new Position(double.Parse(lat), double.Parse(lgt))
            };
            map.Pins.Add(pin);
            map.MoveToRegion(new MapSpan(new Position(double.Parse(lat), double.Parse(lgt)), 0.01, 0.01));
        }
    }
}