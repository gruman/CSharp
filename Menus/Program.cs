using System;

namespace MultiMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            // setup menus
            MenuItem m1i1 = new MenuItem(1, "Home");
            MenuItem m1i2 = new MenuItem(2, "Go to Menu 2");
            MenuItem m1i3 = new MenuItem(3, "Go to Menu 3");

            MenuItem m2i1 = new MenuItem(1, "Option 1");
            MenuItem m2i2 = new MenuItem(2, "Option 2");
            MenuItem m2i3 = new MenuItem(3, "Option 3");

            MenuItem m3i1 = new MenuItem(1, "Setting 1");
            MenuItem m3i2 = new MenuItem(2, "Setting 2");
            MenuItem m3i3 = new MenuItem(3, "Setting 3");

            MenuItem[] menu1Items = { m1i1, m1i2, m1i3 };
            MenuItem[] menu2Items = { m2i1, m2i2, m2i3 };
            MenuItem[] menu3Items = { m3i1, m3i2, m3i3 };

            Menu menu1 = new Menu(menu1Items);
            Menu menu2 = new Menu(menu2Items);
            Menu menu3 = new Menu(menu3Items);

            Menu[] menus = { menu1, menu2, menu3 };

            // send menus to main menu
            MainMenu(menus);
        }

        static void MainMenu(Menu[] menus)
        {
            bool active = true;
            int current = 0; // current menu. Default to home
            while (active)
            {
                Console.Clear();
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
    }
    public class MenuItem
    {
        public int Num; // num to display and fire
        public string Title; // text
        public MenuItem(int num, string title)
        {
            Num = num;
            Title = title;
        }
    }
    public class Menu
    {
        public MenuItem[] MenuItems; // array of menu items
        public Menu(MenuItem[] menuItem)
        {
            MenuItems = menuItem;
        }
    }
}
