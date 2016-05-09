using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    class Dog: Animal
    {
        public Dog(string Breed, string Name, double Weight, bool ImmunizationsUpToDate, string CageNumber, string Adopted)
        {
            type = "dog";
            foodType = "dog";
            foodPerDay = "3";
            breed = Breed;
            name = Name;
            weight = Weight;
            immunizationsUpToDate = ImmunizationsUpToDate;
            cageNumber = CageNumber;
            adopted = Adopted;

        }
        public override string create_variable_string()
        {
            string variableString = String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}", name, type, breed, weight, foodType, foodPerDay, immunizationsUpToDate, cageNumber, adopted);
            return variableString;
        }
    }
}
