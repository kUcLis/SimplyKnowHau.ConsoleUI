using SimplyKnowHau.ConsoleUI.Menus;
using SimplyKnowHau.Logic.Logic;
using SimplyKnowHau.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyKnowHau.ConsoleUI.AddCard
{
    internal class AnimalAddCard
    {
        const ConsoleColor FG = ConsoleColor.DarkYellow;
        const ConsoleColor FG_ACTIVE = ConsoleColor.White;
        const ConsoleColor ERR = ConsoleColor.DarkRed;

        private static List<CardItem>? cardItemsAnimal = new()
        {
            new CardItem(1, "Name of the animal:"),
            new CardItem(2, "Specie:"),
            new CardItem(3, "Date of birth (YYYY-MM-DD):")
        };

        private SpeciesLogic _speciesLogic;

        public AnimalAddCard(SpeciesLogic speciesLogic)
        {
            _speciesLogic = speciesLogic;
        }


        public void DisplatAnimalAddCard()
        {
            var animalLogic = new AnimalLogic();
            bool broken = false;
            LogoAndHelpers.DisplayLogo();
            Console.CursorVisible = true;

            for (int i = 1; i <= cardItemsAnimal.Count; i++)
            {

                if (i == 2)
                {
                    LogoAndHelpers.SetCursorAndMsgWriteLine(50, $"Spiecies you can choose:{_speciesLogic.SpeciesToString()}", FG_ACTIVE);          
                }

                LogoAndHelpers.SetCursorAndMsgWrite(50, $"{cardItemsAnimal.ElementAt(i - 1).CardString}", FG);

                Console.ForegroundColor = ConsoleColor.Gray;

                do
                {
                    string? userInsert = Console.ReadLine();
                    if (userInsert == string.Empty)
                    {
                        LogoAndHelpers.SetCursorAndMsgWriteLine(50, "Type something!", ERR);

                        LogoAndHelpers.SetCursorAndMsgWriteLine(50, "ESC - To go back to Animal Menu. Press any key now to continue.", ERR);
                        
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
                            i = cardItemsAnimal.Count();
                            break;
                        }
                    }
                    if (i == 1)
                    {
                        if (userInsert.Length > 15)
                        {
                            LogoAndHelpers.SetCursorAndMsgWriteLine(50, "Maximum 15 characters!", ERR);

                            LogoAndHelpers.SetCursorAndMsgWriteLine(50, "ESC - To go back to Animal Menu. Press any key now to continue.", ERR);
                            
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
                                i = cardItemsAnimal.Count();
                                break;
                            }
                        }
                        if (!broken)
                        {
                            cardItemsAnimal.ElementAt(i - 1).CardContent = userInsert;
                        }
                        break;
                    }
                    else if (i == 2)
                    {
                        if (_speciesLogic.GetByName(userInsert) == null)
                        {
                            LogoAndHelpers.SetCursorAndMsgWriteLine(50, $"Spiecies you can choose:{_speciesLogic.SpeciesToString()}", FG_ACTIVE);

                            LogoAndHelpers.SetCursorAndMsgWriteLine(50, "Must be in the one of Categories!", ERR);

                            LogoAndHelpers.SetCursorAndMsgWriteLine(50, "ESC - To go back to Animal Menu. Press any key now to continue.", ERR);

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
                                i = cardItemsAnimal.Count();
                                break;
                            }
                        }
                        if (!broken)
                        {
                            cardItemsAnimal.ElementAt(i - 1).CardContent = userInsert;
                        }
                        break;
                    }
                    else
                    {
                        if (!DateTime.TryParse(userInsert, out DateTime result))
                        {
                            LogoAndHelpers.SetCursorAndMsgWriteLine(50, "It has to be in forma YYYY-MM-DD. Example: 1989-03-06", FG_ACTIVE);

                            LogoAndHelpers.SetCursorAndMsgWriteLine(50, "Format the date correctly!", ERR);

                            LogoAndHelpers.SetCursorAndMsgWriteLine(50, "ESC - To go back to Animal Menu. Press any key now to continue.", ERR);

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
                                i = cardItemsAnimal.Count();
                                break;
                            }
                        }
                        if (!broken)
                        {
                            cardItemsAnimal.ElementAt(i - 1).CardContent = userInsert;
                        }
                        break;
                    }
                } while (true);
            }
            if (!broken)
            {
                animalLogic.AddAnimal(
                    cardItemsAnimal.ElementAt(0).CardContent,
                    _speciesLogic.GetByName(cardItemsAnimal.ElementAt(1).CardContent),
                    DateTime.Parse(cardItemsAnimal.ElementAt(2).CardContent)
                    );
            }

            
            var animalMenu = new AnimalMenu(animalLogic);
            animalMenu.MenuStarts();
        }
    }
}
