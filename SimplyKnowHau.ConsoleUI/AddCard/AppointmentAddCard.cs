using SimplyKnowHau.Data.Model;
using SimplyKnowHau.Logic.Logic;
using SimplyKnowHau.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplyKnowHau.ConsoleUI.Menus;

namespace SimplyKnowHau.ConsoleUI.AddCard
{
    internal class AppointmentAddCard
    {
        const ConsoleColor BG = ConsoleColor.Black;
        const ConsoleColor BG_ACTIVE = ConsoleColor.DarkYellow;
        const ConsoleColor FG = ConsoleColor.DarkYellow;
        const ConsoleColor FG_ACTIVE = ConsoleColor.White;
        const ConsoleColor ERR = ConsoleColor.DarkRed;

        private static List<CardItem>? cardItemsAppointment = new();
        private Animal _animal;
        private AnimalLogic _animalLogic;
        private AppointmentLogic _appointmentLogic;


        public AppointmentAddCard(Animal animal, AnimalLogic animalLogic, AppointmentLogic appointmentLogic)
        {
            _animal = animal;
            _animalLogic = animalLogic;
            _appointmentLogic = appointmentLogic;
            cardItemsAppointment.Clear();
            cardItemsAppointment.Add(new CardItem(1, "Animal: ", _animal.Name));
            cardItemsAppointment.Add(new CardItem(2, "Owner: ", UserLogic.currentUser.Name));
            cardItemsAppointment.Add(new CardItem(3, "Age: ", $"{_animalLogic.Age(_animal)}"));
            cardItemsAppointment.Add(new CardItem(4, "Date of birth: ", _animal.DateOfBirth.ToShortDateString()));
            cardItemsAppointment.Add(new CardItem(5, "Date: ", DateTime.Now.ToShortDateString()));
            cardItemsAppointment.Add(new CardItem(6, "Description of the visit: ", "" ));
            cardItemsAppointment.Add(new CardItem(7, "Receives: ", ""));
        }

        public void DisplayAppointmentAddCard()
        {
            
            bool broken = false;
            LogoAndHelpers.DisplayLogo();
            Console.CursorVisible = true;

            for (int i = 1; i <= cardItemsAppointment.Count; i++)
            {

                if (i != cardItemsAppointment.Count - 1 && i != cardItemsAppointment.Count)
                {
                    LogoAndHelpers.SetCursorAndMsgWrite(50, $"{cardItemsAppointment.ElementAt(i - 1).CardString}", FG);
                    Console.ForegroundColor = FG_ACTIVE;
                    Console.Write($"{cardItemsAppointment.ElementAt(i - 1).CardContent}");
                    Console.WriteLine();
                }
                else
                {

                    LogoAndHelpers.SetCursorAndMsgWrite(50, $"{cardItemsAppointment.ElementAt(i - 1).CardString}", FG);
                    Console.ForegroundColor = FG_ACTIVE;
                    do
                    {
                        string? userInsert = Console.ReadLine();
                        if (userInsert == string.Empty)
                        {
                            LogoAndHelpers.SetCursorAndMsgWriteLine(50, "Type something!", ERR);
                            
                            LogoAndHelpers.SetCursorAndMsgWriteLine(50, "ESC - To go back to Appointment Menu. Press any key now to continue.", ERR);
                            
                            Console.SetCursorPosition((Console.WindowWidth - 50) / 2, Console.CursorTop);
                            ConsoleKeyInfo key = Console.ReadKey();
                            if (key.Key != ConsoleKey.Escape)
                            {
                                i--;
                                break;

                            }
                            else
                            {
                                broken = true;
                                i = cardItemsAppointment.Count() - 1;
                                break;
                            }
                        }
                        else
                        {
                            if (userInsert.Length > 300)
                            {
                                LogoAndHelpers.SetCursorAndMsgWriteLine(50, "Maximum 300 characters!", ERR);
                               
                                LogoAndHelpers.SetCursorAndMsgWriteLine(50, "ESC - To go back to Appointment Menu. Press any key now to continue.", ERR);
                                
                                Console.SetCursorPosition((Console.WindowWidth - 50) / 2, Console.CursorTop);
                                ConsoleKeyInfo key = Console.ReadKey();
                                if (key.Key != ConsoleKey.Escape)
                                {
                                    i--;
                                    break;

                                }
                                else
                                {
                                    broken = true;
                                    i = cardItemsAppointment.Count() - 1;
                                    break;
                                }
                            }
                            if (!broken)
                            {
                                cardItemsAppointment.ElementAt(i - 1).CardContent = userInsert;
                            }
                            break;
                        }
                    } while (true);
                }
            }
            if (!broken)
            {
                _appointmentLogic.AddAppointment(
                    UserLogic.currentUser.Id,
                    _animal.Id,
                    DateTime.Parse(cardItemsAppointment.ElementAt(4).CardContent),
                    cardItemsAppointment.ElementAt(5).CardContent,
                    cardItemsAppointment.ElementAt(6).CardContent
                    );
            }

            var appointmentMenu = new AppointmentMenu(_appointmentLogic, _animalLogic);
            appointmentMenu.MenuStarts();
        }
    }
}
