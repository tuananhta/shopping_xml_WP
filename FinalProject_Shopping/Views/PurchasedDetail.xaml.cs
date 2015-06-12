using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Globalization;
using SQLite;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using FinalProject_Shopping.Model;
using FinalProject_Shopping.Helpers;
using System.Net.Http;
using System.Xml.Linq;
using System.Xml;
using Windows.Data.Xml.Dom;
using Windows.UI.Xaml.Media.Imaging; // BitmapImage

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace FinalProject_Shopping.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PurchasedDetail : Page
    {
        int planId;
        public PurchasedDetail()
        {
            this.InitializeComponent();         
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PassPurchasedInfo data = e.Parameter as PassPurchasedInfo;
            planId = data.planId;

            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();//Creating object for DatabaseHelperClass.cs from ViewModel/DatabaseHelperClass.cs  

            ProductPurchased proPurchased = Db_Helper.ReadPurchasedPlan(planId);
            // Load bitmap image
            BitmapImage bitmapImage = new BitmapImage();
            Uri uri = new Uri(proPurchased.ItemImage);
            bitmapImage.UriSource = uri;
            ProductImage.Source = bitmapImage;

            // Load Description
            Price.Text = proPurchased.Pricing.ToString() + " € ";
            Name.Text = proPurchased.Itemname;
            Description.Text = proPurchased.ItemDescription;
            BoughtDate.Text = proPurchased.PurchasedDate.ToString();
        }

        private void ReturnProductList_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
