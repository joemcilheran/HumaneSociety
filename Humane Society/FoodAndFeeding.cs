using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Humane_Society
{
    class FoodAndFeeding
    {
        UserInput userInput;
        FileInteractions fileInteractions;
        Shelter shelter;
        public FoodAndFeeding()
        {
            userInput = new UserInput();
            fileInteractions = new FileInteractions();
            shelter = new Shelter();

        }
        public void run_foodInventory()
        {
            string path = create_foodInventory();
            ask_to_buy_food(path);
        }
        public string create_foodInventory()
        {
            string path = check_and_create_foodInventoryFile();
            shelter.display_foodInventoryFile(path);
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
            switch (answer)
            {
                case "no":
                    shelter.run_new_task();
                    break;
                case "yes":
                    buy_food("dogFood", path);
                    buy_food("catFood", path);
                    buy_food("birdFood", path);
                    shelter.run_new_task();
                    break;
                default:
                    ask_to_buy_food(path);
                    break;

            }
        }
        public void buy_food(string foodType, string path)
        {
            string input = userInput.ask_to_buy_foodType(foodType);
            switch (input)
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
            switch (input)
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
                    shelter.run_new_task();
                    break;
            }

        }
        public void feed_more_types()
        {
            string input = userInput.ask_to_feed_more();
            switch (input)
            {
                case "yes":
                    run_feeding();
                    break;
                case "no":
                    shelter.run_new_task();
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
    }
}
