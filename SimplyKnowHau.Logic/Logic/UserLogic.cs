using SimplyKnowHau.Data;
using SimplyKnowHau.Data.Model;
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
        public User AddUser(string name)
        {
            int id = GetNextId();
            var user = new User(id, name);
            _users.Add(user);
            DataMenager.SaveList(_users, "Users.json");
            return user;
        }

        public User GetById(int id)
        {

            return _users.First(c => c.Id == id);

        }

        public User GetByName(string name)
        {

            return _users.FirstOrDefault(c => c.Name == name);

        }
        public User SetCurrentUser(User? user)
        {
            currentUser = user;
            return currentUser;
        }

        public User GetCurrentUser()
        {
            return currentUser ?? throw new Exception("Not Found");
        }

        private int GetNextId()
        {
            return ++_idCounter;
        }
    }
}
