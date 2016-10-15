using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class ShoppingCategoryViewModel : BindableBase
    {
        public ShoppingCategoryViewModel()
        {

        }

        public string Name { get; set; }

        public int Quantity { get; set; }

        private int _scannedQuantity;
        public int ScannedQuantity
        {
            get
            {
                return _scannedQuantity;
            }
            set
            {
                if(_scannedQuantity != value)
                {
                    _scannedQuantity = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(BackgroundColor));
                    OnPropertyChanged(nameof(QuantityLeft));
                }
            }
        }

        public int QuantityLeft => Quantity - ScannedQuantity;

        public Color BackgroundColor => ScannedQuantity >= Quantity ? Color.FromHex("#b3e0ff") : Color.Transparent;
    }
}
