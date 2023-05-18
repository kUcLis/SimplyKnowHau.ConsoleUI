using SimplyKnowHau.Data.Model;
using SimplyKnowHau.Logic.Logic;
using SimplyKnowHau.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
