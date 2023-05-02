namespace Menus 
{
    public class MenuItem
    {
        public int Num; // num to display and fire
        public string Title; // text
        public int Page; // text
        public MenuItem(int num, string title, int page)
        {
            Num = num;
            Title = title;
            Page = page;
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