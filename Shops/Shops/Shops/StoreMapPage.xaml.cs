using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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

        public async Task IsLocationAvailable()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
						// hitting the ui from a non ui thread as this was called async?  baaaad
                        //await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
	                MyMap.IsShowingUser = true;


                    var results = await CrossGeolocator.Current.GetPositionAsync(new System.TimeSpan(0,0,60));
                    string positionInfo = "Lat: " + results.Latitude + " Long: " + results.Longitude;
                }
                else if (status != PermissionStatus.Unknown)
                {
                    // await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        private async void OnAddPinClicked(object sender, EventArgs e)
        {
            await IsLocationAvailable();


            var point = MyMap.VisibleRegion.Center;
            var item = (await geoCoder.GetAddressesForPositionAsync(point)).FirstOrDefault();

            var name = item ?? "Unknown";

            MyMap.Pins.Add(new Pin
            {
                Label = name,
                Position = point,
                Type = PinType.Generic
            });
            
        }

        private void OnStreetClicked(object sender, EventArgs e) =>
            MyMap.MapType = MapType.Street;

        private void OnHybridClicked(object sender, EventArgs e) =>
            MyMap.MapType = MapType.Hybrid;

        private void OnSatelliteClicked(object sender, EventArgs e) =>
            MyMap.MapType = MapType.Satellite;

        //private async void OnGoToClicked(object sender, EventArgs e)
        //{
        //    var itemz = (await geoCoder.GetPositionsForAddressAsync(EntryLocation.Text));

        //    var item = (await geoCoder.GetPositionsForAddressAsync(EntryLocation.Text)).FirstOrDefault();
        //    if (item == null)
        //    {
        //        await DisplayAlert("Error", "Unable to decode position", "OK");
        //        return;
        //    }

        //    var zoomLevel = SliderZoom.Value; // between 1 and 18
        //    var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
        //    //MyMap.MoveToRegion(new MapSpan(item, latlongdegrees, latlongdegrees));
        //    MyMap.MoveToRegion(new MapSpan(item, .1, .1));
        //}

        private void OnSliderChanged(object sender, ValueChangedEventArgs e)
        {
            var zoomLevel = e.NewValue; // between 1 and 18
            var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
            MyMap.MoveToRegion(new MapSpan(MyMap.VisibleRegion.Center, latlongdegrees, latlongdegrees));
        }

        protected override async void OnAppearing()
        {
	        base.OnAppearing();
	        await IsLocationAvailable();
		}

        
	}
}