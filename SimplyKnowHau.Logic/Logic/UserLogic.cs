using SimplyKnowHau.Data;
using SimplyKnowHau.Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyKnowHau.Logic.Logic
{
    public class UserLogic
    {
        private static int _idCounter = DataMenager.Users.Max(c => c.Id);

        private static List<User>? _users = DataMenager.Users;

        public static User? currentUser = null;
        public static User AddUser(string name)
        {
            int id = GetNextId();
            var user = new User(id, name);
            DataMenager.Users.Add(user);
            return user;
        }

        public static User GetById(int id)
        {

            return _users.First(c => c.Id == id);

        }

        public static User GetByName(string name)
        {

            return _users.FirstOrDefault(c => c.Name == name);

        }
        public static User SetCurrentUser(User? user)
        {
            currentUser = user;
            return currentUser;
        }

        public static User GetCurrentUser()
        {
            return currentUser ?? throw new Exception("Not Found");
        }

        private static int GetNextId()
        {
            return ++_idCounter;
        }
    }
}
