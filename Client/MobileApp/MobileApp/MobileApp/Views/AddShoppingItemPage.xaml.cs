using System;
using Xamarin.Forms;

namespace MobileApp.Views
{
    public partial class AddShoppingItemPage : ContentPage
    {
        public AddShoppingItemPage()
        {
            InitializeComponent();
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            const double STEPVALUE = 1.0;
            var newStep = Math.Round(e.NewValue / STEPVALUE);

            Slider.Value = newStep * STEPVALUE;
        }
    }
}
