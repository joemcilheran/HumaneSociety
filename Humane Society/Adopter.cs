using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    class Adopter
    {
        string adopterName;
        List<string> petsAdopted;

        public Adopter(string AdopterName, List<string> PetsAdopted)
        {
            adopterName = AdopterName;
            petsAdopted = PetsAdopted;
        }
        public string create_variable_string()
        {
            string petsListString = String.Join("/", petsAdopted);
            string variableString = String.Format("{0} {1}", adopterName, petsListString);
            return variableString;
        }
    }
}
