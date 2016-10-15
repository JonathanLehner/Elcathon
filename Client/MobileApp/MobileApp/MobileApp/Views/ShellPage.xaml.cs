using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MobileApp.Views
{
    public partial class ShellPage : MasterDetailPage
    {
        public ShellPage()
        {
            InitializeComponent();

            Master = new ScannedListPage();
            Detail = new NavigationPage(new ContentPage());

        }
    }
}
