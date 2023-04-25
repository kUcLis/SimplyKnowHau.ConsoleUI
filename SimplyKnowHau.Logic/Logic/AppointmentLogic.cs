using SimplyKnowHau.Data.Model;
using SimplyKnowHau.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyKnowHau.Logic.Logic
{
    public class AppointmentLogic
    {
        private static int _idCounter = DataMenager.Appointments.Max(c => c.Id);

        private static List<Appointment>? _appointments = DataMenager.Appointments;

        public Appointment AddAppointment(int userid, int animalid, DateTime date, string description, string recipe)
        {
            int id = GetNextId();
            var appointment = new Appointment(id, userid, animalid, date, description, recipe);
            DataMenager.Appointments.Add(appointment);
            return appointment;
        }

        public Appointment GetById(int id)
        {

            return _appointments.FirstOrDefault(c => c.Id == id);

        }

        public Appointment GetByAnimalId(int animalid)
        {

            return _appointments.FirstOrDefault(c => c.AnimalId == animalid);

        }

        public Appointment GetLastByAnimalId(int animalId)
        {
            return _appointments.LastOrDefault(a => a.AnimalId == animalId);
        }

        public List<CardItem> UserIdToMenu()
        {

            var appointmentList = _appointments.Where(c => c.UserId == UserLogic.currentUser.Id);
            var menu = new List<CardItem>();

            for (int i = 1; i <= appointmentList.Count() + 3; i++)
            {
                if (i == 1)
                {
                    menu.Add(new CardItem(i, "Make an appointment"));
                }
                else if (i == appointmentList.Count() + 3)
                {
                    menu.Add(new CardItem(i, "Back"));
                }
                else if (i >= appointmentList.Count() + 2)
                {
                    menu.Add(new CardItem(i, "No more appointments to show"));
                }
                else
                {
                   // menu.Add(new CardItem(appointmentList.ElementAt(i - 2).Id, $"{appointmentList.ElementAt(i - 2).Date.ToShortDateString()}: {AnimalLogic.GetById(appointmentList.ElementAt(i - 2).AnimalId).Name}"));
                }
            }

            return menu;
        }



        private static int GetNextId()
        {
            return ++_idCounter;
        }
    }
}

