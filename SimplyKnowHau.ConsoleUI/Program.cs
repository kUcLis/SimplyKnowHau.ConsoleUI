using SimplyKnowHau.ConsoleUI.Menus;
using SimplyKnowHau.Logic.Logic;

namespace SimplyKnowHau.ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userLogic = new UserLogic();
            var menu = new StartingMenu(userLogic);

            menu.MenuStarts();

            Environment.Exit(0);
        }
    }
}