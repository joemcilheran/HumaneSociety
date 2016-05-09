using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    class CheckIn
    {
        UserInput userInput;
        FileInteractions fileInteractions;
        Shelter shelter;
        public CheckIn()
        {
            userInput = new UserInput();
            fileInteractions = new FileInteractions();
            shelter = new Shelter();
        }
        public void run_checkIn()
        {
            string type = userInput.get_type();
            check_animal_type(type);
            string animalName = userInput.get_name();
            string cageNumber = cage_check(type, animalName);
            string breed = userInput.get_breed();
            double weight = userInput.get_weight();
            bool immunizationsUpToDate = userInput.check_immunization();
            string adopted = "no";
            create_animal(type, breed, animalName, weight, immunizationsUpToDate, cageNumber, adopted);

        }
        public void check_animal_type(string type)
        {
            if (type != "dog" && type != "cat" && type != "bird")
            {
                userInput.display_NotAcceptingType();
                shelter.run_new_task();
            }
        }
        public string cage_check(string type, string animalName)
        {
            string cageType = String.Format("{0}Cages", type);
            string cagePath = fileInteractions.get_file_path(cageType);
            fileInteractions.check_file_exists(cagePath);
            string[] cageFile = fileInteractions.read_file(cagePath);
            check_for_cage(cageFile);
            string cageNumber = get_cageNumber(cageFile, type, animalName, cagePath);
            return cageNumber;
        }
        public void check_for_cage(string[] cageFile)
        {
            List<int> vacancyList = new List<int>();
            foreach (string line in cageFile)
            {
                string[] lineArray = fileInteractions.splitString(line);
                if (lineArray.Contains("none"))
                {
                    vacancyList.Add(1);
                }
            }
            if (vacancyList.Count == 0)
            {
                userInput.display_unavailibily();
                shelter.run_new_task();
            }
        }
        public string get_cageNumber(string[] cageFile, string type, string animalName, string path)
        {
            shelter.display_cageFile(path);
            string cageNumber = userInput.get_cageNumber_choice();
            foreach (string line in cageFile)
            {
                int lineIndex = Array.IndexOf(cageFile, line);
                string[] lineArray = fileInteractions.splitString(line);
                if (lineArray.Contains(cageNumber) && lineArray.Contains("none"))
                {
                    switch (type)
                    {
                        case "dog":
                            DogCageFactory dogCageFactory = new DogCageFactory();
                            Cage dogCage = dogCageFactory.create_cage(cageNumber, animalName);
                            cageFile[lineIndex] = (dogCage.create_variable_string());
                            fileInteractions.write_all_lines(path, cageFile);
                            break;
                        case "cat":
                            CatCageFactory catCageFactory = new CatCageFactory();
                            Cage catCage = catCageFactory.create_cage(cageNumber, animalName);
                            cageFile[lineIndex] = (catCage.create_variable_string());
                            fileInteractions.write_all_lines(path, cageFile);
                            break;
                        default:
                            BirdCageFactory birdCageFactory = new BirdCageFactory();
                            Cage birdCage = birdCageFactory.create_cage(cageNumber, animalName);
                            cageFile[lineIndex] = (birdCage.create_variable_string());
                            fileInteractions.write_all_lines(path, cageFile);
                            break;
                    }
                }
            }
            return cageNumber;
        }
        public void create_animal(string Type, string Breed, string Name, double Weight, bool ImmunizationsUpToDate, string CageNumber, string Adopted)
        {
            string path = fileInteractions.get_file_path(Type);
            switch (Type)
            {

                case "dog":
                    DogFactory dogfactory = new DogFactory();
                    Animal dog = dogfactory.create_animal(Breed, Name, Weight, ImmunizationsUpToDate, CageNumber, Adopted);
                    string newDog = dog.create_variable_string() + Environment.NewLine;
                    fileInteractions.append_all_text(path, newDog);
                    break;
                case "cat":
                    CatFactory catfactory = new CatFactory();
                    Animal cat = catfactory.create_animal(Breed, Name, Weight, ImmunizationsUpToDate, CageNumber, Adopted);
                    string newCat = cat.create_variable_string() + Environment.NewLine;
                    fileInteractions.append_all_text(path, newCat);
                    break;
                default:
                    BirdFactory birdfactory = new BirdFactory();
                    Animal bird = birdfactory.create_animal(Breed, Name, Weight, ImmunizationsUpToDate, CageNumber, Adopted);
                    string newBird = bird.create_variable_string() + Environment.NewLine;
                    fileInteractions.append_all_text(path, newBird);
                    break;

            }
            Console.WriteLine("{0} '{1}' checked in!", Type, Name);
        }
    }
}
