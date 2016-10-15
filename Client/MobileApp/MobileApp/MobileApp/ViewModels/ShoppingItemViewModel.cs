using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobileApp.ViewModels
{
    public class ShoppingItemViewModel : BindableBase
    {
        public ShoppingItemViewModel()
        {

        }

        public string Name { get; set; }

        public decimal Price
        {
            get;set;
        }

        public decimal TotalPrice => Price * Quantity;
        public string Image { get; set; }
        private int _quantity;
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }
        public string CategoryName { get; set; }
    }
}
