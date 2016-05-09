using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    class CageCreation
    {
        UserInput userInput;
        FileInteractions fileInteractions;
        Shelter shelter;
        public CageCreation()
        {
            userInput = new UserInput();
            fileInteractions = new FileInteractions();
            shelter = new Shelter();

        }
        public void run_cage_creation()
        {
            string input = userInput.get_cage_choice();
            switch (input)
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
                    shelter.run_new_task();
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
            shelter.display_cageFile(path);
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
            shelter.display_cageFile(path);
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
            shelter.display_cageFile(path);
        }
    }
}
