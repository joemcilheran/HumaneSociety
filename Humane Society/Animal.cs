using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    abstract class Animal
    {
        public string type;
        public string breed;
        public string name;
        public double weight;
        public string foodType;
        public string foodPerDay;
        public bool immunizationsUpToDate;
        public string cageNumber;
        public string adopted;
        public Animal()
        {
        }
        public abstract string create_variable_string();

    }
    
}
