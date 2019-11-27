using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin;
using Plugin.Permissions;
using HockeyApp.Android;

namespace Shops.Droid
{
    [Activity(Label = "Shops", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
		// i have put a name to my pain, and it is android 6.0+ permissions
		//https://github.com/jamesmontemagno/MarshmallowSamples/blob/master/RuntimePermissions/MarshmallowPermission/MainActivity.cs
		//https://blog.xamarin.com/requesting-runtime-permissions-in-android-marshmallow/

		protected override void OnResume()
		{
			base.OnResume();
			CrashManager.Register(this, "85ba420d3a4d412b953a94f1d385ac91");
		}

        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
			LoadApplication(new App());
        }

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
		{
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}

