using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    class FileInteractions
    {
        UserInput userInput;
        
        public FileInteractions()
        {
            userInput = new UserInput();
        }



        public void display_cageFile(string path)
        {
            string[] file = File.ReadAllLines(path);
            foreach (string line in file)
            {
                string[] lineArray = splitString(line);
                userInput.display_cageFile(lineArray);
            }
        
        }
        public void display_animalFile(string path)
        {
            string[] file = File.ReadAllLines(path);
            foreach (string line in file)
            {
                string[] lineArray = splitString(line);
                userInput.display_animalFile(lineArray);

            }

        }
        public void display_foodInventoryFile(string path)
        {
            string[] file = File.ReadAllLines(path);
            foreach (string line in file)
            {
                string[] lineArray = splitString(line);
                userInput.display_foodInventoryFile(lineArray);

            }

        }
        public void display_bank(string path)
        {
            string bankTotal = read_text(path);
            userInput.display_bankTotal(bankTotal);
        }
        public void display_all_unadopted(string type)
        {
            string path = get_file_path(type);
            string[] file = File.ReadAllLines(path);
            foreach(string line in file)
            {
                string[] lineArray = splitString(line);
                if (lineArray[8].Equals("no"))
                {
                    userInput.display_animalFile(lineArray);
                }
            }
        }
        public void display_adopted(string type)
        {
            string path = get_file_path(type);
            string[] file = File.ReadAllLines(path);
            foreach (string line in file)
            {
                string[] lineArray = splitString(line);
                if (!lineArray[8].Equals("no"))
                {
                    userInput.display_animalFile(lineArray);
                }
            }
        }
        public void display_adopter(string adopterName)
        {
            string path = get_file_path("adopters");
            string[] file = File.ReadAllLines(path);
            foreach (string line in file)
            {
                string[] lineArray = splitString(line);
                if (lineArray[0] == adopterName)
                {
                    userInput.display_adopter(lineArray);
                }
            }
        }
        public void diplay_all_adopters()
        {
            string path = get_file_path("adopters");
            string[] file = File.ReadAllLines(path);
            foreach (string line in file)
            {
                string[] lineArray = splitString(line);
                    userInput.display_adopter(lineArray);
            }
        }



        public string get_file_path(string fileName)
        {
            string path = String.Format(@"C: \Users\Joseph A McIlheran\Documents\Visual Studio 2015\Projects\Humane Society\Humane Society\{0}.txt", fileName);
            return path;
        }
        public void create_file(string path)
        {
            File.Create(path).Close();
        }
        public void check_file_exists(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
        }

        public string[] read_file(string path)
        {
            string[] file = File.ReadAllLines(path);
            return file;
        }
        public string read_text(string path)
        {
            string fileText = File.ReadAllText(path);
            return fileText;
        }
        public void append_all_text(string path, string newLine)
        {
            File.AppendAllText(path, newLine);
        }
        public void write_all_text(string path, string newText)
        {
            File.WriteAllText(path, newText);
        }
        public int get_line_count(string path)
        {
            int lineCount = File.ReadLines(path).Count();
            return lineCount;
        }
        public void write_all_lines(string path, string[] file)
        {
            File.WriteAllLines(path, file);
        }


        public string[] convert_file_to_array(string type)
        {
            string path = get_file_path(type);
            check_file_exists(path);
            string[] file = read_file(path);
            return file;
        }
        public string[] splitString(string input)
        {
            string[] inputString = input.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
            return inputString;
        }


    }

}
