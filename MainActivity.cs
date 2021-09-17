using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;

namespace Sender
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        string number;
        EditText phone;
        Button whatsapp, telegram;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            phone = FindViewById<EditText>(Resource.Id.numberEntry);
            whatsapp = FindViewById<Button>(Resource.Id.whatsappBtn);
            telegram = FindViewById<Button>(Resource.Id.telegramBtn);

            //clicked handlers
            telegram.Click += Telegram_Click;
            whatsapp.Click += Whatsapp_Click;
        }
        private void CheckNumber()
        {
            if (number.StartsWith("+91") && number.Length == 12)
                return;
            else if (number.Length == 10)
                number = "+91" + number;
            else
                Toast.MakeText(this, "Enter a valid number", ToastLength.Short);

        }

        private void Whatsapp_Click(object sender, System.EventArgs e)
        {
            number = phone.Text;
            if (!string.IsNullOrEmpty(number))
            {
                CheckNumber();
                string url = $"https://api.whatsapp.com/send?phone=${number}";

                try
                {
                    PackageManager.GetPackageInfo("com.whatsapp", Android.Content.PM.PackageInfoFlags.Activities);
                    Intent intent = new Intent(Intent.ActionView);

                    intent.SetData(Android.Net.Uri.Parse(url));
                    StartActivity(intent);
                }
                catch (Exception)
                {
                    Toast.MakeText(this, "Whatsapp is not installed in your phone.", ToastLength.Short).Show();
                }

            }
            else
            {
                Toast.MakeText(this, "Please enter a valid number", ToastLength.Short).Show();
            }



        }

        

        private void Telegram_Click(object sender, System.EventArgs e)
        {
            //CheckNumber();
            //string url = $"https://api.telegram.com/send?phone=${number}";
            Toast.MakeText(this, "this feature is currently not available.", ToastLength.Short).Show();

            //try
            //{
            //    PackageManager.GetPackageInfo("com.telegram", Android.Content.PM.PackageInfoFlags.Activities);
            //    Intent intent = new Intent(Intent.ActionView);
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}