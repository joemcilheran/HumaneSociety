using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    class Immunization
    {
        UserInput userInput;
        FileInteractions fileInteractions;
        Shelter shelter;
        public Immunization()
        {
            userInput = new UserInput();
            fileInteractions = new FileInteractions();
            shelter = new Shelter();

        }
        public void run_immunization()
        {
            string checkChoice = userInput.get_immunization_check();
            switch (checkChoice)
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
                    shelter.run_new_task();

                }
                else if (lineArray[0] == animalName && lineArray[6] == "True")
                {
                    userInput.display_alreadyImmunized(type, animalName);
                    shelter.run_new_task();

                }
            }
            if (checkList.Count == 0)
            {
                userInput.display_no_animal_by_that_name(type);
                shelter.run_new_task();

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
                shelter.run_new_task();

            }
            else
            {
                immunize_type(type);
                shelter.run_new_task();
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
            shelter.run_new_task();
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
            else if (answer == ("one " + type))
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
                    shelter.run_new_task();
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
                    shelter.run_new_task();
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
    }
}
