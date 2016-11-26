using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Todo2.Droid.CustomControlRenderers;
using Todo2.Pages.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using PickerRenderer = Xamarin.Forms.Platform.Android.AppCompat.PickerRenderer;

[assembly: ExportRenderer(typeof(PriorityPicker), typeof(PriorityPickerRenderer))]
namespace Todo2.Droid.CustomControlRenderers
{
    class PriorityPickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetTextSize(global::Android.Util.ComplexUnitType.Dip, (float) (8 * App.ScreenSizeCoefficient));
            }
        }
    }
}