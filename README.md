## .Net PlayGround 🛜
# Purpose 
  - to practice and experiment in .net project on with codespace devcontainer
# Project Lists
  - 1️⃣Console Inventory App💴

# Console Inventory App
  - This app contain with 4 sub method
    - Add Item to inventory : this include generate item no. in 4 digits 0️⃣0️⃣0️⃣1️⃣ ➡️ 9️⃣9️⃣9️⃣9️⃣ format + Save data into .txt file in string line format
    - Search Product : this search function use item no. 4 digits reference and read stored data in .txt file
    - List Product price : this function use reference price and list all the price below to equal the reference price read data from .txt file
    - Inventory Analyst : this function seperate into 2 sub-function
      - Average Inventory Price : Calculate average price of all product in an inventory <.txt file>
      - Inventory Price Range : Display the lowest and Highest Price in an inventory
         -use double.MinValue for Highest price to compare with all price and substitute with highest price in file
         -use double.MaxValue for Lowest price to compare with all lower than Max value and substitute with lowest price in file

  - Usage and Amendment Possibility 
    - File that use to store data can be change location and also file name
      - change in var filepath < if you use path should use filepath = path.combine(YOURPATH)
    - ITEM NO. can change format
      - change in auto generate logic
      - change in validate loop in Add Item Product < use Regex to check >      
    - Change Foreground and Background color 
      - use Console.Background | Console.Foreground
---------------------------------------̶---------------------̶-----------------------------------------------------------------------------------------------------