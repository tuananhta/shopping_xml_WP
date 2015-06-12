using System;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace FinalProject_Shopping.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ReadPlanList : Page
    {
        int weekId;
        int year;
        ObservableCollection<ShoppingPlans> DB_ShoppingPlans = new ObservableCollection<ShoppingPlans>();
        public ReadPlanList()
        {
            this.InitializeComponent();
            this.Loaded += ReadPlanList_Loaded;
        }
        private void ReadPlanList_Loaded(object sender, RoutedEventArgs e)
        {
            ReadAllShoppingPlansList dbShoppingPlans = new ReadAllShoppingPlansList();
            DB_ShoppingPlans = dbShoppingPlans.GetAllPlans(weekId, year);//Get all DB contacts 
            listBoxobj.ItemsSource = DB_ShoppingPlans.OrderByDescending(i => i.Id).ToList();//Binding DB data to LISTBOX and Latest contact ID can Display first. 
        }

        private void listBoxobj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int SelectedPlanID = 0;
            if (listBoxobj.SelectedIndex != -1)
            {
                ShoppingPlans listitem = listBoxobj.SelectedItem as ShoppingPlans;//Get slected listbox item contact ID 
                if (listitem.Bought == false)
                {
                    // to buy product 
                    Frame.Navigate(typeof(ShowProduct), new PassPlanInfo { PlanId = listitem.Id, searchWord = listitem.ProductName, weekId = this.weekId, year = this.year });
                }
                else
                {
                    // show purchased history
                    Frame.Navigate(typeof(PurchasedDetail), new PassPurchasedInfo { planId = listitem.Id });
                }
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PassTime data = e.Parameter as PassTime;

            // receive year and weekId, pass to the properties weekId, year of the class
            this.weekId = data.weekId;
            this.year = data.year;
        }

        private void AddPlan_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddPlan));
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ReturnHomeScreen_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
