using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    class FoodInventory
    {
        string foodType;
        int amount;
        public FoodInventory(string FoodType, int Amount)
        {
            foodType = FoodType;
            amount = Amount;
        }
        public string create_variable_string()
        {
            string amountAsString = Convert.ToString(amount);
            string variableString = String.Format("{0} {1}", foodType, amount);
            return variableString;
        }
    }
    
}
