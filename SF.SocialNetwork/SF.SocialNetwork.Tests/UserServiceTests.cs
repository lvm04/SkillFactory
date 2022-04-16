using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DAL.Entities;
using SocialNetwork.DAL.Repositories;
using Moq;

namespace SF.SocialNetwork.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        UserService _userService = new UserService();

        #region Проверка полей при регистрации
        [TestMethod]
        public void Register_AddUserWithoutFirstName_Exception()
        {
            var invalidUser = new UserRegistrationData { FirstName = "", LastName = "LastName", Password = "12345678", Email = "eprst@mail.ru" };
            var ex = Assert.ThrowsException<ArgumentNullException>(() => _userService.Register(invalidUser));
            Assert.AreEqual("Имя не заполнено (Parameter 'FirstName')", ex.Message);
        }

        [TestMethod]
        public void Register_AddUserWithoutLastName_Exception()
        {
            var invalidUser = new UserRegistrationData { FirstName = "FirstName", LastName = "", Password = "12345678", Email = "eprst@mail.ru" };
            var ex = Assert.ThrowsException<ArgumentNullException>(() => _userService.Register(invalidUser));
            Assert.AreEqual("Фамилия не заполнена (Parameter 'LastName')", ex.Message);
        }

        [TestMethod]
        public void Register_AddUserWithoutPassword_Exception()
        {
            var invalidUser = new UserRegistrationData { FirstName = "FirstName", LastName = "LastName", Password = "", Email = "eprst@mail.ru" };
            var ex = Assert.ThrowsException<ArgumentNullException>(() => _userService.Register(invalidUser));
            Assert.AreEqual("Пароль не заполнен (Parameter 'Password')", ex.Message);
        }

        [TestMethod]
        public void Register_AddUserWithoutEmail_Exception()
        {
            var invalidUser = new UserRegistrationData { FirstName = "FirstName", LastName = "LastName", Password = "12345678", Email = "" };
            var ex = Assert.ThrowsException<ArgumentNullException>(() => _userService.Register(invalidUser));
            Assert.AreEqual("Почта не заполнена (Parameter 'Email')", ex.Message);
        }

        [TestMethod]
        public void Register_AddUserWithShortPassword_Exception()
        {
            var invalidUser = new UserRegistrationData { FirstName = "FirstName", LastName = "LastName", Password = "123456", Email = "eprst@mail.ru" };
            var ex = Assert.ThrowsException<ArgumentNullException>(() => _userService.Register(invalidUser));
            Assert.AreEqual("Пароль слишком короткий (Parameter 'Password.Length')", ex.Message);
        }

        [TestMethod]
        public void Register_AddUserWithNoValidEmail_Exception()
        {
            var invalidUser = new UserRegistrationData { FirstName = "FirstName", LastName = "LastName", Password = "12345678", Email = "eprst_mail.ru" };
            var ex = Assert.ThrowsException<ArgumentNullException>(() => _userService.Register(invalidUser));
            Assert.AreEqual("Почтовый адрес не соответствует формату (Parameter 'Email')", ex.Message);
        }

        [TestMethod]
        public void Register_AddUserAlreadyExist_Exception()
        {
            var mock = new Mock<IUserRepository>();
            mock.Setup(m => m.FindByEmail("eprst@mail.ru")).Returns(new UserEntity() { firstname = "FirstName", lastname = "LastName" });
            UserService userService = new UserService(mock.Object);

            var invalidUser = new UserRegistrationData { FirstName = "FirstName", LastName = "LastName", Password = "12345678", Email = "eprst@mail.ru" };
            var ex = Assert.ThrowsException<ArgumentNullException>(() => userService.Register(invalidUser));
            Assert.AreEqual("Пользователь уже существует (Parameter 'AlreadyExists')", ex.Message);
        }
        #endregion


    }
}
