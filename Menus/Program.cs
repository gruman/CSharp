using System;

namespace Menus
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }

        static void MainMenu()
        {
            string[] menu1 = { "1. Second Menu", "0. Home"};
            string[] menu2 = { "2. Third Menu", "0. Home"};

            string[] chosenMenu;
            int menu = 0;
            bool active = true;
            while (active)
            {
                Console.Clear();
                Console.WriteLine("Menu #" + menu);
                if (menu == 1)
                {
                    chosenMenu = menu2;
                }
                else
                {
                    chosenMenu = menu1;
                }
                
                    foreach (string item in chosenMenu)
                    {
                        Console.WriteLine(item);
                    }
                int choice;
                string input = Console.ReadLine();
                // chrvk if input is a number and apply it to choice
                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }
                if (int.TryParse(input, out choice))
                {
                    menu = choice;
                }
            }
        }
    }
}