using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class LoginPage : ContentPage
    {
        ObservableCollection<Store> stores;

        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            stores = new ObservableCollection<Store>
            {
                new Store{ Id=1,     Name ="Biggs and Company", Address="111 Division St. Buffalo MN", Distance="4mi", StoreImage="icon.png"},
                new Store{ Id=2 ,   Name ="Biggs and Company", Address="111 Division St. Buffalo MN", Distance="4mi", StoreImage="icon.png"},
                new Store{ Id=3,     Name ="Biggs and Company", Address="111 Division St. Buffalo MN", Distance="4mi", StoreImage="icon.png"},
                new Store{ Id=4,    Name ="Biggs and Company", Address="111 Division St. Buffalo MN", Distance="4mi", StoreImage="icon.png"},
                new Store{ Id=5,    Name ="Biggs and Company", Address="111 Division St. Buffalo MN", Distance="4mi", StoreImage="icon.png"},
                new Store{ Id=6 ,   Name ="Biggs and Company", Address="111 Division St. Buffalo MN", Distance="4mi", StoreImage="icon.png"},
                new Store{ Id=7,     Name ="Biggs and Company", Address="111 Division St. Buffalo MN", Distance="4mi", StoreImage="icon.png"}






            };
            shoplistview.ItemsSource = stores;
            BindingContext = new LoginPageViewModel();

        }
    }

    class LoginPageViewModel : INotifyPropertyChanged
    {

        public LoginPageViewModel()
        {
            IncreaseCountCommand = new Command(IncreaseCount);
        }

        int count;

        string countDisplay = "You clicked 0 times.";
        public string CountDisplay
        {
            get { return countDisplay; }
            set { countDisplay = value; OnPropertyChanged(); }
        }

        public ICommand IncreaseCountCommand { get; }

        void IncreaseCount() =>
            CountDisplay = $"You clicked {++count} times";


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}