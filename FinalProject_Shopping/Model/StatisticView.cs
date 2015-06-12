using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject_Shopping.Model
{
    public class StatisticView
    {
        public double spentMoney {get;set;}
        public int totalPlans { get; set; }
        public int plansDone { get; set; }

        public StatisticView()
        {
            spentMoney = 0.0;
            totalPlans = 0;
            plansDone = 0;
        }
    }
}
