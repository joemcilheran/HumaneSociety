using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    class Adoption
    {
        UserInput userInput;
        FileInteractions fileInteractions;
        Shelter shelter;
        public Adoption()
        {
            userInput = new UserInput();
            fileInteractions = new FileInteractions();
            shelter = new Shelter();
        }
        public void run_adoption()
        {
            string answer = userInput.ask_which_to_type_to_adopt();
            switch (answer)
            {
                case "dogs":
                    adopt_by_type("dog");
                    break;
                case "cats":
                    adopt_by_type("cat");
                    break;
                case "birds":
                    adopt_by_type("bird");
                    break;
                default:
                    adopt(answer);
                    break;
            }

        }
        public void display_unadopted_by_breed(string type, string breed)
        {
            {
                List<int> checkList = new List<int>();
                string path = fileInteractions.get_file_path(type);
                string[] file = fileInteractions.read_file(path);
                foreach (string line in file)
                {
                    string[] lineArray = fileInteractions.splitString(line);
                    if (lineArray[8] == "no" && lineArray[2] == breed)
                    {
                        userInput.display_cageFile(lineArray);
                        checkList.Add(1);
                    }
                }
                if (checkList.Count == 0)
                {
                    userInput.display_breed_unavailable_for_adoption(type);
                    shelter.run_new_task();
                }
            }
        }
        public void adopt_by_type(string type)
        {
            shelter.display_all_unadopted(type);
            string input = userInput.ask_which_breed_to_adopt(type);
            switch (input)
            {
                case "breed":
                    string breed = userInput.ask_for_breed();
                    display_unadopted_by_breed(type, breed);
                    string animalName = userInput.ask_name_to_adopt(type, breed);
                    adopt(animalName);
                    break;
                default:
                    adopt(input);
                    break;
            }

        }

        public void adopt(string animalName)
        {
            string answer = userInput.confirm_animal_to_adopt(animalName);
            if (answer == "yes")
            {
                string adopterName = get_adopter(animalName);
                retrieve_animal(animalName, adopterName);
            }
            else
                shelter.run_new_task();
        }
        public string get_adopter(string animalName)
        {
            string adopterName = userInput.get_adopterName();
            string[] adopterFile = fileInteractions.convert_file_to_array("adopters");
            string path = fileInteractions.get_file_path("adopters");
            List<int> checkList = new List<int>();
            foreach (string line in adopterFile)
            {
                int lineIndex = Array.IndexOf(adopterFile, line);
                string[] lineArray = fileInteractions.splitString(line);
                if (lineArray[0] == adopterName)
                {
                    List<string> petsAdopted = lineArray[1].Split('/').ToList();
                    petsAdopted.Add(animalName);
                    Adopter adopter = new Adopter(adopterName, petsAdopted);
                    adopterFile[lineIndex] = (adopter.create_variable_string());
                    fileInteractions.write_all_lines(path, adopterFile);
                    checkList.Add(1);

                }
             }
             if (checkList.Count == 0)
             {
                    List<string> petsAdopted = new List<string>() { animalName };
                    Adopter adopter = new Adopter(adopterName, petsAdopted);
                    string newAdopter = adopter.create_variable_string() + Environment.NewLine;
                    fileInteractions.append_all_text(path, newAdopter);
              }
              return adopterName;
        }

        public void retrieve_animal(string animalName, string adopterName)
        {
            if (search_for_animal("dog", animalName, adopterName) == "false" && search_for_animal("cat", animalName, adopterName) == "false" && search_for_animal("bird", animalName, adopterName) == "false")
            {
                userInput.display_noneAvailableToAdopt();
            }
        }
        public string search_for_animal(string type, string animalName, string adopterName)
        {
            List<int> checkList = new List<int>();
            string[] file = fileInteractions.convert_file_to_array(type);
            string path = fileInteractions.get_file_path(type);
            foreach (string line in file)
            {
                int lineIndex = Array.IndexOf(file, line);
                string[] lineArray = fileInteractions.splitString(line);
                if (lineArray[0] == animalName && lineArray[8] == "no")
                {
                    adopt_animal(type, animalName, lineArray, lineIndex, adopterName);
                    vacate_cage(lineArray, type);
                    checkList.Add(1);
                    shelter.run_new_task();
                }
            }
            if (checkList.Count == 0)
            {

                return "false";
            }
            else
            {
                return "true";
            }

        }
        public void adopt_animal(string type, string animalName, string[] lineArray, int lineIndex, string adopterName)
        {
            recieve_adoption_fee();
            switch (type)
            {
                case "dog":
                    adopt_dog(lineArray, lineIndex, adopterName);
                    break;
                case "cat":
                    adopt_cat(lineArray, lineIndex, adopterName);
                    break;
                default:
                    adopt_bird(lineArray, lineIndex, adopterName);
                    break;
            }
            userInput.confirm_adoption(animalName, adopterName);
        }
        public void adopt_dog(string[] lineArray, int lineIndex, string adopterName)
        {
            bool immunizationsUpToDate = Convert.ToBoolean(lineArray[6]);
            double weight = Convert.ToDouble(lineArray[3]);
            string[] file = fileInteractions.convert_file_to_array("dog");
            DogFactory dogfactory = new DogFactory();
            Animal dog = dogfactory.create_animal(lineArray[2], lineArray[0], weight, immunizationsUpToDate, "NA", adopterName);
            file[lineIndex] = (dog.create_variable_string());
            string path = fileInteractions.get_file_path("dog");
            fileInteractions.write_all_lines(path, file);
        }
        public void adopt_cat(string[] lineArray, int lineIndex, string adopterName)
        {
            bool immunizationsUpToDate = Convert.ToBoolean(lineArray[6]);
            double weight = Convert.ToDouble(lineArray[3]);
            string[] file = fileInteractions.convert_file_to_array("cat");
            CatFactory catfactory = new CatFactory();
            Animal cat = catfactory.create_animal(lineArray[2], lineArray[0], weight, immunizationsUpToDate, "NA", adopterName);
            file[lineIndex] = (cat.create_variable_string());
            string path = fileInteractions.get_file_path("cat");
            fileInteractions.write_all_lines(path, file);
        }
        public void adopt_bird(string[] lineArray, int lineIndex, string adopterName)
        {
            bool immunizationsUpToDate = Convert.ToBoolean(lineArray[6]);
            double weight = Convert.ToDouble(lineArray[3]);
            string[] file = fileInteractions.convert_file_to_array("bird");
            BirdFactory birdfactory = new BirdFactory();
            Animal bird = birdfactory.create_animal(lineArray[2], lineArray[0], weight, immunizationsUpToDate, "NA", adopterName);
            file[lineIndex] = (bird.create_variable_string());
            string path = fileInteractions.get_file_path("bird");
            fileInteractions.write_all_lines(path, file);
        }
        public void recieve_adoption_fee()
        {
            int adoptionFee = 50;
            string path = fileInteractions.get_file_path("bank");
            if (!File.Exists(path))
            {
                fileInteractions.create_file(path);
                Bank bank = new Bank(adoptionFee);
                string bankTotal = bank.create_variable_string();
                fileInteractions.write_all_text(path, bankTotal);

            }
            else
            {
                string bankFile = fileInteractions.read_text(path);
                int oldTotal = Convert.ToInt16(bankFile);
                int newTotal = (adoptionFee + oldTotal);
                Bank bank = new Bank(newTotal);
                string bankTotal = bank.create_variable_string();
                fileInteractions.write_all_text(path, bankTotal);

            }
        }
        public void vacate_cage(string[] lineArray, string type)
        {
            string cageNumber = lineArray[7];
            string cageType = String.Format("{0}Cages", type);
            string[] cageFile = fileInteractions.convert_file_to_array(cageType);
            string path = fileInteractions.get_file_path(cageType);
            foreach (string line in cageFile)
            {
                int lineIndex = Array.IndexOf(cageFile, line);
                string[] cagelLineArray = fileInteractions.splitString(line);
                if (cagelLineArray[1] == cageNumber)
                {
                    switch (type)
                    {
                        case "dog":
                            DogCageFactory dogCageFactory = new DogCageFactory();
                            Cage dogCage = dogCageFactory.create_cage(cageNumber, "none");
                            cageFile[lineIndex] = (dogCage.create_variable_string());
                            fileInteractions.write_all_lines(path, cageFile);
                            break;
                        case "cat":
                            CatCageFactory catCageFactory = new CatCageFactory();
                            Cage catCage = catCageFactory.create_cage(cageNumber, "none");
                            cageFile[lineIndex] = (catCage.create_variable_string());
                            fileInteractions.write_all_lines(path, cageFile);
                            break;
                        default:
                            BirdCageFactory birdCageFactory = new BirdCageFactory();
                            Cage birdCage = birdCageFactory.create_cage(cageNumber, "none");
                            cageFile[lineIndex] = (birdCage.create_variable_string());
                            fileInteractions.write_all_lines(path, cageFile);
                            break;
                    }
                }
            }
        }
    }
}
