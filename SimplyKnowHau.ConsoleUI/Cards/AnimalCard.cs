﻿using SimplyKnowHau.ConsoleUI.EditCard;
using SimplyKnowHau.ConsoleUI.Menus;
using SimplyKnowHau.Data.Model;
using SimplyKnowHau.Logic;
using SimplyKnowHau.Logic.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyKnowHau.ConsoleUI.Cards
{
    internal class AnimalCard
    {
        const ConsoleColor BG = ConsoleColor.Black;
        const ConsoleColor BG_ACTIVE = ConsoleColor.DarkYellow;
        const ConsoleColor FG = ConsoleColor.DarkYellow;
        const ConsoleColor FG_ACTIVE = ConsoleColor.White;
        const ConsoleColor ERR = ConsoleColor.DarkRed;


        private static int activePosition = 1;


        private static List<CardItem>? cardItemsAnimal = new();
        private AnimalLogic _animalLogic;
        private Animal _animal;


        private static List<CardItem> shortMenu = new()
        {
            new CardItem(1, "Make an appointment for that animal"),
            new CardItem(2, "Edit animal"),
            new CardItem(3, "Back")
        };



        public AnimalCard(Animal animal, AnimalLogic animalLogic, AppointmentLogic appointmentLogic)
        {
            _animalLogic = animalLogic;
            _animal = animal;

            var appointment = appointmentLogic.GetLastByAnimalId(animal.Id);

            cardItemsAnimal.Clear();
            cardItemsAnimal.Add(new CardItem(1, "Animal Id: ", animal.Id.ToString()));
            cardItemsAnimal.Add(new CardItem(2, "Name: ", animal.Name));
            cardItemsAnimal.Add(new CardItem(3, "Specie: ", animal.AnimalCategory.Specie));
            cardItemsAnimal.Add(new CardItem(4, "Age: ", _animalLogic.Age(animal).ToString()));
            cardItemsAnimal.Add(new CardItem(5, "Date of birth: ", animal.DateOfBirth.ToShortDateString()));
            if (appointment != null)
            {
                cardItemsAnimal.Add(new CardItem(6, "Date of last visit: ", appointment.Date.ToShortDateString()));
                cardItemsAnimal.Add(new CardItem(7, "Description of the visit: ", appointment.Description));
                cardItemsAnimal.Add(new CardItem(8, "Receives: ", appointment.Recipe));
            }
            else
            {
                cardItemsAnimal.Add(new CardItem(6, "Date of last visit: ", "No appointment yet!"));
                cardItemsAnimal.Add(new CardItem(7, "Description of the visit: ", "No appointment yet!"));
                cardItemsAnimal.Add(new CardItem(8, "Receives: ", "No appointment yet!"));
            }

        }

        public void StartAnimalCard()
        {
            DisplayCardAnimal();
            DisplayShortAnimalMenu();
            SelectShortMenuOption();
            ChoosenShortOption();
        }

        private void DisplayCardAnimal()
        {
            LogoAndHelpers.DisplayLogo();

            
            for (int i = 0; i < cardItemsAnimal.Count; i++)
            {

                LogoAndHelpers.SetCursorAndMsgWrite(50, $"{cardItemsAnimal.ElementAt(i).CardString}", FG);

                Console.ForegroundColor = FG_ACTIVE;
                string stringhelper = cardItemsAnimal.ElementAt(i).CardContent;
                while (true)
                {
                    if (stringhelper.Length > 60)
                    {

                        Console.WriteLine();
                        Console.SetCursorPosition((Console.WindowWidth - 50) / 2, Console.CursorTop);
                        int j = 50;
                        string stringhelper2 = stringhelper;
                        while (true)
                        {
                            if (stringhelper2[j] != ' ')
                            {
                                j++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        stringhelper2 = stringhelper2.Substring(0, j);
                        Console.Write(stringhelper2);
                        stringhelper2 = stringhelper;
                        Console.SetCursorPosition((Console.WindowWidth - 50) / 2, Console.CursorTop);
                        stringhelper = stringhelper2.Substring(j + 1, stringhelper2.Length - (j + 1));

                    }
                    else
                    {
                        Console.Write($"{stringhelper}");
                        break;
                    }
                }
                Console.WriteLine();
                Console.ForegroundColor = FG;
            }
        }

        private void DisplayShortAnimalMenu()
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
