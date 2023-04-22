using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyKnowHau.Data.Model
{
    public class Appointment
    {

        public int Id { get; set; }

        public int UserId { get; set; }

        public int AnimalId { get; set; }

        public DateTime Date { get; set; }

        public string? Description { get; set; }

        public string? Recipe { get; set; }


        public Appointment(int id, int userId, int animalId, DateTime date, string description, string recipe)
        {
            Id = id;
            UserId = userId;
            AnimalId = animalId;
            Date = date;
            Description = description;
            Recipe = recipe;
        }
    }
}
