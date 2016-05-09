using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    abstract class AnimalFactory
    {
        public AnimalFactory()
        {}
        public abstract Animal create_animal(string Breed, string Name, double Weight, bool ImmunizationsUpToDate, string CageNumber, string Adopted);
        
    }
}
