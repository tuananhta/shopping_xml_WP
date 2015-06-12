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

namespace FinalProject_Shopping.Helpers
{
    public class ReadAllShoppingPlansList
    {
            DatabaseHelperClass Db_Helper = new DatabaseHelperClass();
            public ObservableCollection<ShoppingPlans> GetAllPlans(int weekId, int year)
            {
                return Db_Helper.ShoppingPlans(weekId, year);
            }
    }
}
