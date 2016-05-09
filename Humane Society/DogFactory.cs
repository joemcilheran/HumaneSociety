using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    class DogFactory : AnimalFactory
    {
        

        override public Animal create_animal(string Breed, string Name, double Weight, bool ImmunizationsUpToDate, string CageNumber, string Adopted)
        {
            return new Dog(Breed, Name, Weight, ImmunizationsUpToDate, CageNumber, Adopted);
        }
    }
}
