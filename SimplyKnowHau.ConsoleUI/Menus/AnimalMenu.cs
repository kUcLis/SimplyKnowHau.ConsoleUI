﻿using SimplyKnowHau.ConsoleUI.AddCard;
using SimplyKnowHau.ConsoleUI.Cards;
using SimplyKnowHau.ConsoleUI.Interfaces;
using SimplyKnowHau.Data.Model;
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
    internal class AnimalMenu : IMenu
    {
        const ConsoleColor BG = ConsoleColor.Black;
        const ConsoleColor BG_ACTIVE = ConsoleColor.DarkYellow;
        const ConsoleColor FG = ConsoleColor.DarkYellow;
        const ConsoleColor FG_ACTIVE = ConsoleColor.White;
        const ConsoleColor ERR = ConsoleColor.DarkRed;

        private string userName = UserLogic.currentUser.Name;

        private static int activePosition = 1;
        private static string? welcomeMessage = "Welcome user! Give me your name:";

        private static List<CardItem> animalMenuOptions = new();

        private AnimalLogic _animalLogic;

        public AnimalMenu(AnimalLogic animalLogic)
        {
            _animalLogic = animalLogic;
            animalMenuOptions = CreateMenu();
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

            LogoAndHelpers.SetCursorAndMsgWriteLine(32, $"{userName} pets.", FG);

            Console.WriteLine();

            for (int i = 1; i <= animalMenuOptions.Count; i++)
            {
                if (activePosition == i)
                {
                    Console.BackgroundColor = BG_ACTIVE;
                    Console.ForegroundColor = FG_ACTIVE;
                    if (i == animalMenuOptions.Count)
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
                    Console.WriteLine("{0,-30}", animalMenuOptions.ElementAt(i - 1).CardString);
                    Console.BackgroundColor = BG;
                    Console.ForegroundColor = FG;
                }
                else
                {
                    if (i == animalMenuOptions.Count)
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
                    Console.WriteLine(animalMenuOptions.ElementAt(i - 1).CardString);
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
                    activePosition = (activePosition > 1) ? --activePosition : animalMenuOptions.Count;
                    MenuDisplay();
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    activePosition = (activePosition % animalMenuOptions.Count) + 1;
                    MenuDisplay();
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    activePosition = animalMenuOptions.Count;
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
                animalAddCard.DisplatAnimalAddCard();
            }
            else if (activePosition == animalMenuOptions.Count)
            {
                MenuExit();
            }
            else if (animalMenuOptions.ElementAt(activePosition - 1).CardString != "No more animals to show")
            {
                var appointmentLogic = new AppointmentLogic();
                var animalCard = new AnimalCard(_animalLogic.GetById(animalMenuOptions.ElementAt(activePosition - 1).Id), _animalLogic, appointmentLogic);
                animalCard.StartAnimalCard();
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

            var animalList = _animalLogic.GetAnimalList().Where(a => a.UserId == UserLogic.currentUser.Id);
            var menu = new List<CardItem>();

            for (int i = 1; i <= animalList.Count() + 3; i++)
            {
                if (i == 1)
                {
                    menu.Add(new CardItem(i, "Add your Animal"));
                }
                else if (i == animalList.Count() + 3)
                {
                    menu.Add(new CardItem(i, "Back"));
                }
                else if (i >= animalList.Count() + 2)
                {
                    menu.Add(new CardItem(i, "No more animals to show"));
                }
                else
                {
                    menu.Add(new CardItem(animalList.ElementAt(i - 2).Id, $"{animalList.ElementAt(i - 2).AnimalCategory.Specie}: {animalList.ElementAt(i - 2).Name}, Age:{_animalLogic.Age(animalList.ElementAt(i - 2))}"));
                }
            }

            return menu;
        }

        public List<CardItem> ChooseAnimalForAppointmentMenu()
        {

            var animalList = _animalLogic.GetAnimalList().Where(a => a.UserId == UserLogic.currentUser.Id);
            var menu = new List<CardItem>();

            for (int i = 1; i <= animalList.Count() + 2; i++)
            {
                if (i == animalList.Count() + 2)
                {
                    menu.Add(new CardItem(i, "Back"));
                }
                else if (i >= animalList.Count() + 1)
                {
                    menu.Add(new CardItem(i, "No more animals to show"));
                }
                else
                {
                    menu.Add(new CardItem(animalList.ElementAt(i - 2).Id, $"{animalList.ElementAt(i - 2).AnimalCategory.Specie}: {animalList.ElementAt(i - 2).Name}, Age:{_animalLogic.Age(animalList.ElementAt(i - 2))}"));
                }
            }

            return menu;
        }
    }
}
