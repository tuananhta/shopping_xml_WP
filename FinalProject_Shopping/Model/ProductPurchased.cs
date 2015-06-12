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
    public class ProductPurchased
    {
        //The Id property is marked as the Primary Key  
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        public int planId { get; set; }
        public string Itemname { get; set; }
        public string ItemDescription { get; set; }
        public string ItemCategory { get; set; }
        public string ItemID { get; set; }
        public string ItemImage { get; set; }
        public double Pricing { get; set; }
        public DateTime PurchasedDate { get; set; }

        public ProductPurchased() { }
        public ProductPurchased(int planId, string itemName, string itemDes, string itemCat, string itemID, string itemImage, double price)
        {
            this.planId = planId;
            this.Itemname = itemName;
            this.ItemCategory = itemCat;
            this.ItemDescription = itemDes;
            this.Pricing = price;
            this.ItemID = itemID;
            this.ItemImage = itemImage;
        }
    }
}
