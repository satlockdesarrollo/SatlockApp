using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SatlockApp.Droid;
using SatlockApp.Renders;

[assembly: Xamarin.Forms.Dependency(typeof(ToastInterface))]
namespace SatlockApp.Droid
{
    class ToastInterface : ToastMessage
    {
        public void Show(string message)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }

    }
}