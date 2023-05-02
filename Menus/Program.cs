using System;

namespace MultiMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu(FillMenus());
        }
        
        static void MainMenu(Menus.Menu[] menus)
        {
            Page test = new Page(1, "Title", "Body", new DateTime(2023, 5, 1));


            bool active = true;
            int current = 0; // current menu. Default to home
            while (active)
            {
                Console.Clear();
                
                Console.WriteLine($"Page ID: #{test.ID}");
                Console.WriteLine($"{test.Title}");
                Console.WriteLine($"{test.Content}");
                Console.WriteLine($"Updated: {test.Date}");
                for (int i = 0; i < menus[current].MenuItems.Length; i++)
                {   // print out each line in the current menu
                    Console.WriteLine(menus[current].MenuItems[i].Num + ". " + menus[current].MenuItems[i].Title);
                }
                // check for user input, filtering out blanks and making sure the input is numerical
                int choice;
                string input = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }
                if (int.TryParse(input, out choice))
                {
                    if (choice <= menus[current].MenuItems.Length && choice > 0)
                    { // change the menu based on user input and check it it's above 0 and less than the length of the menu array
                        current = choice - 1;
                    }
                }
            }
        }
        
        static Menus.Menu[] FillMenus()
        {
            
            Menus.MenuItem m1i1 = new Menus.MenuItem(1, "Home", 0);
            Menus.MenuItem m1i2 = new Menus.MenuItem(2, "Go to Menu 2", 0);
            Menus.MenuItem m1i3 = new Menus.MenuItem(3, "Go to Menu 3", 0);

            Menus.MenuItem m2i1 = new Menus.MenuItem(1, "Option 1", 0);
            Menus.MenuItem m2i2 = new Menus.MenuItem(2, "Option 2", 0);
            Menus.MenuItem m2i3 = new Menus.MenuItem(3, "Option 3", 0);

            Menus.MenuItem m3i1 = new Menus.MenuItem(1, "Setting 1", 0);
            Menus.MenuItem m3i2 = new Menus.MenuItem(2, "Setting 2", 0);
            Menus.MenuItem m3i3 = new Menus.MenuItem(3, "Setting 3", 0);

            Menus.MenuItem[] menu1Items = { m1i1, m1i2, m1i3 };
            Menus.MenuItem[] menu2Items = { m2i1, m2i2, m2i3 };
            Menus.MenuItem[] menu3Items = { m3i1, m3i2, m3i3 };

            Menus.Menu menu1 = new Menus.Menu(menu1Items);
            Menus.Menu menu2 = new Menus.Menu(menu2Items);
            Menus.Menu menu3 = new Menus.Menu(menu3Items);

            Menus.Menu[] menus = { menu1, menu2, menu3 };

            return menus;
        } 
    }
    public class Page
    {
        public int ID; // num to display and fire
        public string Title; // text
        public string Content; // text
        public DateTime Date;
        public Page(int id, string title, string content, DateTime date)
        {
            ID = id;
            Title = title;
            Content = content;
            Date = date;
        }
    }
    
}
