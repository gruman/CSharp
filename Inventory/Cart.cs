namespace Cart
{
public class Item
    {
        public List<InventoryItem.Item> items;
        public string location;
        public int currentPos;
        public int login;
        public float discount;

        public Item()
        {
            items = new List<InventoryItem.Item>();
            location = "Canada";
            currentPos = 0;
            login = 0;
            discount = 1;
        }

        public void AddItem(InventoryItem.Item item)
        {
            items.Add(item);
            Console.WriteLine("\nAdded " + item.Name + " to cart.");
        }

        public void RemoveItem(string itemName)
        {
            InventoryItem.Item itemToRemove = null;
            foreach (InventoryItem.Item item in items)
            {
                if (item.Name.ToLower() == itemName.ToLower())
                {
                    itemToRemove = item;
                    break;
                }
            }
            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
                Console.WriteLine("\nRemoved " + itemName + " from cart.");
            }
            else
            {
                Console.WriteLine("\nItem '" + itemName + "' not found in cart.");
            }
        }

        public void ViewCart()
        {
            Console.Clear();
            Console.WriteLine("Cart:");
            Console.WriteLine("-----");

            if (items.Count == 0)
            {
                Console.WriteLine("\nYour cart is empty.");
            }
            else
            {
                decimal totalPrice = 0;
                foreach (InventoryItem.Item item in items)
                {
                    Console.WriteLine(item.Name + " (" + item.Category + ") - " + item.Quantity + " @" + item.Price + " - " + item.Location);
                    totalPrice += item.Price;
                }
                Console.WriteLine("\nTotal price: $" + totalPrice);
            }

            Console.WriteLine("\nPress any key to return to the main menu.");
            Console.ReadKey();
        }
        public decimal Checkout()
        {
            decimal totalPrice = 0;
            foreach (InventoryItem.Item item in items)
            {
                Console.WriteLine(item.Name + " (" + item.Category + ") - " + item.Quantity + " @" + item.Price + " - " + item.Location);
                totalPrice += item.Price;
            }
            return totalPrice;
        }

        public string CountryToggle()
        {
            string[] countries = { "Canada", "USA", "Mexico" };
            currentPos++;
            if (currentPos >= countries.Length)
            {
                currentPos = 0;
            }
            return countries[currentPos];

        }
    }
}