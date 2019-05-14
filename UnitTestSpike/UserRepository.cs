using System.Collections.Generic;
using System.Linq;

namespace UnitTestSpike
{
    public class UserRepository : IUserRepository
    {
        private static IList<User> _users;

        static UserRepository()
        {
            _users = new List<User>
            {
                new User{Login = "Admin", Password = "Admin123", FirstName = "Jan", LastName = "Kowalski"}
            };
        }
        public User GetUserByLogin(string login)
        {
            return _users.SingleOrDefault(x => x.Login == login);
        }
    }
}