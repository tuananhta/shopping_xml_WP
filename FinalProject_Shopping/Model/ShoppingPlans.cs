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

namespace FinalProject_Shopping.Model
{
    public class ShoppingPlans
    {
        //The Id property is marked as the Primary Key  
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        public int WeekId { get; set; }
        public int Year { get; set; }
        public string ProductName { get; set; }
        public bool Bought { get; set; }
        public ShoppingPlans()
        {
            //empty constructor  
        }
        public ShoppingPlans(string productName, bool bought = false)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            Calendar cal = dfi.Calendar;

            ProductName = productName;
            Bought = bought;
            WeekId = cal.GetWeekOfYear(DateTime.Now, dfi.CalendarWeekRule,
                                          dfi.FirstDayOfWeek);
            Year = DateTime.Now.Year;
        }
    }
}
