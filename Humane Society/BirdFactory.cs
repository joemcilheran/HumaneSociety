using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    class BirdFactory: AnimalFactory
    {
        public BirdFactory()
        {

        }
        override public Animal create_animal(string Breed, string Name, double Weight, bool ImmunizationsUpToDate, string CageNumber, string Adopted)
        {
            return new Bird(Breed, Name, Weight, ImmunizationsUpToDate, CageNumber, Adopted);
        }
    }
}
