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
            _appointments.Add(appointment);
            DataMenager.SaveList(_appointments, "Appointments.json");
            return appointment;
        }

        public Appointment GetById(int id)
        {

            return _appointments.FirstOrDefault(c => c.Id == id);

        }

        public List<Appointment> GetAppointmentsList()
        {
            return _appointments;
        }

        public Appointment GetByAnimalId(int animalid)
        {

            return _appointments.FirstOrDefault(c => c.AnimalId == animalid);

        }

        public Appointment GetLastByAnimalId(int animalId)
        {
            return _appointments.LastOrDefault(a => a.AnimalId == animalId);
        }

        public void Delete(Appointment appointment)
        {
            DataMenager.Appointments.Remove(appointment);
            DataMenager.SaveList(DataMenager.Appointments, "Appointments.json");
        }
        private static int GetNextId()
        {
            return ++_idCounter;
        }
    }
}

