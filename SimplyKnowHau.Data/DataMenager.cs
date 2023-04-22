using SimplyKnowHau.Data.Model;
using System.Text.Json;

namespace SimplyKnowHau.Data
{
    public static class DataMenager
    {
        public static List<Animal>? Animals { get; set; } = GetList<Animal>("Animals.json");
        public static List<User>? Users { get; set; } = GetList<User>("Users.json");
        public static List<Appointment>? Appointments { get; set; } = GetList<Appointment>("Appointments.json");
        public static List<Species>? Species { get; set; } = GetList<Species>("Species.json");

        public static List<T> GetList<T>(string fileName)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName);
            string itemsSerialized = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(itemsSerialized) ?? new List<T>();
        }

        public static void SaveListAnimal(List<Animal> animalsList)
        {
            string fileName = "Animal.json";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName);

            string itemsSerialized = JsonSerializer.Serialize(animalsList);

            File.WriteAllText(filePath, itemsSerialized);


        }
    }
}