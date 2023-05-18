using SimplyKnowHau.Data.Model;
using SimplyKnowHau.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyKnowHau.Logic.Logic
{
    public class AnimalLogic
    {
        private static int _idCounter = DataMenager.Animals.Max(c => c.Id);

        private static readonly List<Animal>? _animals = DataMenager.Animals;
        public Animal AddAnimal(string name, Species animalCategory, DateTime dateOfBirth)
        {
            int id = GetNextId();
            int userId = UserLogic.currentUser.Id;
            var animal = new Animal(id, userId, name, animalCategory, dateOfBirth);
            _animals.Add(animal);
            DataMenager.SaveList(_animals, "Animals.json");
            return animal;
        }

        public Animal? GetById(int id)
        {
            return _animals.FirstOrDefault(c => c.Id == id);
        }
        
        public List<Animal> GetAnimalList()
        {
            return _animals;
        }
        
        public List<CardItem> ChooseAnimalUserIdToMenu()
        {

            var animalList = _animals.Where(c => c.UserId == UserLogic.currentUser.Id);
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
                    menu.Add(new CardItem(animalList.ElementAt(i - 1).Id, $"{animalList.ElementAt(i - 1).AnimalCategory.Specie}: {animalList.ElementAt(i - 1).Name}, Age:{Age(animalList.ElementAt(i - 1))}"));
                }
            }

            return menu;
        }

        private static int GetNextId()
        {
            return ++_idCounter;
        }

        public int Age(Animal animal)
        {
            int age = DateTime.Now.Year - animal.DateOfBirth.Year;
            if (DateTime.Now.Month < animal.DateOfBirth.Month)
            {
                age--;
            }

            return age;
        }
    }
}
