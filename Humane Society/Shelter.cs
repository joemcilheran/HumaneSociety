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
        UserInput userInput;
        FileInteractions fileInteractions;
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
                    run_adoption();
                    break;
                case "check-in":
                    run_checkIn();
                    break;
                case "immunization":
                    run_immunization();
                    break;
                case "create cage":
                    run_cage_creation();
                    break;
                case "check food inventory":
                    run_foodInventory();
                    break;
                case "feed animals":
                    run_feeding();
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
                    fileInteractions.diplay_all_adopters();
                    break;
                default:
                    run_new_task();
                    break;
            }
            run_new_task();
        }



        public void run_adoption()
        {
            string answer = userInput.ask_which_to_type_to_adopt();
            switch(answer)
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
                    run_new_task();
                }
            }
        }
        public void adopt_by_type(string type)
        {
            fileInteractions.display_all_unadopted(type);
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
                run_new_task();
        }
        public string get_adopter(string animalName)
        {
            string adopterName = userInput.get_adopterName();           
            string[] adopterFile = fileInteractions.convert_file_to_array("adopters");
            string path = fileInteractions.get_file_path("adopters");
            if (!adopterFile.Contains(adopterName))
            {
                List<string> petsAdopted = new List<string>() { animalName };
                Adopter adopter = new Adopter(adopterName,petsAdopted);
                string newAdopter = adopter.create_variable_string() + Environment.NewLine;
                fileInteractions.append_all_text(path, newAdopter);
            }
            else
            {
                foreach (string line in adopterFile)
                {
                    int lineIndex = Array.IndexOf(adopterFile, line);
                    string[] lineArray = fileInteractions.splitString(line);
                    if (lineArray[0] == adopterName)
                    {
                        List<string> petsAdopted = lineArray[1].Split(',').ToList();
                        petsAdopted.Add(animalName);
                        Adopter adopter = new Adopter(adopterName, petsAdopted);
                        adopterFile[lineIndex] = (adopter.create_variable_string());
                        fileInteractions.write_all_lines(path, adopterFile);

                    }
                }
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
                    run_new_task();
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
        public void vacate_cage(string[] lineArray, string type)
        {
            string cageNumber = lineArray[7];
            string cageType = String.Format("{0}Cages", type);
            string[] cageFile = fileInteractions.convert_file_to_array(cageType);
            string path = fileInteractions.get_file_path(cageType);
            foreach (string line in cageFile)
            {
                int lineIndex = Array.IndexOf(cageFile, line);
                string [] cagelLineArray = fileInteractions.splitString(line);
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
                run_new_task();
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
                run_new_task();
            }
        }
        public string get_cageNumber(string[] cageFile, string type, string animalName, string path)
        {
            fileInteractions.display_cageFile(path);
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



        public void run_immunization()
        {
            string checkChoice = userInput.get_immunization_check();
            switch(checkChoice)
            {
                case "check all cats":
                    display_unimmunized_animals("cat");
                    break;
                case "check all dogs":
                    display_unimmunized_animals("dog");
                    break;
                case "check all birds":
                    display_unimmunized_animals("bird");
                    break;
                default:
                    input_error_immunizationCheck();
                    break;
            }
           
        }
        public void search_by_name(string type, string animalName)
        {
            List<int> checkList = new List<int>();
            string[] file = fileInteractions.convert_file_to_array(type);
            foreach (string line in file)
            {
                int lineIndex = Array.IndexOf(file, line);
                string[] lineArray = fileInteractions.splitString(line);
                if (lineArray[0] == animalName && lineArray[6] == "False") 
                {
                    immunize_one_animal(type, animalName, lineArray, lineIndex);
                    checkList.Add(1);
                    run_new_task();
                   
                }
                else if (lineArray[0] == animalName && lineArray[6] == "True")
                {
                    userInput.display_alreadyImmunized(type, animalName);
                    run_new_task();
                    
                }
            }
            if (checkList.Count == 0)
            {
                userInput.display_no_animal_by_that_name(type);
                run_new_task();

            }
        }
        public void display_unimmunized_animals(string type)
        {
            List<int> checkList = new List<int>();
            string[] file = fileInteractions.convert_file_to_array(type);
            foreach (string line in file)
            {
                int lineIndex = Array.IndexOf(file, line);
                string[] lineArray = fileInteractions.splitString(line);
                if (lineArray[6] == "False")
                {
                    checkList.Add(1);
                    Console.WriteLine("{0} needs to be immunized.", lineArray[0]);
                }
            }
            if (checkList.Count == 0)
            {
                userInput.display_none_to_be_immunized(type);
                run_new_task();
                
            }
            else
            {
                immunize_type(type);
                run_new_task();
            }
        }
        public void immunize_all_of_type(string type)
        {
            string[] file = fileInteractions.convert_file_to_array(type);
            foreach (string line in file)
            {
                int lineIndex = Array.IndexOf(file, line);
                string[] lineArray = fileInteractions.splitString(line);
                if (lineArray[6] == "False")
                {
                    switch (type)
                    {
                        case "dog":
                            immunize_dog(lineArray, lineIndex);
                            break;
                        case "cat":
                            immunize_cat(lineArray, lineIndex);
                            break;
                        default:
                            immunize_bird(lineArray, lineIndex);
                            break;
                    }
                }
           
            }
            userInput.confirm_immunized_type(type);
        }
        public void immunize_one_animal(string type, string animalName, string[] lineArray, int lineIndex)
        {
            string answer = userInput.ask_to_immunize(type, animalName);
            if (answer == "yes")
            {
                switch (type)
                {
                    case "dog":
                        immunize_dog(lineArray, lineIndex);
                        break;
                    case "cat":
                        immunize_cat(lineArray, lineIndex);
                        break;
                    default:
                        immunize_bird(lineArray, lineIndex);
                        break;
                }
            }
            userInput.confirm_immunized_animal(type, animalName);
            run_new_task();
        }
        public void immunize_dog(string[] lineArray, int lineIndex)
        {
            double weight = Convert.ToDouble(lineArray[3]);
            string[] file = fileInteractions.convert_file_to_array("dog");
            DogFactory dogfactory = new DogFactory();
            Animal dog = dogfactory.create_animal(lineArray[2], lineArray[0], weight, true, lineArray[7], lineArray[8]);
            file[lineIndex] = (dog.create_variable_string());
            string path = fileInteractions.get_file_path("dog");
            fileInteractions.write_all_lines(path, file);
        }
        public void immunize_cat(string[] lineArray, int lineIndex)
        {
            double weight = Convert.ToDouble(lineArray[3]);
            string[] file = fileInteractions.convert_file_to_array("cat");
            CatFactory catfactory = new CatFactory();
            Animal cat = catfactory.create_animal(lineArray[2], lineArray[0], weight, true, lineArray[7], lineArray[8]);
            file[lineIndex] = (cat.create_variable_string());
            string path = fileInteractions.get_file_path("cat");
            fileInteractions.write_all_lines(path, file);
        }
        public void immunize_bird(string[] lineArray, int lineIndex)
        {
            double weight = Convert.ToDouble(lineArray[3]);
            string[] file = fileInteractions.convert_file_to_array("bird");
            BirdFactory birdfactory = new BirdFactory();
            Animal bird = birdfactory.create_animal(lineArray[2], lineArray[0], weight, true, lineArray[7], lineArray[8]);
            file[lineIndex] = (bird.create_variable_string());
            string path = fileInteractions.get_file_path("bird");
            fileInteractions.write_all_lines(path, file);
        }

        public void immunize_type(string type)
        {
            string answer = userInput.ask_which_to_immunize(type);
            if (answer == ("all " + type + "s"))
            {
                immunize_all_of_type(type);
                
            }
            else if (answer == ("one "+ type))
            {
                string animalName = userInput.get_name();
                search_by_name(type, animalName);
               
            }
            else
            { 
                input_error_immunizeType(type);
            }

        }
        public void input_error_immunizeType(string type)
        {
            string answer = userInput.display_input_error();
            switch (answer)
            {
                case "try again":
                    immunize_type(type);
                    break;
                case "something else":
                    run_new_task();
                    break;
                case "quit":
                    Console.WriteLine("Goodbye");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
                default:
                    input_error_immunizeType(type);
                    break;
            }
        }
        public void input_error_immunizationCheck()
        {
            string answer = userInput.display_input_error();
            switch (answer)
            {
                case "try again":
                    run_immunization();
                    break;
                case "something else":
                    run_new_task();
                    break;
                case "quit":
                    Console.WriteLine("Goodbye");
                    Console.ReadKey();
                    Environment.Exit(0);
                    break;
                default:
                    input_error_immunizationCheck();
                    break;
            }
        }










        public void run_cage_creation()
        {
            string input = userInput.get_cage_choice();
            switch(input)
            {
                case "dog cage":
                    make_dogCages(userInput.get_number_of_cages(), fileInteractions.get_file_path("dogCages"));
                    break;
                case "cat cage":
                    make_catCages(userInput.get_number_of_cages(), fileInteractions.get_file_path("catCages"));
                    break;
                case "bird cage":
                    make_birdCages(userInput.get_number_of_cages(), fileInteractions.get_file_path("birdCages"));
                    break;
                default:
                    userInput.display_not_making_cageType();
                    run_new_task();
                    break;
            }
        }
        public void make_dogCages(int number, string path)
        {
            string type = "dog";
            fileInteractions.check_file_exists(path);
            DogCageFactory dogCageFactory = new DogCageFactory();            
            int counter = 0;
            while (counter < number)
            {
                int lineCount = fileInteractions.get_line_count(path);
                int nameAsInt = (lineCount + 1);
                string name = Convert.ToString(nameAsInt);
                string status = "none";
                Cage dogCage = dogCageFactory.create_cage(name, status);
                string newCage = dogCage.create_variable_string() + Environment.NewLine;
                fileInteractions.append_all_text(path, newCage);
                counter = (counter + 1);
            }
            userInput.confirm_cagesCreated(number, type);
            fileInteractions.display_cageFile(path);
        }
        public void make_catCages(int number, string path)
        {
            string type = "cat";
            fileInteractions.check_file_exists(path);
            CatCageFactory catCageFactory = new CatCageFactory();
            int counter = 0;
            while (counter < number)
            {
                int lineCount = fileInteractions.get_line_count(path);
                int nameAsInt = (lineCount + 1);
                string name = Convert.ToString(nameAsInt);
                string status = "none";
                Cage catCage = catCageFactory.create_cage(name, status);
                string newCage = catCage.create_variable_string() + Environment.NewLine;
                fileInteractions.append_all_text(path, newCage);
                counter = (counter + 1);
            }
            userInput.confirm_cagesCreated(number, type);
            fileInteractions.display_cageFile(path);
        }
        public void make_birdCages(int number, string path)
        {
            string type = "bird";
            fileInteractions.check_file_exists(path);
            BirdCageFactory birdCageFactory = new BirdCageFactory();
            int counter = 0;
            while (counter < number)
            {
                int lineCount = fileInteractions.get_line_count(path);
                int nameAsInt = (lineCount + 1);
                string name = Convert.ToString(nameAsInt);
                string status = "none";
                Cage birdCage = birdCageFactory.create_cage(name, status);
                string newCage = birdCage.create_variable_string() + Environment.NewLine;
                fileInteractions.append_all_text(path, newCage);
                counter = (counter + 1);
            }
            userInput.confirm_cagesCreated(number, type);
            fileInteractions.display_cageFile(path);
        }




        public void run_foodInventory()
        {
            string path = create_foodInventory();
            ask_to_buy_food(path);
        }
        public string create_foodInventory()
        {
            string path = check_and_create_foodInventoryFile();
            fileInteractions.display_foodInventoryFile(path);
            return path;

        }
        public string check_and_create_foodInventoryFile()
        {
            string path = fileInteractions.get_file_path("foodInventory");
            if (!File.Exists(path))
            {
                fileInteractions.create_file(path);
                FoodInventory dogFoodInventory = new FoodInventory("dogFood", 100);
                string dogfood = dogFoodInventory.create_variable_string();
                FoodInventory catFoodInventory = new FoodInventory("catFood", 100);
                string catfood = catFoodInventory.create_variable_string();
                FoodInventory birdFoodInventory = new FoodInventory("birdFood", 100);
                string birdfood = birdFoodInventory.create_variable_string();
                string[] inventoryList = new string[3] { dogfood, catfood, birdfood };
                fileInteractions.write_all_lines(path, inventoryList);

            }
            return path;
        }
        public void ask_to_buy_food(string path)
        {
            string answer = userInput.ask_to_buy_more_food();
            switch(answer)
            {
                case "no":
                    run_new_task();
                    break;
                case "yes":
                    buy_food("dogFood", path);
                    buy_food("catFood", path);
                    buy_food("birdFood", path);
                    run_new_task();
                    break;
                default:
                    ask_to_buy_food(path);
                    break;

            }
        }
        public void buy_food(string foodType,string path)
        {
            string input = userInput.ask_to_buy_foodType(foodType);
            switch(input)
            {
                case "yes":
                    string[] file = fileInteractions.convert_file_to_array("foodInventory");
                    foreach (string line in file)
                    {
                        int lineIndex = Array.IndexOf(file, line);
                        string[] lineArray = fileInteractions.splitString(line);
                        int oldAmount = Convert.ToInt16(lineArray[1]);
                        int newAmount = (oldAmount + 100);
                        if (lineArray[0] == foodType)
                        {
                            FoodInventory foodInventory = new FoodInventory(foodType, newAmount);
                            file[lineIndex] = foodInventory.create_variable_string();
                            fileInteractions.write_all_lines(path, file);
                            userInput.display_new_inventoryTotal(foodType, newAmount);
                        }
                    }
                    
                    break;
                case "no":
                    break;
                default:
                    buy_food(foodType, path);
                    break;
            }
        }




        public void run_feeding()
        {
            string input = userInput.ask_who_to_feed();
            switch(input)
            {
                case "dogs":
                    feed_animals("dog");
                    feed_more_types();
                    break;
                case "cats":
                    feed_animals("cat");
                    feed_more_types();
                    break;
                case "birds":
                    feed_animals("bird");
                    feed_more_types();
                    break;
                default:
                    run_new_task();
                    break;
            }
            
        }
        public void feed_more_types()
        {
            string input = userInput.ask_to_feed_more();
            switch(input)
            {
                case "yes":
                    run_feeding();
                    break;
                case "no":
                    run_new_task();
                    break;
                default:
                    feed_more_types();
                    break;

            }
        }
        public void feed_animals(string type)
        {
            int dailyFoodTotal = get_dailyFoodTotal(type);
            check_and_deduct_from_inventory(dailyFoodTotal, type);
        }
        public int get_dailyFoodTotal(string type)
        {
            string[] file = fileInteractions.convert_file_to_array(type);
            List<int> foodPerDayList = new List<int>();
            foreach (string line in file)
            {
                string[] lineArray = fileInteractions.splitString(line);
                int foodPerDay = Convert.ToInt16(lineArray[5]);
                foodPerDayList.Add(foodPerDay);
            }
            int dailyFoodTotal = foodPerDayList.Sum();
            return dailyFoodTotal;
        }
        public void check_and_deduct_from_inventory(int dailyFoodTotal, string type)
        {
            string[] foodInventoryFile = fileInteractions.convert_file_to_array("foodInventory");
            string path = fileInteractions.get_file_path("foodInventory");
            foreach (string inventoryLine in foodInventoryFile)
            {
                string[] inventoryLineArray = fileInteractions.splitString(inventoryLine);
                int amount = Convert.ToInt16(inventoryLineArray[1]);
                if (inventoryLineArray[0] == String.Format("{0}Food", type))
                {
                    if (dailyFoodTotal > amount)
                    {
                        userInput.insufficient_food(type);
                        buy_food(inventoryLineArray[0], path);
                    }
                    else
                    {
                        int lineIndex = Array.IndexOf(foodInventoryFile, inventoryLine);
                        int newAmount = (amount - dailyFoodTotal);
                        FoodInventory foodInventory = new FoodInventory(inventoryLineArray[0], newAmount);
                        foodInventoryFile[lineIndex] = foodInventory.create_variable_string();
                        fileInteractions.write_all_lines(path, foodInventoryFile);
                    }
                }

            }
            userInput.confirm_animals_fed(type);
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
            fileInteractions.display_bank(path);       
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



        public void run_cage_view()
        {
            string input = userInput.which_cages_to_display();
            switch(input)
            {
                case "dog cages":
                    string dogCagePath = fileInteractions.get_file_path("dogCages");
                    fileInteractions.display_cageFile(dogCagePath);
                    break;
                case "cat cages":
                    string catCagePath = fileInteractions.get_file_path("catCages");
                    fileInteractions.display_cageFile(catCagePath);
                    break;
                case "bird cages":
                    string birdCagePath = fileInteractions.get_file_path("birdCages");
                    fileInteractions.display_cageFile(birdCagePath);
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
                    fileInteractions.display_all_adopted(type);
                    break;
                case "unadopted":
                    fileInteractions.display_all_unadopted(type);
                    break;
                case "all":
                    string path = fileInteractions.get_file_path(type);
                    fileInteractions.display_animalFile(path,type);

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
    }
}
