using System;
using System.Security.Authentication;

namespace UnitTestSpike
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string Login(string login, string password)
        {
            var user = _userRepository.GetUserByLogin(login);
            if (user == null)
            {
                throw new EntityNotFoundException("User not found");
            }

            if (user.Password != password)
            {
                throw new AuthenticationException();
            }

            return Guid.NewGuid().ToString();
        }
    }
}