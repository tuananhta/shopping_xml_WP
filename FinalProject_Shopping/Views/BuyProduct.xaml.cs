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
    public sealed partial class BuyProduct : Page
    {
        int planId;
        ProductPurchased newProd = new ProductPurchased();

        public BuyProduct()
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
            PassProductConfirm data = e.Parameter as PassProductConfirm;
            planId = data.planID;

            // Load bitmap image
            BitmapImage bitmapImage = new BitmapImage();
            Uri uri = new Uri(data.prodDetail.ItemImage);
            bitmapImage.UriSource = uri;
            ProductImage.Source = bitmapImage;

            // Load Description
            Price.Text = data.prodDetail.Pricing;
            Name.Text = data.prodDetail.Itemname;
            Description.Text = data.prodDetail.ItemDescription;

            // Update new product info
            newProd.planId = planId;
            newProd.Itemname = data.prodDetail.Itemname;
            newProd.ItemCategory = data.prodDetail.ItemCategory;
            newProd.ItemDescription = data.prodDetail.ItemDescription;
            newProd.Pricing = Convert.ToDouble(data.prodDetail.Pricing);
            newProd.ItemImage = data.prodDetail.ItemImage;
            newProd.ItemID = data.prodDetail.ItemID;
            newProd.PurchasedDate = DateTime.Now;
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();//Creating object for DatabaseHelperClass.cs from ViewModel/DatabaseHelperClass.cs
            ShoppingPlans plan = Db_Helper.ReadShoppingPlan(planId);
            // Update status
            plan.Bought = true;
            Db_Helper.UpdatePlan(plan);

            int weekId = plan.WeekId;
            int year = plan.Year;
            
            // Create to the database
            Db_Helper.InsertProduct(newProd);
            // Read Plan List
            Frame.Navigate(typeof(ReadPlanList), new PassTime { weekId = weekId, year = year });//after add contact redirect to contact listbox page  
            
        }

        private void ReturnProductList_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
