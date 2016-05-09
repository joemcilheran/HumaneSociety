using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    class Shelter
    {       
        public UserInput userInput;
        public FileInteractions fileInteractions;
        public Shelter()
        {
            userInput = new UserInput();
            fileInteractions = new FileInteractions();
        }
        public void run_shelter()
        {
            userInput.display_welcome();
            run_program();
        }
        public void run_program()
        {
            
            string task =  userInput.get_task();
            switch(task)
            {
                case "adoption":
                    Adoption adoption = new Adoption();
                    adoption.run_adoption();
                    break;
                case "check-in":
                    CheckIn checkIn = new CheckIn();
                    checkIn.run_checkIn();
                    break;
                case "immunization":
                    Immunization immunization = new Immunization();
                    immunization.run_immunization();
                    break;
                case "create cage":
                    CageCreation cageCreation = new CageCreation();
                    cageCreation.run_cage_creation();
                    break;
                case "check food inventory":
                    FoodAndFeeding foodAndFeeding = new FoodAndFeeding();
                    foodAndFeeding.run_foodInventory();
                    break;
                case "feed animals":
                    FoodAndFeeding foodAndFeedingOne = new FoodAndFeeding();
                    foodAndFeedingOne.run_feeding();
                    break;
                case "check bank total":
                    run_bank();
                    break;
                case "view animals":
                    run_animal_view();
                    break;
                case "view cages":
                    run_cage_view();
                    break;
                case "view adopters":
                    diplay_all_adopters();
                    break;
                default:
                    run_new_task();
                    break;
            }
            run_new_task();
        }
       public void run_bank()
        {
        
            string path = fileInteractions.get_file_path("bank");
            if (!File.Exists(path))
            {
                fileInteractions.create_file(path);
                Bank bank = new Bank(0);
                string bankTotal = bank.create_variable_string();
                fileInteractions.write_all_text(path, bankTotal);

            }
            display_bank(path);       
         }
        public void run_cage_view()
        {
            string input = userInput.which_cages_to_display();
            switch(input)
            {
                case "dog cages":
                    string dogCagePath = fileInteractions.get_file_path("dogCages");
                    display_cageFile(dogCagePath);
                    break;
                case "cat cages":
                    string catCagePath = fileInteractions.get_file_path("catCages");
                    display_cageFile(catCagePath);
                    break;
                case "bird cages":
                    string birdCagePath = fileInteractions.get_file_path("birdCages");
                    display_cageFile(birdCagePath);
                    break;
                default:
                    run_new_task();
                    break;
            }
        }
        public void run_animal_view()
        {
            string input = userInput.which_type_to_display();
            switch(input)
            {
                case "dogs":
                    view_a_type("dog");
                    break;
                case "cats":
                    view_a_type("cat");
                    break;
                case "birds":
                    view_a_type("birds");
                    break;
                case "quit":
                    run_new_task();
                    break;
                default:
                    run_animal_view();
                    break;
            }

        }
        public void view_a_type(string type)
        {
            string input = userInput.which_animals_to_display();
            switch (input)
            {
                case "adopted":
                    display_all_adopted(type);
                    break;
                case "unadopted":
                    display_all_unadopted(type);
                    break;
                case "all":
                    string path = fileInteractions.get_file_path(type);
                    display_animalFile(path,type);

                    break;
                default:
                    run_new_task();
                    break;
            }
        }
        public void run_new_task()
        {
            string input = userInput.get_new_task();
            switch (input)
            {
                case "other task":
                    run_program();
                    break;
                case "quit":
                    Console.WriteLine("Goodbye.");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
                default:
                    run_new_task();
                    break;
            }
        }

        public void display_cageFile(string path)
        {
            string[] file = fileInteractions.read_file(path);
            foreach (string line in file)
            {
                string[] lineArray = fileInteractions.splitString(line);
                userInput.display_cageFile(lineArray);
            }

        }
        public void display_animalFile(string path, string type)
        {
            List<int> checkList = new List<int>();
            string[] file = fileInteractions.read_file(path);
            foreach (string line in file)
            {
                string[] lineArray = fileInteractions.splitString(line);
                userInput.display_animalFile(lineArray);
                checkList.Add(1);
            }
            if (checkList.Count() == 0)
            {
                userInput.display_none_of_type(type);
            }

        }
        public void display_foodInventoryFile(string path)
        {
            string[] file = fileInteractions.read_file(path);
            foreach (string line in file)
            {
                string[] lineArray = fileInteractions.splitString(line);
                userInput.display_foodInventoryFile(lineArray);

            }

        }
        public void display_bank(string path)
        {
            string bankTotal = fileInteractions.read_text(path);
            userInput.display_bankTotal(bankTotal);
        }
        public void display_all_unadopted(string type)
        {
            List<int> checkList = new List<int>();
            string[] file = fileInteractions.convert_file_to_array(type);
            foreach (string line in file)
            {
                string[] lineArray = fileInteractions.splitString(line);
                if (lineArray[8].Equals("no"))
                {
                    userInput.display_animalFile(lineArray);
                    checkList.Add(1);
                }
            }
            if (checkList.Count() == 0)
            {
                userInput.display_noUnadopted(type);
                run_new_task();
            }
        }
        public void display_all_adopted(string type)
        {
            List<int> checkList = new List<int>();
            string[] file = fileInteractions.convert_file_to_array(type);
            foreach (string line in file)
            {
                string[] lineArray = fileInteractions.splitString(line);
                if (!lineArray[8].Equals("no"))
                {
                    userInput.display_animalFile(lineArray);
                    checkList.Add(1);
                }
            }
            if (checkList.Count() == 0)
            {
                userInput.display_noAdopted(type);
            }
        }
        public void display_adopter(string adopterName)
        {
            string[] file = fileInteractions.convert_file_to_array("adopters");
            foreach (string line in file)
            {
                string[] lineArray = fileInteractions.splitString(line);
                if (lineArray[0] == adopterName)
                {
                    string[] petsSplit = fileInteractions.splitPets(lineArray[1]);
                    string petsListString = String.Join(", ", petsSplit);
                    userInput.display_adopter(lineArray, petsListString);
                }
            }
        }
        public void diplay_all_adopters()
        {
            List<int> checkList = new List<int>();
            string[] file = fileInteractions.convert_file_to_array("adopters");
            foreach (string line in file)
            {
                string[] lineArray = fileInteractions.splitString(line);
                string[] petsSplit = fileInteractions.splitPets(lineArray[1]);
                string petsListString = String.Join(", ", petsSplit);
                userInput.display_adopter(lineArray, petsListString);
                checkList.Add(1);
            }
            if (checkList.Count() == 0)
            {
                userInput.display_none_of_type("adopters");
            }
        }
    }
}
