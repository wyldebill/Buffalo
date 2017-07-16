using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Shops
{

    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreMapPage : ContentPage
    {
        Geocoder geoCoder;

        public StoreMapPage()
        {

            InitializeComponent();
            geoCoder = new Geocoder();


            // for now, intialize the location to Buffalo, arbitrary point
            MyMap.MoveToRegion(new MapSpan(new Position(45.172386, -93.874920), .001, .001));


            // now let's add a pin, just default pin without custom ui goodness.  yes, it's ugly
            ShopMapData site = new ShopMapData()
            {
                Address = "105 Divison St. Buffalo MN 55313",
                Name = "Biggs & Company",
                Website = "www.biggsandcompany.com",
                Latitude = 45.172182,
                Longitude = -93.874363
            };

            Pin pin = new Pin
            {
                BindingContext = site,
                Label = site.Name,
                Position = new Position(site.Latitude, site.Longitude),
                Address = site.Address
            };

            //pin.Clicked += OnPinClicked;
            MyMap.Pins.Add(pin);

        }

        

        private async void OnAddPinClicked(object sender, EventArgs e)
        {
            var point = MyMap.VisibleRegion.Center;
            var item = (await geoCoder.GetAddressesForPositionAsync(point)).FirstOrDefault();

            var name = item ?? "Unknown";

            MyMap.Pins.Add(new Pin
            {
                Label = name,
                Position = point,
                Type = PinType.Generic
            });
            //  MyMap.MoveToRegion(new MapSpan(new Position(37.8044866, -122.4324132), 360, 360));
        }

        private void OnStreetClicked(object sender, EventArgs e) =>
            MyMap.MapType = MapType.Street;

        private void OnHybridClicked(object sender, EventArgs e) =>
            MyMap.MapType = MapType.Hybrid;

        private void OnSatelliteClicked(object sender, EventArgs e) =>
            MyMap.MapType = MapType.Satellite;

        private async void OnGoToClicked(object sender, EventArgs e)
        {
            var itemz = (await geoCoder.GetPositionsForAddressAsync(EntryLocation.Text));

            var item = (await geoCoder.GetPositionsForAddressAsync(EntryLocation.Text)).FirstOrDefault();
            if (item == null)
            {
                await DisplayAlert("Error", "Unable to decode position", "OK");
                return;
            }

            var zoomLevel = SliderZoom.Value; // between 1 and 18
            var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
            //MyMap.MoveToRegion(new MapSpan(item, latlongdegrees, latlongdegrees));
            MyMap.MoveToRegion(new MapSpan(item, .1, .1));
        }

        private void OnSliderChanged(object sender, ValueChangedEventArgs e)
        {
            var zoomLevel = e.NewValue; // between 1 and 18
            var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
            MyMap.MoveToRegion(new MapSpan(MyMap.VisibleRegion.Center, latlongdegrees, latlongdegrees));
        }

        protected  override void OnAppearing()
        {
            //MyMap.MoveToRegion(new MapSpan(new Position(45.1718, -93.874), 180, 180));

        }
    }
}