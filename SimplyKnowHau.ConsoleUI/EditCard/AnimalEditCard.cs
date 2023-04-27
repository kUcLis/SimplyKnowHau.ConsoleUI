using SimplyKnowHau.ConsoleUI.Cards;
using SimplyKnowHau.Data;
using SimplyKnowHau.Data.Model;
using SimplyKnowHau.Logic;
using SimplyKnowHau.Logic.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyKnowHau.ConsoleUI.EditCard
{
    internal class AnimalEditCard
    {
        const ConsoleColor FG = ConsoleColor.DarkYellow;
        const ConsoleColor FG_ACTIVE = ConsoleColor.White;
        const ConsoleColor ERR = ConsoleColor.DarkRed;

        private SpeciesLogic _speciesLogic;
        private Animal _animal;
        private  List<CardItem>? editCardItemsAnimal = new();

        public AnimalEditCard(Animal animal, SpeciesLogic speciesLogic)
        {
            _animal = animal;
            _speciesLogic = speciesLogic;

            
            editCardItemsAnimal.Add(new CardItem(1, "Name: ", animal.Name));
            editCardItemsAnimal.Add(new CardItem(2, "Specie: ", animal.AnimalCategory.Specie));
            editCardItemsAnimal.Add(new CardItem(3, "Date of birth: ", animal.DateOfBirth.ToShortDateString()));

        }

        public void EditCardAnimal()
        {
            
            bool broken = false;
            LogoAndHelpers.DisplayLogo();
            Console.CursorVisible = true;


            LogoAndHelpers.SetCursorAndMsgWriteLine(50, "Leave empty if you don't want to change value", FG_ACTIVE);
            
            for (int i = 1; i <= editCardItemsAnimal.Count; i++)
            {

                if (i == 2)
                {
                    LogoAndHelpers.SetCursorAndMsgWriteLine(50, $"Spiecies you can choose:{_speciesLogic.SpeciesToString()}", FG_ACTIVE);
                }

                LogoAndHelpers.SetCursorAndMsgWrite(50, $"{editCardItemsAnimal.ElementAt(i - 1).CardString}", FG);
                
                Console.Write($"{editCardItemsAnimal.ElementAt(i - 1).CardContent}");
                
                Console.WriteLine();

                LogoAndHelpers.SetCursorAndMsgWrite(50, "New: ", FG_ACTIVE);
                

                do
                {
                    string? userInsert = Console.ReadLine();
                    if (userInsert == string.Empty)
                    {

                        break;
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
                                i = editCardItemsAnimal.Count();
                                break;
                            }
                        }
                        if (!broken)
                        {
                            editCardItemsAnimal.ElementAt(i - 1).CardContent = userInsert;
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
                                i = editCardItemsAnimal.Count();
                                break;
                            }
                        }
                        if (!broken)
                        {
                            editCardItemsAnimal.ElementAt(i - 1).CardContent = userInsert;
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
                                i = editCardItemsAnimal.Count();
                                break;
                            }
                        }
                        if (!broken)
                        {
                            editCardItemsAnimal.ElementAt(i - 1).CardContent = userInsert;
                        }
                        break;
                    }
                } while (true);
            }
            if (!broken)
            {
                var animalToEdit = DataMenager.Animals.First(c => c.Id == _animal.Id);

                animalToEdit.Name = editCardItemsAnimal.ElementAt(0).CardContent;

                animalToEdit.AnimalCategory = _speciesLogic.GetByName(editCardItemsAnimal.ElementAt(1).CardContent);

                animalToEdit.DateOfBirth = DateTime.Parse(editCardItemsAnimal.ElementAt(2).CardContent);

                _animal = animalToEdit;
            }

            var animalLogic = new AnimalLogic();
            var appointmentLogic = new AppointmentLogic();
            var animalCard = new AnimalCard(_animal, animalLogic,appointmentLogic);
            animalCard.StartAnimalCard();
        }


    }
}
