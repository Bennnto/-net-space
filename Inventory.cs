using System.ComponentModel.Design;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.IO;
using System.Text;

namespace Assignment3
{
    public class Game
    { // public card uses to initialized objects such as item no., user rating, price, item name
      // Use public class to accessibility
        public string itemNumber { get; set; }
        public int userRating { get; set; }
        public double price { get; set; }
        public string itemName { get; set; }
        public int stockQuantity { get; set; }
        public Game(string? itemNumber, string? itemName, double price, int userRating, int stockQuantity) // use to order object 
        {
            this.itemNumber = itemNumber;
            this.itemName = itemName;
            this.userRating = userRating;
            this.price = price;
            this.stockQuantity = stockQuantity;
        }
        public override string ToString() // To convert all object that use to store in to .txt file to string 
        {
            return $"{itemNumber},{itemName},{price},{userRating},{stockQuantity}";
        }
    }
    internal class Program
    {
        private static void Main()
        {   // Main use as the main entry point during complier and execution
            Console.WriteLine("|         WELCOME TO INVENTORY SYSTEM          |\n" + breakline);
            Console.WriteLine("|       PLEASE SELECT Y(START) or N(EXIT)      |: ");
            string input = Console.ReadLine(); 
            input = input.ToUpper(); //To flexibility of user can use both upper and lower case all will convert in to Upper case 
            if (input == "Y") // if input = Y or y execute next step to Main Menu 
            {
                MainMenu();
            }
            else if (input == "N") // if inpit = N or n execute next step exit 0 normal exit 
            {
                Environment.Exit(0); 
            }
            else
            {
                Console.WriteLine("|❌ INVALID : invalid input ... Please Try Again|"); // if input not match Y or n return to Main to enter input again 
                Main();
            }
        }
        private static void Menu() 
        {  // this Menu method use to be a sub-menu of Main menu that ask user to continue 
            // in program or exit after user enter 1 of the main menu execution
            Console.WriteLine("Would you like to return to the main menu? (Y/N)");
            string input = Console.ReadLine();
            input = input.ToUpper(); // Convert input to uppercase
            if (input == "Y") // if Y or y will convert to uppercase so if Y execute Main()
            {
                MainMenu();
            }
            else if (input == "N")
            {
                Console.WriteLine("Thank you for using the Inventory System. Goodbye!");
                Environment.Exit(0);
            }
            else
            {   // to handle invalid input
                Console.WriteLine("Invalid input. Please enter Y or N.");
                Menu();
            }
        }
        private static List<string> itNum = new List<string>(); // list to accommodate auto generate item number 
        public static string breakline { get; set; } = "───────────────────────────────────────────────"; 
        private static string GenerateItemNum() 
        { string numBer;
            do // use do... while loop to accommodate and prevent generate duplicate numbers check in list (itNum)
            {
                Random Numb = new Random();
                numBer = Numb.Next(1, 10000).ToString("D4"); // Random number from 1,10000 but all number will show as 0000 D4 and store as string 
            } while (itNum.Contains(numBer)); 
            itNum.Add(numBer);
            return numBer;
        }
        private static void AddProduct()
        {                                    
            Console.WriteLine(breakline + "\n|                 ADD NEW ITEM                |\n" + breakline);
            Console.WriteLine("This function to add Items\n" + "existing Item number/Auto Generate Item Number for New Items\n");

            //Ask for input from "user in each variable
            string itemNumber = "";
            string chkItemnum = @"^\d{4}$"; // Use Regular expression to check format of item number 4 digit 
            Regex ChkNumb = new Regex(chkItemnum);
            Console.WriteLine("🔢 Do you have an Item Number Answer as a number (0 (No), 1(Yes)) :");
            string Ans = Console.ReadLine();
            if (Ans == "1")
            {
                Console.WriteLine("Please Enter Item Number 4 digits : ");
                string ItemNumber = Console.ReadLine();
                if (Regex.IsMatch(ItemNumber, chkItemnum))
                {
                    itemNumber = ItemNumber;
                    Console.WriteLine("Your Item Number input validated");
                }
                else {
                    Console.WriteLine("|❌ INVALID : invalid input ... Please Try Again|");
                    itemNumber = GenerateItemNum();
                }
            }
            if (itemNumber == "")
            {
                itemNumber = GenerateItemNum();
            }
            Console.WriteLine("✅ New Item code is :" + itemNumber); 
            Console.WriteLine("🎮 Please Enter Product Name:"); // ask user enter item name
            var itemName = Console.ReadLine();
            Console.WriteLine("New Item Name is :" + itemName);

            //-------------------------------------------------------------
            int userRating; //use do while loop to validate input 0-5
            do
            {
                Console.WriteLine("📈 Please Enter Product Rating 0-5: "); // ask user enter rating 
                userRating = int.Parse(Console.ReadLine());
                if (userRating < 0 || userRating > 5) // check rating format 0-5 single digit store as int
                {
                    Console.WriteLine("|❌ INVALID : invalid rating input ... please try again|");
                }
            } while (userRating < 0 || userRating > 5);
            Console.WriteLine("Item Rating is :" + userRating);
            Console.WriteLine("Please Enter Item Price : ");
            var price = double.Parse(Console.ReadLine()); // ask user enter price and read price as double mean .2 decimal place 
            Console.WriteLine("Item Price is :" + price + "💰");
            var stockQuantity = 0;
            Console.WriteLine("Please Enter Item Quantity : ");
            stockQuantity = int.Parse(Console.ReadLine());  
            Console.WriteLine("Item Quantity is :" + stockQuantity + "📦");
            var newGame = new Game(itemNumber, itemName, price, userRating, stockQuantity); // create new object game and rearrange all element 
            Console.WriteLine("\n| ✅ ADD > NEW ITEM 🎮 : new item added to the inventory... >\n" + newGame);
            string Line = newGame.ToString(); // use public class method change all variable in game to string before store to .txt file 
            string filepath = "videogames.txt"; // initialized filepath to videogames.txt 
            using (StreamWriter writer = new StreamWriter(filepath, append: true)) // use stream writer
                // append : true mean not override the previous information append + new line 
            {
                writer.WriteLine(Line); //write game object to videogames.txt
            }
        }
        public static void FindItemNumber()
        {                                    
            Console.WriteLine(breakline +"\n|                 SEARCH ITEMS                 |\n" + breakline + "\nThis page use Item Number As Indicator"
            + "\nItem Number Characteristic is 4 digits [0-9]{4}$\n" + breakline); // indicate format for user before input item number 
            Console.WriteLine("This function use Item Number (XXXX) for reference");
            Console.WriteLine("Please Enter Item Number 4 digits :");
            var NumberRef = Console.ReadLine();
            Console.WriteLine($"| REFERENCE > 📄 ITEM NUMBER REFERENCE : your reference item number is : {NumberRef} |");
            Console.WriteLine("\n| SEARCH ITEM > ⏱ HOLD ON : system retrieving data ... |")
            string filepath = "videogames.txt"; // initialized filepath to videogames.txt
            bool found = false; // found false to open loop and will set it true after found match 
            try
            {
                using StreamReader reader = new StreamReader(filepath);
                string lines;
                while ((lines = reader.ReadLine()) != null) // read line != read only line that have information
                {
                    if (lines.StartsWith(NumberRef + ",")) // search line that start with number ref follow with "," delimeter)
                    {
                        found = true; // found match set found = true
                        Console.WriteLine(breakline + $"\n| ITEM > {lines} |"); // display line matched 
                        break;
                    }
                }
                if (!found) // found = false
                {
                    Console.WriteLine("\n|❌ INVALID : not found item matched ... reference|");
                }
            }
            catch (Exception ex) //catch to display error message ex
            {
                Console.WriteLine("Error Catched: " + ex.Message);
            }
        }
        public static void Listprice()
        {
            Console.WriteLine(breakline + "\n|                 List Inventory Price                 |\n" + breakline + "\nInstruction\n" +
            "When execute this function will lists all items in the inventory \nthat price below or equal to preference Price preference price format XX.XX");
            Console.WriteLine("Please Enter your preference price: ");

            if (double.TryParse(Console.ReadLine(), out double prefprice)) // this use try parse to check input validity
            {
                Console.WriteLine($"\n| REFERENCE > 📄 PRICE REFERENCE : your reference price is : {prefprice} |");
            }
            else
            {
                Console.WriteLine("\n|❌ INVALID : invalid input ... Please Try Again|");
                return;
            }
            Console.WriteLine("\n| LIST PRICE > ⏱ HOLD ON : system retrieving data ... |")
            bool found = false;
            string filepath = "videogames.txt"; //initialized filepath to videogames.txt
            using StreamReader reader1 = new StreamReader(filepath);
            string line; //initialized line as string 
            while ((line = reader1.ReadLine()) != null) //line = 1 line in txt file because we store 1 item as 1 line 
            {
                string[] part = line.Split(",");  //set line data as array seperate with , delimeter 
                if (part.Length >= 4 && double.TryParse(part[2], out double price)) // inditcate price that find match with preference price as array[2]
                {
                    if (price <= prefprice) // show and list all the item price below or equal to pref price
                    {
                        Console.WriteLine(breakline + $"\n| ITEM > {line} |");
                        found = true;
                    }
                }
            }
            if(!found)
            {
                Console.WriteLine("\n|❌ INVALID : not found item matched ... reference|");
            }
            Menu();
        }
        private static void Average()
        {
            string filepath = "videogames.txt"; //intitalized file path to videogames.txt
            using StreamReader reader2 = new StreamReader(filepath);
            string line;
            double Acc = 0; // set accumulator for total price
            double average; // initialized average variable 
            int i = 0; // set i to find average 
            while ((line = reader2.ReadLine()) != null)
            {
                string[] part = line.Split(",");
                if (part.Length >= 4 && double.TryParse(part[2], out double price))
                {
                    Acc += price; // acc = acc + price 
                    i++; // I + 1 in each iteration
                }
            }
            if (i > 0)
            {
                average = Acc / i; //find average = accumulate / amount of data / item in list
                Console.WriteLine(breakline + $"\n| AVERAGE PRICE > The Average Inventory Price is : {average} |");
            } else
            {
                Console.WriteLine("\n|❌ INVALID : not found item matched ... reference|");
            }
            
        }
        private static void Range()
        {
            string filepath = "videogames.txt";
            double Highest = double.MinValue; // Initialize to lowest possible value to compare with all value that possible so it will substitute with all high price until highest
            double Lowest = double.MaxValue;  // Initialize to highest possible value to compare and substitute with lowest value in list
            string Highline = "";
            string Lowline = "";
            
            using StreamReader reader3 = new StreamReader(filepath);
            string line;
            
            while ((line = reader3.ReadLine()) != null)
            {
                string[] part = line.Split(",");
                if (part.Length >= 4 && double.TryParse(part[2], out double price))
                {
                    if (price > Highest) // price now tracks the highest price
                    {
                        Highest = price;
                        Highline = line;  // Fixed variable assignment to show in result of this method
                    }
                    if (price < Lowest)  // Changed to if instead of else if
                    {
                        Lowest = price;
                        Lowline = line;   // Fixed variable assignment to show in result of this method
                    }
                }
            }

            // Moved outside the loop and added validation
            if (Highest != double.MinValue && Lowest != double.MaxValue)
            {
                Console.WriteLine(breakline + "\n| PRICE RANGE > The Inventory Price Range |\n" +
                $"| Highest price in the inventory is : {Highline} |\n" +
                $"| Lowest price in the inventory is : {Lowline} |");
            }
            else
            {
                Console.WriteLine("|❌ INVALID : No items found in inventory|");
            }
        }
        private static void Analyst()
        { // use same logic as Menu and Main Menu for this method 
            Console.WriteLine(breakline + "\n|                INVENTORY ANALYST              |\n" + breakline + "Please Select Function to start Analyse\n" +
            "A. Average Inventory Price\n" +
            "B. Inventory Price Range Lowest - Highest Price");
            Console.WriteLine("Please Select between A or B : ");
            string Input = Console.ReadLine();
            Input = Input.ToUpper();
            switch (Input)
            {
                case "A":
                    Console.WriteLine("| AVERAGE PRICE > ⏱ HOLD ON : System Analyze ... Please Wait |");
                    Average();
                    Menu();
                    break;
                case "B":
                    Console.WriteLine("| PRICE RANGE > ⏱ HOLD ON : System Analyze ... Please Wait |");
                    Range();
                    Menu();
                    break;
                default:
                    Console.WriteLine("|❌ INVALID : invalid input ... Please Try Again|");
                    Analyst();
                    break;
            }
        }

        private static void MainMenu()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("───────────────────────────────────────────────");
            Console.WriteLine("|                 MAIN MENU                    |\n" +
                              "|                Directories                   |\n" +
                              "|           1➕Add Product                    |\n" +
                              "|           2🔎Search Inventory               |\n" +
                              "|           3👀Product Lookup                 |\n" +
                              "|           4📊Product Analysis               |\n" + breakline);
            Console.WriteLine("| Please Enter 1-5 to Navigate to Function Page : |");
            string inPut = Console.ReadLine();
            Console.WriteLine($"You Entered :  + {inPut}");
            switch (inPut)
            {
                case "1":
                    AddProduct();
                    Menu();
                    break;
                case "2":
                    FindItemNumber();
                    Menu();
                    break;
                case "3":
                    Listprice();
                    Menu();
                    break;
                case "4":
                    Analyst();
                    Menu();
                    break;
                default:
                    Console.WriteLine("|❌ INVALID : invalid input ... Please Try Again|");
                    Main();
                    break;
            }




        }

    }
}
