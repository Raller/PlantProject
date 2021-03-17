using PlantApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlantApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPlantView : ContentPage
    {
        public NewPlantView()
        {
            InitializeComponent();
            BindingContext = new NewPlantViewModel(Navigation);
        }
    }
}