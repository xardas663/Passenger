using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Passenger.Core.Repositories;
using System.Threading.Tasks;
using Xunit;
using AutoMapper;
using Passenger.Infrastructure.Services;
using Passenger.Core.Domain;
using System;

namespace Passenger.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public async Task register_async_should_invoke_add_async_on_repository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var encrypterMock = new Mock<IEncrypter>();
            var userService = new UserService(userRepositoryMock.Object,encrypterMock.Object, mapperMock.Object);
            await userService.RegisterAsync(Guid.NewGuid(),"ziomek@email.com","user55","secret","user");
            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }
    }
}
