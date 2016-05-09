using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    class CatCage: Cage
    {
        public CatCage(string Name, string Status)
        {
            type = "cat";
            name = Name;
            status = Status;

        }
        public override string create_variable_string()
        {
            string variableString = String.Format("{0} {1} {2}", type, name, status);
            return variableString;
        }
    }
}
