using SimplyKnowHau.Data.Model;
using SimplyKnowHau.Logic.Logic;
using SimplyKnowHau.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyKnowHau.ConsoleUI.AddCard;
using SimplyKnowHau.ConsoleUI.Cards;
using SimplyKnowHau.ConsoleUI.Interfaces;

namespace SimplyKnowHau.ConsoleUI.Menus
{
    internal class AppointmentMenu : IMenu
    {
        const ConsoleColor BG = ConsoleColor.Black;
        const ConsoleColor BG_ACTIVE = ConsoleColor.DarkYellow;
        const ConsoleColor FG = ConsoleColor.DarkYellow;
        const ConsoleColor FG_ACTIVE = ConsoleColor.White;
        const ConsoleColor ERR = ConsoleColor.DarkRed;

        private string userName = UserLogic.currentUser.Name;

        private AppointmentLogic _appointmentLogic;
        private AnimalLogic _animalLogic;

        private static int activePosition = 1;
        private static string? welcomeMessage = "Welcome user! Give me your name:";

        private static List<CardItem> appointmentMenuOptions = new();

        public AppointmentMenu(AppointmentLogic appointmentLogic, AnimalLogic animalLogic)
        {
            _appointmentLogic = appointmentLogic;
            _animalLogic = animalLogic;
            appointmentMenuOptions = CreateMenu();
            
        }
        public void MenuStarts()
        {
            activePosition = 1;
            MenuDisplay();
            SelectMenuOption();
            ChoosenOption();
        }

        public void MenuDisplay()
        {
            Console.CursorVisible = false;

            LogoAndHelpers.DisplayLogo();

            LogoAndHelpers.SetCursorAndMsgWriteLine(32, $"Hi {userName}! What you want to do?", FG);

            Console.WriteLine();

            for (int i = 1; i <= appointmentMenuOptions.Count; i++)
            {
                if (activePosition == i)
                {
                    Console.BackgroundColor = BG_ACTIVE;
                    Console.ForegroundColor = FG_ACTIVE;
                    if (i == appointmentMenuOptions.Count)
                    {
                        Console.SetCursorPosition((Console.WindowWidth - welcomeMessage.Length) / 2, Console.CursorTop);
                        Console.Write($" ESC. ");
                    }
                    else
                    {
                        Console.SetCursorPosition((Console.WindowWidth - welcomeMessage.Length) / 2, Console.CursorTop);
                        Console.Write($" {i}. ");
                    }
                    //Console.SetCursorPosition((Console.WindowWidth - _stMenuOptions.ElementAt(1).Value.Length) / 2, Console.CursorTop);
                    Console.WriteLine("{0,-30}", appointmentMenuOptions.ElementAt(i - 1).CardString);
                    Console.BackgroundColor = BG;
                    Console.ForegroundColor = FG;
                }
                else
                {
                    if (i == appointmentMenuOptions.Count)
                    {
                        Console.SetCursorPosition((Console.WindowWidth - welcomeMessage.Length) / 2, Console.CursorTop);
                        Console.Write(" ESC.");
                    }
                    else
                    {
                        Console.SetCursorPosition((Console.WindowWidth - welcomeMessage.Length) / 2, Console.CursorTop);
                        Console.Write($" {i}. ");
                    }
                    //Console.SetCursorPosition((Console.WindowWidth - _stMenuOptions.ElementAt(1).Value.Length) / 2, Console.CursorTop);
                    Console.WriteLine(appointmentMenuOptions.ElementAt(i - 1).CardString);
                }
            }
        }

        public void SelectMenuOption()
        {
            do
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    activePosition = (activePosition > 1) ? --activePosition : appointmentMenuOptions.Count;
                    MenuDisplay();
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    activePosition = (activePosition % appointmentMenuOptions.Count) + 1;
                    MenuDisplay();
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    activePosition = appointmentMenuOptions.Count;
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else
                {
                    MenuDisplay();
                }
            } while (true);
        }

        public void ChoosenOption()
        {
            if (activePosition == 1)
            {
                activePosition = 1;
                var speciesLogic = new SpeciesLogic();
                var animalAddCard = new AnimalAddCard(speciesLogic);
                animalAddCard.AddAnimal();
            }
            else if (activePosition == appointmentMenuOptions.Count)
            {
                MenuExit();
            }
            else if (appointmentMenuOptions.ElementAt(activePosition - 1).CardString != "No more animals to show")
            {
                
            }
            else
            {
                MenuStarts();
            }
        }

        public void MenuExit()
        {
            var userLogic = new UserLogic();
            var startingMenu = new StartingMenu(userLogic);
            startingMenu.MenuStarts();
        }

        public List<CardItem> CreateMenu()
        {

            var appointmentList = _appointmentLogic.GetAppointmentsList().Where(c => c.UserId == UserLogic.currentUser.Id);
            var menu = new List<CardItem>();

            for (int i = 1; i <= appointmentList.Count() + 3; i++)
            {
                if (i == 1)
                {
                    menu.Add(new CardItem(i, "Make an appointment"));
                }
                else if (i == appointmentList.Count() + 3)
                {
                    menu.Add(new CardItem(i, "Back"));
                }
                else if (i >= appointmentList.Count() + 2)
                {
                    menu.Add(new CardItem(i, "No more appointments to show"));
                }
                else
                {
                    menu.Add(new CardItem(appointmentList.ElementAt(i - 2).Id, $"{appointmentList.ElementAt(i - 2).Date.ToShortDateString()}: {_animalLogic.GetById(appointmentList.ElementAt(i - 2).AnimalId).Name}"));
                }
            }

            return menu;
        }
    }
}
