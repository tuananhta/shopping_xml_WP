using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class AddPlan : Page
    {
        static DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
        static Calendar cal = dfi.Calendar;
        static int WeekId = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule,
                                          dfi.FirstDayOfWeek);
        public AddPlan()
        {
            this.InitializeComponent();
            

            string dayInfo ="";       
            dayInfo += "Week: " + WeekId.ToString() + " - Year: " + DateTime.Now.Year.ToString();
            DayInfo.Text = dayInfo;          
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();//Creating object for DatabaseHelperClass.cs from ViewModel/DatabaseHelperClass.cs  
            if (ProductName.Text != "")
            {
                Db_Helper.Insert(new ShoppingPlans(ProductName.Text));
                Frame.Navigate(typeof(ReadPlanList), new PassTime { weekId = WeekId, year = DateTime.Now.Year });//after add contact redirect to contact listbox page  
            }
            else
            {
                MessageDialog messageDialog = new MessageDialog("Please fill the field");//Text should not be empty  
                await messageDialog.ShowAsync();
            } 
        }
    }
}
