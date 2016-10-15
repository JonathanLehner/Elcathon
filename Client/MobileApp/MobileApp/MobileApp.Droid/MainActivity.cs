using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms.Platform.Android;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services;
using ImageCircle.Forms.Plugin.Droid;
using Acr.UserDialogs;
using Gcm.Client;
using Android.Content;

namespace MobileApp.Droid
{
    [Activity(Label = "Shoppy", Icon = "@drawable/icon", Theme = "@style/AppTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : XFormsApplicationDroid
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            FormsAppCompatActivity.ToolbarResource = Resource.Layout.Toolbar;
            FormsAppCompatActivity.TabLayoutResource = Resource.Layout.Tabbar;

            // iconify init
            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.MaterialModule());
            FormsPlugin.Iconize.Droid.IconControls.Init();

            // xlabs init
            var container = new SimpleContainer();
            container.Register<IDevice>(t => AndroidDevice.CurrentDevice);
            container.Register<IDisplay>(t => t.Resolve<IDevice>().Display);
            container.Register<INetwork>(t => t.Resolve<IDevice>().Network);
            Resolver.SetResolver(container.GetResolver());

            global::Xamarin.Forms.Forms.Init(this, bundle);

            // xamarin plugins init
            ImageCircleRenderer.Init();

            // init gcm client
            GcmClient.CheckDevice(this);
            GcmClient.CheckManifest(this);

            //Call to Register the device for Push Notifications
            GcmClient.Register(this, GcmBroadcastReceiver.SENDER_IDS);

            LoadApplication(new App());
        }
    }

    [BroadcastReceiver(Permission = Constants.PERMISSION_GCM_INTENTS)]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_MESSAGE },
    Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_REGISTRATION_CALLBACK },
    Categories = new string[] { "@PACKAGE_NAME@" })]
    [IntentFilter(new string[] { Constants.INTENT_FROM_GCM_LIBRARY_RETRY },
    Categories = new string[] { "@PACKAGE_NAME@" })]
    public class GcmBroadcastReceiver : GcmBroadcastReceiverBase<GcmService>
    {
        //IMPORTANT: Change this to your own Sender ID!
        //The SENDER_ID is your Google API Console App Project Number
        public static string[] SENDER_IDS = new string[] { "377000252815" };
    }

    [Service] //Must use the service tag
    public class GcmService : GcmServiceBase
    {
        public GcmService() : base(GcmBroadcastReceiver.SENDER_IDS) { }

        protected override void OnRegistered(Context context, string registrationId)
        {
            //Receive registration Id for sending GCM Push Notifications to
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            //Receive notice that the app no longer wants notifications
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            //Push Notification arrived - print out the keys/values
            if (intent != null && intent.Extras != null)
            {
                //foreach (var key in intent.Extras.KeySet())
                var data = intent.Extras.Get("default").ToString();
                ((App)App.Current).SendMessageForScan(data);
            }
        }
        
        protected override bool OnRecoverableError(Context context, string errorId)
        {
            //Some recoverable error happened
            return true;
        }

        protected override void OnError(Context context, string errorId)
        {
            //Some more serious error happened
        }
    }
}

