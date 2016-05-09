using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    class UserInput
    {


        public UserInput()
        {
        }
        public void display_welcome()
        {
            Console.WriteLine("Welcome to the Humane Society Shelter System.");
        }
        public string get_task()
        {
            Console.WriteLine("Please choose from the following:\n 'adoption'\n 'check-in'\n 'immunization'\n 'create cage'\n 'check food inventory'\n 'feed animals'\n 'check bank total'\n 'view animals'\n 'view cages'");
            string input = Console.ReadLine();
            return input;
        }
        public string get_name()
        {
            Console.WriteLine("Enter name of animal.");
            string input = Console.ReadLine();
            return input;
        }
        public string get_type()
        {
            Console.WriteLine("Are you checking in a dog, cat, or bird?");
            string input = Console.ReadLine();
            return input;
        }
        public string get_breed()
        {
            Console.WriteLine("Enter breed.");
            string input = Console.ReadLine();
            return input;
        }
        public double get_weight()
        {
            Console.WriteLine("Enter weight in pounds.");
            string input = Console.ReadLine();
            double inputAsDouble = Convert.ToDouble(input);
            return inputAsDouble;
        }
        public bool check_immunization()
        {
            Console.WriteLine("Are immunizations up to date?");
            string input = Console.ReadLine();
            if (input == "yes")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void display_unavailibily()
        {
            Console.WriteLine("No space available for that type of animal at this time.");
        }
        public string get_new_task()
        {
            Console.WriteLine("What would you like to do? Enter: 'quit' or 'other task'.");
            string input = Console.ReadLine();
            return input;
        }
        public void display_NotAcceptingType()
        {
            Console.WriteLine("We are only accepting Cats, Dogs, and Birds at this time.");
        }
        public void display_not_making_cageType()
        {
            Console.WriteLine("We can only create cages for Cats, Dogs, and Birds at this time.");
        }
        public string get_cage_choice()
        {
            Console.WriteLine("What type of cage would you like to make: 'dog cage', 'cat cage', or 'bird cage'?");
            string input = Console.ReadLine();
            return input;
        }
        public int get_number_of_cages()
        {
            Console.WriteLine("How many cages would you like to make?");
            string input = Console.ReadLine();
            int output = Convert.ToInt16(input);
            return output;

        }
        public void confirm_cagesCreated(int number, string type)
        {
            Console.WriteLine("{0} {1} cages created!", number, type);
        }
        public string get_cageNumber_choice()
        {
            Console.WriteLine("enter the number of the cage you would like.");
            string input = Console.ReadLine();
            return input;
        }
        public string get_immunization_check()
        {
            Console.WriteLine("Would you like to: 'check an animal by name', 'check all cats', 'check all dogs', or 'check all birds'?");
            string input = Console.ReadLine();
            return input;
        }
        public string get_name_to_check_immunization()
        {
            Console.WriteLine("Enter the name of the animal you wish to check.");
            string input = Console.ReadLine();
            return input;
        }
        public string ask_to_immunize(string type, string animalName)
        {
            Console.WriteLine("The {0} {1} needs to be immunized. Would you like to immunize {1} now?", type, animalName);
            string answer = Console.ReadLine();
            return answer;
        }
        public string confirm_immunized_animal(string type, string animalName)
        {
            Console.WriteLine("The {0} {1} is now immunized", type, animalName);
            string answer = Console.ReadLine();
            return answer;
        }
        public string confirm_immunized_type(string type)
        {
            Console.WriteLine("All {0}s are now immunized", type);
            string answer = Console.ReadLine();
            return answer;
        }
        public string ask_which_to_immunize(string type)
        {
            Console.WriteLine("Would you like to immunize 'all {0}s' or 'one {0}'?", type);
            string answer = Console.ReadLine();
            return answer;
        }
        public string get_name_to_immunize()
        {
            Console.WriteLine("Enter the name of the animal you wish to immunize.");
            string input = Console.ReadLine();
            return input;
        }
        public void display_alreadyImmunized(string type, string animalName)
        {
            Console.WriteLine("The {0} {1}'s immunizations are up to date.", type, animalName);
        }
        public void display_none_to_be_immunized(string type)
        {
            Console.WriteLine("No {0}s need immunizations at this time.", type);
        }
        public void display_no_animal_by_that_name(string type)
        {
            Console.WriteLine("There is no {0} by that name in the system.", type);
        }
        public string display_input_error()
        {
            Console.WriteLine("Invalid input. Would you like to 'try again', 'quit', or do 'something else'?");
            string answer = Console.ReadLine();
            return answer;
        }
        public void display_cageFile(string[] lineArray)
        {
            Console.WriteLine("{0} cage #{1} occupied by: {2}", lineArray[0], lineArray[1], lineArray[2]);
        }
        public void display_animalFile(string[] lineArray)
        {
            Console.WriteLine("{0}\t\n type: {1}\n breed: {2}\n weight: {3} pounds\n foodType: {4}food\n foodPerDay: {5} cups\n immunizaionsUpToDate: {6}\n cagenumber: {7}\n adopted: {8}", lineArray[0],lineArray[1],lineArray[2],lineArray[3],lineArray[4],lineArray[5],lineArray[6],lineArray[7],lineArray[8]);
        }
        public void display_foodInventoryFile(string[] lineArray)
        {
            Console.WriteLine("{0}: {1}cups", lineArray[0], lineArray[1]);
        }
        public string ask_to_buy_foodType(string foodType)
        {
            Console.WriteLine("Would you like to buy more {0}?", foodType);
            string input = Console.ReadLine();
            return input;
        }
        public string ask_to_buy_more_food()
        {
            Console.WriteLine("Would you like to buy food?");
            string input = Console.ReadLine();
            return input;
        }
        public void display_new_inventoryTotal(string foodType, int newAmount)
        {
            Console.WriteLine("{0} inventory now at {1} cups.", foodType, newAmount);
        }
        public void display_adopter(string[] lineArray)
        {
            Console.WriteLine("name: {0}, pets: {1}", lineArray[0], lineArray[1]);
        }
        public string ask_which_to_type_to_adopt()
        {
            Console.WriteLine("Enter the name of the animal who will be adopted or enter 'cats', 'dogs', or 'birds' to display all cats, dogs, or birds.");
            string answer = Console.ReadLine();
            return answer;
        }
        public string ask_which_breed_to_adopt(string type)
        {
            Console.WriteLine("Enter the name of the {0} who will be adopted or enter 'breed' to search by breed.", type);
            string input = Console.ReadLine();
            return input;
        }
        public string ask_for_breed()
        {
            Console.WriteLine("What breed would you like to search?");
            string input = Console.ReadLine();
            return input;
        }
        public string ask_name_to_adopt(string type, string breed)
        {
            Console.WriteLine("Enter the name of the {1} {0} who will be adopted.", type, breed);
            string input = Console.ReadLine();
            return input;
        } 
        public string confirm_animal_to_adopt(string animalName)
        {
            Console.WriteLine("Would you like to run adoption for {0} now?", animalName);
            string input = Console.ReadLine();
            return input;
        }
        public string get_adopterName()
        {
            Console.WriteLine("Enter the name of the adopter");
            string input = Console.ReadLine();
            return input;
        }
        public void confirm_adoption(string animalName, string adopterName)
        {
            Console.WriteLine("{0} has been adopted by {1}.", animalName, adopterName);
        }
        public void display_noneAvailableToAdopt()
        {
            Console.WriteLine("No animals by that name are available to adopt at this time.");
        }
        public void display_breed_unavailable_for_adoption(string type)
        {
            Console.WriteLine("No {0} of that breed is available for adoption at this time.", type);
        }
        public string ask_who_to_feed()
        {
            Console.WriteLine("Would you like to feed 'cats', 'dogs', or 'birds'?");
            string input = Console.ReadLine();
            return input;
        }
        public void insufficient_food(string type)
        {
            Console.WriteLine("Not enough food to feed all {0}.", type);
        }
        public void confirm_animals_fed(string type)
        {
            Console.WriteLine("All {0}s now fed.", type);
        }
        public void display_bankTotal(string bankTotal)
        {
            Console.WriteLine("Bank total: ${0}", bankTotal);
        }
        public string which_cages_to_display()
        {
            Console.WriteLine("Would you like to view 'cat cages', 'bird cages', or 'dog cages'?");
            string input = Console.ReadLine();
            return input;
        }
        public string which_animals_to_display()
        {
            Console.WriteLine("Would you like to view 'adopted', 'unadopted', or 'all'?");
            string input = Console.ReadLine();
            return input;

        }
        



    }
    
}
