using PlantApp.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlantApp
{
    public partial class App : Application
    {
        public App()
        {
            Xamarin.Forms.DataGrid.DataGridComponent.Init();

            InitializeComponent();

            MainPage = new NavigationPage(new ViewPlant());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
