using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shops
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreTypeSelectionPage : ContentPage
    {
        List<StoreSelectionItem> storeSelection;
        public StoreTypeSelectionPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);

            storeSelection = new List<StoreSelectionItem>
            {
                new StoreSelectionItem{ Id=1,     Name ="EveryDay", Description="These stores typically...", StoreTypeImage="icon.png"},
                new StoreSelectionItem{ Id=1,     Name ="Weekends", Description="These stores typically are open Weekends only...", StoreTypeImage="icon.png"},
                new StoreSelectionItem{ Id=1,     Name ="Occasional", Description="These stores are open the same time each month...", StoreTypeImage="icon.png"}
                //new Store{ Id=4,    Name ="Biggs and Company", Address="111 Division St. Buffalo MN", Distance="4mi", StoreImage="icon.png"},
                //new Store{ Id=5,    Name ="Biggs and Company", Address="111 Division St. Buffalo MN", Distance="4mi", StoreImage="icon.png"},
                //new Store{ Id=6 ,   Name ="Biggs and Company", Address="111 Division St. Buffalo MN", Distance="4mi", StoreImage="icon.png"},
                //new Store{ Id=7,     Name ="Biggs and Company", Address="111 Division St. Buffalo MN", Distance="4mi", StoreImage="icon.png"}
            };
            storeTypeSelectionList.ItemsSource = storeSelection;

            //BindingContext = new StoreTypeSelectionPageViewModel();
        }
    }

   
}