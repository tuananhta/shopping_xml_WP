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

namespace FinalProject_Shopping.Helpers
{
    //This class for perform all database CRUD operations   
    public class DatabaseHelperClass
    {
        SQLiteConnection dbConn;
        //Create Tabble   
        public async Task<bool> onCreate(string DB_PATH)
        {
            try
            {
                if (!CheckFileExists(DB_PATH).Result)
                {
                    using (dbConn = new SQLiteConnection(DB_PATH))
                    {
                        dbConn.CreateTable<ShoppingPlans>();
                        dbConn.CreateTable<ProductPurchased>();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private async Task<bool> CheckFileExists(string fileName)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Retrieve the specific plan from the database.   
        public ShoppingPlans ReadShoppingPlan(int Planid)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingPlan = dbConn.Query<ShoppingPlans>("select * from ShoppingPlans where Id =" + Planid).FirstOrDefault();
                return existingPlan;
            }
        }
        // Retrieve the all plan list from the database.   
        public ObservableCollection<ShoppingPlans> ShoppingPlans(int weekId, int year)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                List<ShoppingPlans> myCollection = dbConn.Table<ShoppingPlans>().Where(plan => (plan.WeekId == weekId && plan.Year == year)).ToList<ShoppingPlans>();
                ObservableCollection<ShoppingPlans> ShoppingPlansList = new ObservableCollection<ShoppingPlans>(myCollection);
                return ShoppingPlansList;
            }
        }

        //Update existing conatct   
        public void UpdatePlan(ShoppingPlans plan)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingPlan = dbConn.Query<ShoppingPlans>("select * from ShoppingPlans where Id =" + plan.Id).FirstOrDefault();
                if (existingPlan != null)
                {
                    existingPlan.ProductName = plan.ProductName;
                    existingPlan.Bought = plan.Bought;

                    dbConn.RunInTransaction(() =>
                    {
                        dbConn.Update(existingPlan);
                    });
                }
            }
        }
        // Insert the new plan in the plans table.   
        public void Insert(ShoppingPlans newPlan)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.Insert(newPlan);
                });

            }
        
        }

        //---------------------------------------------------------------------------
        // Insert Product 
        public void InsertProduct(ProductPurchased newProduct)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.Insert(newProduct);
                });

            }
        }

        // Retrieve the specific purchased from the database.   
        public ProductPurchased ReadPurchasedPlan(int Planid)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingPurchase = dbConn.Table<ProductPurchased>().First(p => p.planId == Planid);
                return existingPurchase;
            }
        }

        private double getPrice(int planId) {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                ProductPurchased prodPurchased = dbConn.Table<ProductPurchased>().First(p => p.planId == planId);
                if (prodPurchased != null)
                    return prodPurchased.Pricing;
                else
                    return 0;
            }
        }
        // Statistic
        public StatisticView spentMoney(int weekId, int year)
        {
            StatisticView stasView = new StatisticView();

            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                IEnumerable<ShoppingPlans> myCollectionPlans = dbConn.Table<ShoppingPlans>().Where(plan => (plan.WeekId == weekId && plan.Year == year));

                foreach (var item in myCollectionPlans)
                {
                    stasView.totalPlans++;
                    if (item.Bought == true)
                    {
                        stasView.plansDone++;
                        ProductPurchased prodPurchased = dbConn.Table<ProductPurchased>().First(p => p.planId == item.Id);

                        stasView.spentMoney += prodPurchased.Pricing;
                    }
                }               
            }

            return stasView;
        }
        
    } 
}
