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
            var page = new NavigationPage(new ViewPlant());
            page.BarBackgroundColor = Color.FromHex("#02f085");
            page.BarTextColor = Color.Black;
            MainPage = page;
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
