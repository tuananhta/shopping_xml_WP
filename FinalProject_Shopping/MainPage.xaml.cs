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
using FinalProject_Shopping.Views;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace FinalProject_Shopping
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        static DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
        static Calendar cal = dfi.Calendar;
        static int WeekId = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule,
                                          dfi.FirstDayOfWeek);
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();//Creating object for DatabaseHelperClass.cs from ViewModel/DatabaseHelperClass.cs  

            StatisticView stasView = Db_Helper.spentMoney(WeekId, DateTime.Now.Year);
            Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            TotalMoney.Text = stasView.spentMoney.ToString() + " €";
            TotalPlans.Text = stasView.totalPlans.ToString();
            PlansDone.Text = stasView.plansDone.ToString();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ReadPlanList), new PassTime { weekId = WeekId, year = DateTime.Now.Year });//after add contact redirect to contact listbox page
        }
    }
}
