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
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
