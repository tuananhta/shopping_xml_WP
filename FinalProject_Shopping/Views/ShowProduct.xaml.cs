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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace FinalProject_Shopping.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShowProduct : Page
    {
        int weekId;
        int year;
        string productSearch = "";
        int planId;
        ObservableCollection<ShoppingPlans> DB_ShoppingPlans = new ObservableCollection<ShoppingPlans>(); 

        public ShowProduct()
        {
            this.InitializeComponent();
            this.Loaded += ShowProduct_Loaded;
        }

        private async void ShowProduct_Loaded(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient(); // Add: using System.Net.Http;
            var response = await client.GetAsync(new Uri("http://www.supermarketapi.com/api.asmx/COMMERCIAL_SearchByProductName?APIKEY=APIKEY&ItemName=" + productSearch));
            var resultXML = await response.Content.ReadAsStringAsync();
            string xmlns = "xmlns=\"http://www.SupermarketAPI.com\"";
            resultXML = resultXML.Remove(resultXML.IndexOf(xmlns), xmlns.Length);
            //XDocument.Load("http://www.supermarketapi.com/api.asmx/COMMERCIAL_SearchByProductName?APIKEY=ef5a217103&ItemName=" + productSearch);
            //XDocument xmlDoc = XDocument.Parse(resultXML);

            XDocument xmlDoc = XDocument.Parse(resultXML);
            xmlDoc.Root.RemoveAttributes();

            IEnumerable<ProductDetail> ProductLists = xmlDoc.Descendants("Product_Commercial").Select(Product_Commercial =>
                 new ProductDetail
                 {
                     Itemname = Product_Commercial.Element("Itemname").Value,
                     ItemDescription = Product_Commercial.Element("ItemDescription").Value,
                     ItemCategory = Product_Commercial.Element("ItemCategory").Value,
                     ItemID = Product_Commercial.Element("ItemID").Value,
                     ItemImage = Product_Commercial.Element("ItemImage").Value,
                     Pricing = Product_Commercial.Element("Pricing").Value.ToString()
                 });

            listBoxobj.ItemsSource = ProductLists.ToList();//Binding DB data to LISTBOX and Latest contact ID can Display first. 
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PassPlanInfo data = e.Parameter as PassPlanInfo;
            planId = data.PlanId;

            // receive year and weekId, pass to the properties weekId, year of the class
            this.weekId = data.weekId;
            this.year = data.year;
            ProductNameHeader.Text = data.searchWord;
            productSearch = data.searchWord;
            TimeInfo.Text = "Week: " + weekId + " - Year: " + year;
        }

        private void listBoxobj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxobj.SelectedIndex != -1)
            {
                ProductDetail listitem = listBoxobj.SelectedItem as ProductDetail;//Get slected listbox item contact ID                
                    // to buy product 
                    Frame.Navigate(typeof(BuyProduct), new PassProductConfirm { prodDetail =listitem, planID = this.planId });
            }
        }

        private void Return_Plan_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ReadPlanList), new PassTime { weekId=this.weekId, year=this.year });
        }
    }
}
