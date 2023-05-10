using SimplyKnowHau.Data.Model;
using SimplyKnowHau.Logic.Logic;
using SimplyKnowHau.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyKnowHau.ConsoleUI.EditCard;
using SimplyKnowHau.ConsoleUI.Menus;

namespace SimplyKnowHau.ConsoleUI.Cards
{
    internal class AppointmentCard
    {
        const ConsoleColor BG = ConsoleColor.Black;
        const ConsoleColor BG_ACTIVE = ConsoleColor.DarkYellow;
        const ConsoleColor FG = ConsoleColor.DarkYellow;
        const ConsoleColor FG_ACTIVE = ConsoleColor.White;
        const ConsoleColor ERR = ConsoleColor.DarkRed;


        private static int activePosition = 1;


        private static List<CardItem>? cardItemsAppointment = new();
        private AnimalLogic _animalLogic;
        private Appointment _appointment;


        private static List<CardItem> shortMenu = new()
        {
            new CardItem(1, "Delete Visit"),
            new CardItem(2, "Back"),

        };

        public AppointmentCard(Appointment appointment, AnimalLogic animalLogic)
        {
            _animalLogic = animalLogic;
            _appointment = appointment;
            cardItemsAppointment.Add(new CardItem(1, "Animal: ", _animalLogic.GetById(appointment.AnimalId).Name));
            cardItemsAppointment.Add(new CardItem(2, "Owner: ", UserLogic.currentUser.Name));
            cardItemsAppointment.Add(new CardItem(3, "Age: ", _animalLogic.Age(_animalLogic.GetById(appointment.AnimalId)).ToString()));
            cardItemsAppointment.Add(new CardItem(4, "Date of birth: ", _animalLogic.GetById(appointment.AnimalId).DateOfBirth.ToShortDateString()));
            cardItemsAppointment.Add(new CardItem(5, "Date: ", appointment.Date.ToShortDateString()));
            cardItemsAppointment.Add(new CardItem(6, "Description of the visit: ", appointment.Description));
            cardItemsAppointment.Add(new CardItem(7, "Receives: ", appointment.Recipe));
        }

        private void DisplayShortAppointmentMenu()
        {
            Console.SetCursorPosition((Console.WindowWidth - 32) / 2, Console.CursorTop);
            Console.WriteLine();

            for (int i = 1; i <= shortMenu.Count; i++)
            {
                if (activePosition == i)
                {
                    Console.BackgroundColor = BG_ACTIVE;
                    Console.ForegroundColor = FG_ACTIVE;
                    if (i == shortMenu.Count)
                    {
                        Console.SetCursorPosition((Console.WindowWidth - 32) / 2, Console.CursorTop);
                        Console.Write($" ESC. ");
                    }
                    else
                    {
                        Console.SetCursorPosition((Console.WindowWidth - 32) / 2, Console.CursorTop);
                        Console.Write($" {i}. ");
                    }
                    //Console.SetCursorPosition((Console.WindowWidth - _stMenuOptions.ElementAt(1).Value.Length) / 2, Console.CursorTop);
                    Console.WriteLine("{0,-30}", shortMenu.ElementAt(i - 1).CardString);
                    Console.BackgroundColor = BG;
                    Console.ForegroundColor = FG;
                }
                else
                {
                    if (i == shortMenu.Count)
                    {
                        Console.SetCursorPosition((Console.WindowWidth - 32) / 2, Console.CursorTop);
                        Console.Write(" ESC.");
                    }
                    else
                    {
                        Console.SetCursorPosition((Console.WindowWidth - 32) / 2, Console.CursorTop);
                        Console.Write($" {i}. ");
                    }
                    //Console.SetCursorPosition((Console.WindowWidth - _stMenuOptions.ElementAt(1).Value.Length) / 2, Console.CursorTop);
                    Console.WriteLine(shortMenu.ElementAt(i - 1).CardString);
                }
            }
        }
        public void SelectShortMenuOption()
        {
            do
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    activePosition = activePosition > 1 ? --activePosition : shortMenu.Count;
                    StartAnimalCard();
                    break;
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    activePosition = activePosition % shortMenu.Count + 1;
                    StartAnimalCard();
                    break;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    activePosition = shortMenu.Count;
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else
                {
                    if (key.Key == ConsoleKey.D1) activePosition = 1;
                    if (key.Key == ConsoleKey.D2) activePosition = 2;
                    if (key.Key == ConsoleKey.D3) activePosition = 3;
                    break;
                }
            } while (true);
        }
        public void ChoosenShortOption()
        {
            switch (activePosition)
            {
                case 1:

                    break;
                case 2:
                    var speciesLogic = new SpeciesLogic();
                    var animalEditCard = new AnimalEditCard(_animal, speciesLogic);
                    animalEditCard.EditCardAnimal();
                    break;
                case 3:
                    ShortMenuExit();
                    break;
            }
        }

        private void ShortMenuExit()
        {
            var animalLogic = new AnimalLogic();
            var animalMenu = new AnimalMenu(animalLogic);
            animalMenu.MenuStarts();
        }
    }
}
