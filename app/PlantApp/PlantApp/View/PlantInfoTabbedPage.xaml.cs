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
    public partial class PlantInfoTabbedPage : TabbedPage
    {
        public PlantInfoTabbedPage()
        {
            InitializeComponent();
        }

        public PlantInfoTabbedPage(SelectedItemChangedEventArgs e)
        {
            InitializeComponent();
        }
    }
}