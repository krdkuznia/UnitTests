using System;
using Moq;
using NUnit.Framework;

namespace UnitTestSpike.Tests
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private AuthenticationService _sut;
        private Mock<IUserRepository> _userRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
            _sut = new AuthenticationService(_userRepositoryMock.Object);
        }

        [Test]
        public void Login_LoginHasLessThan3Chars_ThrowsArgumentException()
        {
            //Arrange
            string login = "ab";
            string password = "admin";
          
            //Act, Assert
            Assert.Throws<ArgumentException>(() => _sut.Login(login, password));
        }

        [Test]
        public void Login_LoginCorrectAndUserNotFound_ThrowsEntityNotFoundException()
        {
            //Arrange
            string login = "admin";
            string password = "admin123";
            _userRepositoryMock.Setup(x => x.GetUserByLogin(login)).Returns((User)null).Verifiable();
            
            //Act, Assert
            Assert.Throws<EntityNotFoundException>(() => _sut.Login(login, password));
            _userRepositoryMock.Verify();
        }

        [Test]
        public void Login_LoginAndPasswordMatches_ReturnsToken()
        {
            //Arrange
            string login = "admin";
            string password = "admin123";
            _userRepositoryMock.Setup(x => x.GetUserByLogin(login))
                .Returns(new User {Login = login, Password = password});
            
            //Act
            var result = _sut.Login(login, password);

            //Assert
            Assert.NotNull(result);
            Assert.AreEqual(Guid.NewGuid().ToString().Length, result.Length);
        }
    }
}