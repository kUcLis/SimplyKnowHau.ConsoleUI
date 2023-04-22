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
            return JsonConvert.DeserializeObject<List<T>>(itemsSerialized) ?? new List<T>();
        }

    }
}