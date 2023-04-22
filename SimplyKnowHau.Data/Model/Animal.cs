using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyKnowHau.Data.Model
{
    public class Animal
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public Species AnimalCategory { get; set; }

        public DateTime DateOfBirth { get; set; }


        public Animal(int id, int userId, string name, Species animalCategory, DateTime dateOfBirth)
        {
            Id = id;
            UserId = userId;
            Name = name;
            AnimalCategory = animalCategory;
            DateOfBirth = dateOfBirth;
        }

    }
}
