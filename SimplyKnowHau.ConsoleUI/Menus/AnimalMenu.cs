using SimplyKnowHau.ConsoleUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyKnowHau.ConsoleUI.Menus
{
    internal class AnimalMenu : IMenu
    {
        private static int activePosition = 1;

        private static List<CardItem> startMenuOptions = new() {
            new CardItem(1, "Your Animals")),
            new CardItem(2, "Make an apointment"),
            new CardItem(3, "History of apointments"),
            new CardItem(4, "?"),
            new CardItem(5, "?"),
            new CardItem(6, "?"),
            new CardItem(7, "?"),
            new CardItem(8, "Logout"),
            new CardItem(9, "Quit")
            };

        public void MenuStarts()
        {
            activePosition = 1;
            MenuDisplay();
            SelectMenuOption();
            ChoosenOption();
        }

        public void MenuDisplay()
        {
            throw new NotImplementedException();
        }

        public void ChoosenOption()
        {
            throw new NotImplementedException();
        }

        

        public void MenuExit()
        {
            throw new NotImplementedException();
        }

        

        public void SelectMenuOption()
        {
            throw new NotImplementedException();
        }
    }
}
