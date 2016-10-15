using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MobileApp.ViewModels
{
    public class ShoppingCategoryViewModel : BindableBase
    {
        public ShoppingCategoryViewModel()
        {

        }

        public string Name { get; set; }

        public int Quantity { get; set; }
        public int ScannedQuantity { get; set; }
    }
}
