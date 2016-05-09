using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    class Bank
    {
        public int total;
        public Bank(int Total)
        {
            total = Total;   
        }
        public string create_variable_string()
        {
            string totalAsString = Convert.ToString(total);
            string variableString = String.Format(totalAsString);
            return variableString;
        }

    }
}
