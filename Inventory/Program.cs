using System;
using System.Linq;
using System.IO;
using Cart;

namespace InventoryProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a list to store items
            List<InventoryItem.Item> inventory = new List<InventoryItem.Item>();

            // Get items from CSV
            using (var reader = new StreamReader("inventory.csv"))
            {
                // Skip header row
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    // Parse values from CSV and create new inventory item
                    string name = values[0];
                    string category = values[1];
                    string location = values[2];
                    decimal price = decimal.Parse(values[3]); // value coming in as text so parse to decimal
                    int quantity = int.Parse(values[4]);
                    // add to the inventory list
                    inventory.Add(new InventoryItem.Item(name, category, location, price, quantity));

                }
            }
            // Show the main menu, passing the inventory object
            ShowMenu(inventory);
        }

        static void ShowMenu(List<InventoryItem.Item> inventory)
        { // shows the main menu

            // set up the cart which also stores login data
            Cart.Item cart = new Cart.Item();
            cart.login = 1;

            while (true)
            { // keep it on loop
                Console.Clear();
                if (cart.login == 1)
                { // logged in
                    Console.WriteLine("0. Logout");
                }
                else
                { // logged out
                    Console.WriteLine("0. Login");
                }
                Console.WriteLine("1. Location: " + cart.location);
                Console.WriteLine("2. View Inventory");
                Console.WriteLine("3. Search Inventory");
                Console.WriteLine("4. Add to Cart");
                Console.WriteLine("5. Remove from Cart");
                Console.WriteLine($"6. View Cart (" + cart.items.Count + ")");
                Console.WriteLine("7. Checkout");
                if (cart.login == 1)
                { // logged in, show private routes

                    Console.WriteLine("8. Add Item To Inventory");
                    Console.WriteLine("9. Remove Item From Inventory");
                    Console.WriteLine("10. Add discount code");
                }
                if (cart.discount > 1)
                {
                    Console.WriteLine("11. Discount: " + cart.discount + "%");
                }
                else
                {
                    Console.WriteLine("11. Enter discount code");
                }
                Console.WriteLine("12. Exit");
                Console.Write("Enter your choice: ");

                // Get the user's choice
                int choice;
                string input = Console.ReadLine();
                // chrvk if input is a number and apply it to choice
                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }
                if (int.TryParse(input, out choice))
                {
                    switch (choice)
                    {
                        case 0:
                            if (cart.login == 0)
                            { // not logged in
                              // password page to login
                                Console.Clear();
                                Console.WriteLine("Enter password (abcd)");
                                string pw = Console.ReadLine();
                                if (pw == "abcd")
                                {
                                    cart.login = 1; // login
                                }
                            }
                            else
                            {
                                cart.login = 0; // logout
                            }
                            break;
                        case 1:
                            cart.location = cart.CountryToggle();
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine("1. Sort by name");
                            Console.WriteLine("2. Sort by location");
                            Console.WriteLine("3. Sort by price");
                            int i = int.Parse(Console.ReadLine());
                            ViewInventory(inventory, cart, i);
                            break;
                        case 3:
                            SearchInventory(inventory);
                            break;
                        case 4:
                            Console.Write("Enter the name of the item to add: ");
                            string itemName = Console.ReadLine();
                            InventoryItem.Item itemToAdd = inventory.Find(item => item.Name.ToLower() == itemName.ToLower());
                            if (itemToAdd != null)
                            {
                                cart.AddItem(itemToAdd);
                            }
                            else
                            {
                                Console.WriteLine("\nItem '" + itemName + "' not found in inventory.");
                            }
                            break;
                        case 5:
                            // Remove item from cart
                            Console.Write("Enter the name of the item to remove: ");
                            string itemToRemove = Console.ReadLine();
                            cart.RemoveItem(itemToRemove);
                            break;

                        case 6:
                            cart.ViewCart();
                            break;
                        case 7:
                            Console.WriteLine("Are you sure you want to checkout? (y/n)");
                            string confirm = Console.ReadLine();
                            if (confirm.ToLower() == "y")
                            {
                                decimal total = cart.Checkout();
                                Console.WriteLine("Your total is $" + total + ". Thank you for your purchase!");
                                Console.ReadKey();
                                return;
                            }
                            break;

                        case 8:
                            if (cart.login == 1)
                            {
                                AddItem(inventory);
                            }
                            break;

                        case 9:
                            // Remove item
                            if (cart.login == 1)
                            {
                                RemoveItem(inventory);
                            }
                            break;
                        case 10:
                            if (cart.login == 1)
                            {
                                AddDiscount(cart);
                            }
                            break;
                        case 11:
                                EnterDiscount(cart);
                            break;
                        case 12:
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Press any key to try again.");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }
            }
        }

        static void ViewInventory(List<InventoryItem.Item> inventory, Cart.Item cart, int sort)
        {
            Console.Clear();
            Console.WriteLine("Inventory:");
            Console.WriteLine("----------");
            List<InventoryItem.Item> sortedList = inventory;
            switch (sort)
            {
                case 1:
                    sortedList = inventory.OrderBy(x => x.Name).ToList();
                    break;
                case 2:
                    sortedList = inventory.OrderBy(x => x.Location).ToList();
                    break;
                case 3:
                    sortedList = inventory.OrderBy(x => x.Price).ToList();
                    break;
            }

            foreach (InventoryItem.Item item in sortedList)
            {
                Console.WriteLine(item.Name + " (" + item.Category + ") - " + item.Quantity + " @" + item.Price.ToString("F2") + " - " + item.Location + " (Price for " + cart.location + " $" + item.GetDiscountedPrice(cart).ToString("F2") + ")");
            }

            Console.WriteLine("\nPress any key to return to the main menu.");
            Console.ReadKey();
        }

        static void AddItem(List<InventoryItem.Item> inventory)
        {
            Console.Clear();
            Console.WriteLine("Add Item:");
            Console.WriteLine("---------");

            // Get the item details from the user
            Console.Write("Name (str): ");
            string name = Console.ReadLine();
            Console.Write("Category (str): ");
            string category = Console.ReadLine();
            Console.Write("Location (str): ");
            string location = Console.ReadLine();
            Console.Write("Price (dec): ");
            decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Quantity (int): ");
            int quantity = int.Parse(Console.ReadLine());

            // Create a new item and add it to the inventory
            inventory.Add(new InventoryItem.Item(name, category, location, price, quantity));

            // Save the new blog to a CSV file
            string fileName = "inventory.csv";

            if (!File.Exists(fileName))
            {
                // Create a new CSV file and write the header row
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine("Name,Category,Location,Price,Quantity");
                }
            }

            // Append the new blog to the CSV file
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine($"{name},{category},{location},{price},{quantity}");
            }

            Console.WriteLine("\nItem added successfully. Press any key to return to the main menu.");
            Console.ReadKey();
        }

        static void RemoveItem(List<InventoryItem.Item> inventory)
        {
            Console.Clear();
            Console.WriteLine("Remove Item:");
            Console.WriteLine("------------");

            // Get the name of the item to remove from the user
            Console.Write("Name: ");
            string name = Console.ReadLine();

            // Find the item in the inventory and remove it
            InventoryItem.Item itemToRemove = null;
            foreach (InventoryItem.Item item in inventory)
            {
                if (item.Name.ToLower() == name.ToLower())
                {
                    itemToRemove = item;
                    break;
                }
            }
            if (itemToRemove != null)
            {
                inventory.Remove(itemToRemove);
                Console.WriteLine("\nItem removed successfully. Press any key to return to the main menu.");
            }
            else
            {
                Console.WriteLine("\nItem '" + name + "' not found. Press any key to return to the main menu.");
            }

            Console.ReadKey();
        }

        static void AddDiscount(Cart.Item cart)
        {
            Console.Clear();
            Console.WriteLine("Enter a code");
            string code = Console.ReadLine();
            string filePath = "coupons.txt";
            using (StreamWriter writer = File.AppendText(filePath))
            {
               writer.WriteLine(code);
            }
                Console.WriteLine("Code Added.");
                Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        static void EnterDiscount(Cart.Item cart)
        {
            Console.Clear();
            Console.WriteLine("Enter a code");
            string code = Console.ReadLine();
            int check = 0;
            string filePath = "coupons.txt";

          // Read the lines of the file into an array
            string[] lines = File.ReadAllLines(filePath);

            // Display the lines in the console
            foreach (string line in lines)
            {
                if (code == line)
                { // found a code
                    check++;
                }
            }
            if (check > 0)
            {
                cart.discount = 15f;
                Console.WriteLine("Success! You have a 15% dicount on the entire store");
            }
            else
            {
                Console.WriteLine("Coupon code not found.");
            }
                Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        static void SearchInventory(List<InventoryItem.Item> inventory)
        {
            Console.Clear();
            Console.WriteLine("Search Inventory:");
            Console.WriteLine("-----------------");

            // Get the search query from the user
            Console.Write("Search Query: ");
            string query = Console.ReadLine();

            // Search the inventory for items matching the query
            List<InventoryItem.Item> searchResults = new List<InventoryItem.Item>();
            foreach (InventoryItem.Item item in inventory)
            {
                if (item.Name.ToLower().Contains(query.ToLower()) ||
                    item.Category.ToLower().Contains(query.ToLower()) ||
                    item.Location.ToLower().Contains(query.ToLower()))
                {
                    searchResults.Add(item);
                }
            }

            if (searchResults.Count > 0)
            {
                Console.WriteLine("\nSearch Results (" + searchResults.Count + ")");
                Console.WriteLine("--------------------------------------");
                foreach (InventoryItem.Item item in searchResults)
                {
                    Console.WriteLine(item.Name + " (" + item.Category + ") - " + item.Quantity + " @" + item.Price + " - " + item.Location);
                }
            }
            else
            {
                Console.WriteLine("\nNo items found matching '" + query + "'.");
            }

            Console.WriteLine("\nPress any key to return to the main menu.");
            Console.ReadKey();
        }
    }

    
}