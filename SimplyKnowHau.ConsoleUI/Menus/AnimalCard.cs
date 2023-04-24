using SimplyKnowHau.Data.Model;
using SimplyKnowHau.Logic;
using SimplyKnowHau.Logic.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyKnowHau.ConsoleUI.Menus
{
    internal class AnimalCard
    {
        const ConsoleColor FG = ConsoleColor.DarkYellow;
        const ConsoleColor FG_ACTIVE = ConsoleColor.White;

        private static List<CardItem>? cardItemsAnimal = new();
        private AnimalLogic _animalLogic;
        

        public AnimalCard(Animal animal, AnimalLogic animalLogic, AppointmentLogic appointmentLogic )
        {
            _animalLogic = animalLogic;
            

            var appointment = appointmentLogic.GetLastByAnimalId(animal.Id);

            cardItemsAnimal.Clear();
            cardItemsAnimal.Add(new CardItem(1, "Animal Id: ", animal.Id.ToString()));
            cardItemsAnimal.Add(new CardItem(2, "Name: ", animal.Name));
            cardItemsAnimal.Add(new CardItem(3, "Specie: ", animal.AnimalCategory.Specie));
            cardItemsAnimal.Add(new CardItem(4, "Age: ", _animalLogic.Age(animal).ToString()));
            cardItemsAnimal.Add(new CardItem(5, "Date of birth: ", animal.DateOfBirth.ToShortDateString()));
            cardItemsAnimal.Add(new CardItem(6, "Date of last visit: ", _appointmentLogic.GetByAnimalId(animal.Id).Date.ToShortDateString()));
            cardItemsAnimal.Add(new CardItem(7, "Description of the visit: ", _appointmentLogic.GetByAnimalId(animal.Id).Description));
            cardItemsAnimal.Add(new CardItem(8, "Receives: ", _appointmentLogic.GetByAnimalId(animal.Id).Recipe));
        }

        public void DisplayCardAnimal()
        {
            LogoAndHelpers.DisplayLogo();

            for (int i = 0; i < cardItemsAnimal.Count; i++)
            {

                Console.SetCursorPosition((Console.WindowWidth - 50) / 2, Console.CursorTop);
                Console.Write($"{cardItemsAnimal.ElementAt(i).CardString}");
                Console.ForegroundColor = FG_ACTIVE;
                Console.Write($"{cardItemsAnimal.ElementAt(i).CardContent}");
                Console.WriteLine();
                Console.ForegroundColor = FG;
            }
        }
    }
}
