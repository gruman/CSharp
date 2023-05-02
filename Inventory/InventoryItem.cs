namespace InventoryItem
{
public class Item
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Item(string name, string category, string location, decimal price, int quantity)
        {
            Name = name;
            Category = category;
            Location = location;
            Price = price;
            Quantity = quantity;
        }
        public decimal GetDiscountedPrice(Cart.Item cart)
        {
            decimal discountPercentage = 0m;

            // Check the country to determine the discount
            if (cart.location == Location)
            {
                switch (Location)
                {
                    case "USA":
                        discountPercentage = 0.1m;
                        break;

                    case "Canada":
                        discountPercentage = 0.15m;
                        break;

                    default:
                        discountPercentage = 0m;
                        break;
                }
            }


            // Calculate the discounted price
            decimal discountedPrice = Price * (1 - discountPercentage);
            return discountedPrice;
        }
    }
}