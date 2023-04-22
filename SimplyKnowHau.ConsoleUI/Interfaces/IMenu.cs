using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyKnowHau.ConsoleUI.Interfaces
{
    public interface IMenu
    {
        void MenuStarts();

        void MenuDisplay();

        void SelectMenuOption();

        void ChoosenOption();

        void MenuExit();
    }
}
