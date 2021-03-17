using PlantApp.Model;
using PlantApp.Utils;
using PlantApp.View;
using RateABeer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PlantApp.ViewModel
{
    public class NewPlantViewModel : Bindable
    {

        private Plant plant = new Plant();
        public Plant Plant { get { return plant; } set { plant = value; OnPropertyChanged(); } }

        private Image image = new Image();
        public Image Image { get { return image; } set { image = value; OnPropertyChanged(); } }

        public ICommand CancelCommand { get; private set; }
        public ICommand TakePhotoCommand { get; private set; }
        public ICommand OpenMapCommand { get; private set; }



        INavigation navigation;
        public NewPlantViewModel()
        {

        }
        public NewPlantViewModel(INavigation nav)
        {
            navigation = nav;
            CancelCommand = new Command(CancelPlant);
            TakePhotoCommand = new Command(TakePicture);
            OpenMapCommand = new Command(OpenMap);
        }

        private async void CancelPlant()
        {
            await navigation.PopAsync();
        }

        private async void TakePicture()
        {
            Image.Source = await Camera.TakePhotoAsync();
        }

        private async void OpenMap()
        {
            Console.WriteLine("navigated");
            await navigation.PushAsync(new MapView());
        }





    }
}
