using SimplyKnowHau.ConsoleUI.Interfaces;
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



        public static string? userName = string.Empty;

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


                LogoAndHelpers.SetCursorAndMsg(32, welcomeMessage, FG);

                do
                {

                    userName = LogoAndHelpers.SetCursorAndRead(32);

                    if (string.IsNullOrWhiteSpace(userName))
                    {
                        LogoAndHelpers.SetCursorAndMsg(32, "First, give me your name!", ERR);

                    }
                    else if (UserLogic.GetByName(userName) == null)
                    {
                        LogoAndHelpers.DisplayLogo();

                        LogoAndHelpers.SetCursorAndMsg(32, $"Hi {userName}!", FG);

                        LogoAndHelpers.SetCursorAndMsg(32, "Remember that username, you were added to our database!", FG);

                        Console.WriteLine();

                        LogoAndHelpers.SetCursorAndMsg(32, "Are you happy with your choice? (Y/N)", FG);

                        Console.CursorVisible = false;

                        do
                        {
                            ConsoleKeyInfo key = Console.ReadKey();
                            if (key.Key == ConsoleKey.N)
                            {
                                userName = String.Empty;
                                Starts(dictionary);
                                break;
                            }
                            else if (key.Key == ConsoleKey.Y)
                            {
                                break;
                            }
                        } while (true);

                        UserLogic.SetCurrentUser(UserLogic.AddUser(userName));
                        break;
                    }
                    else
                    {
                        UserLogic.SetCurrentUser(UserLogic.GetByName(userName));
                        break;
                    }
                } while (true);
            }


        }
        public void MenuDisplay()
        {
            throw new NotImplementedException();
        }

        public void SelectMenuOption()
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

        

        
    }
}
