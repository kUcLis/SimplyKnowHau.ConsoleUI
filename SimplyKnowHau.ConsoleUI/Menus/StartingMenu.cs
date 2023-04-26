using SimplyKnowHau.ConsoleUI.Interfaces;
using SimplyKnowHau.Logic;
using SimplyKnowHau.Logic.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyKnowHau.ConsoleUI.Menus
{
    internal class StartingMenu : IMenu
    {
        const ConsoleColor BG = ConsoleColor.Black;
        const ConsoleColor BG_ACTIVE = ConsoleColor.DarkYellow;
        const ConsoleColor FG = ConsoleColor.DarkYellow;
        const ConsoleColor FG_ACTIVE = ConsoleColor.White;
        const ConsoleColor ERR = ConsoleColor.DarkRed;


        private static int activePosition = 1;
        private static string? welcomeMessage = "Welcome user! Give me your name:";
        
        private static List<CardItem> startMenuOptions = new() {
            new CardItem(1, "Your Animals"),
            new CardItem(2, "Make an apointment"),
            new CardItem(3, "History of apointments"),
            new CardItem(4, "?"),
            new CardItem(5, "?"),
            new CardItem(6, "?"),
            new CardItem(7, "?"),
            new CardItem(8, "Logout"),
            new CardItem(9, "Quit")
            };

        private UserLogic _userLogic;

        public static string? userName = string.Empty;



        public StartingMenu(UserLogic userLogic)
        {
            _userLogic = userLogic;
        }



        public void MenuStarts()
        {
            activePosition = 1;
            Login();
            MenuDisplay();
            SelectMenuOption();
            ChoosenOption();
        }
        public void Login()
        {
            LogoAndHelpers.DisplayLogo();
            Console.CursorVisible = true;

            if (userName == string.Empty)
            {


                LogoAndHelpers.SetCursorAndMsgWriteLine(32, welcomeMessage, FG);

                do
                {

                    userName = LogoAndHelpers.SetCursorAndRead(32);

                    if (string.IsNullOrWhiteSpace(userName))
                    {
                        LogoAndHelpers.SetCursorAndMsgWriteLine(32, "First, give me your name!", ERR);

                    }
                    else if (_userLogic.GetByName(userName) == null)
                    {
                        LogoAndHelpers.DisplayLogo();

                        LogoAndHelpers.SetCursorAndMsgWriteLine(32, $"Hi {userName}!", FG);

                        LogoAndHelpers.SetCursorAndMsgWriteLine(32, "Remember that username, you were added to our database!", FG);

                        Console.WriteLine();

                        LogoAndHelpers.SetCursorAndMsgWriteLine(32, "Are you happy with your choice? (Y/N)", FG);

                        Console.CursorVisible = false;

                        do
                        {
                            ConsoleKeyInfo key = Console.ReadKey();
                            if (key.Key == ConsoleKey.N)
                            {
                                userName = String.Empty;
                                MenuStarts();
                                break;
                            }
                            else if (key.Key == ConsoleKey.Y)
                            {
                                break;
                            }
                        } while (true);

                        _userLogic.SetCurrentUser(_userLogic.AddUser(userName));
                        break;
                    }
                    else
                    {
                        _userLogic.SetCurrentUser(_userLogic.GetByName(userName));
                        break;
                    }
                } while (true);
            }


        }
        public void MenuDisplay()
        {
            Console.CursorVisible = false;

            LogoAndHelpers.DisplayLogo();

            LogoAndHelpers.SetCursorAndMsgWriteLine(32, $"Hi {userName}! What you want to do?", FG);

            Console.WriteLine();

            for (int i = 1; i <= startMenuOptions.Count; i++)
            {
                if (activePosition == i)
                {
                    Console.BackgroundColor = BG_ACTIVE;
                    Console.ForegroundColor = FG_ACTIVE;
                    if (i == startMenuOptions.Count)
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
                    Console.WriteLine("{0,-30}", startMenuOptions.ElementAt(i - 1).CardString);
                    Console.BackgroundColor = BG;
                    Console.ForegroundColor = FG;
                }
                else
                {
                    if (i == startMenuOptions.Count)
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
                    Console.WriteLine(startMenuOptions.ElementAt(i - 1).CardString);
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
                    activePosition = (activePosition > 1) ? --activePosition : startMenuOptions.Count;
                    MenuDisplay();
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    activePosition = (activePosition % startMenuOptions.Count) + 1;
                    MenuDisplay();
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    activePosition = startMenuOptions.Count;
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
            switch (activePosition)
            {
                case 1:
                    var animalLogic = new AnimalLogic();
                    var animalMenu = new AnimalMenu(animalLogic);
                    animalMenu.MenuStarts();
                    break;
                case 2:
                    //var dictionary4 = new Dictionaries(5);
                    //Starts(Dictionaries.ChooseAnimalMenuOptions);
                    break;
                case 3:
                    //var dictionary3 = new Dictionaries(4);
                    //Starts(Dictionaries.AppointmentMenuOptions);
                    break;
                case 4:

                    MenuStarts();
                    break;
                case 5:
                    MenuStarts();
                    break;
                case 6:
                    MenuStarts();
                    break;
                case 7:
                    MenuStarts();
                    break;
                case 8:
                    userName = String.Empty;
                    MenuStarts();
                    break;
                case 9:
                    MenuExit();
                    break;
            }
        }

        

        public void MenuExit()
        {
            LogoAndHelpers.DisplayLogo();
            LogoAndHelpers.SetCursorAndMsgWriteLine(32, "You really want to quit? (Y/N)", FG);

            do
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.N)
                {
                    MenuStarts();
                    break;
                }
                else if (key.Key == ConsoleKey.Y)
                {
                    LogoAndHelpers.DisplayLogo();
                    LogoAndHelpers.SetCursorAndMsgWriteLine(32, $"Bye {userName}", FG);

                    activePosition = 0;
                    Console.ReadLine();

                    Environment.Exit(0);
                    break;
                }
            } while (true);
        }

        

        
    }
}
