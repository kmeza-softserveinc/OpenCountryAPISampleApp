using System.Net;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenCountryAPISampleApp.Controllers;
using OpenCountryAPISampleApp.EFModels.UsersModel;

namespace OpenCountryAPISampleApp_UnitTests.ControllersTests
{
    public class LoginControllerTests
    {
        private readonly UsersDbContext _dbContext = Substitute.For<UsersDbContext>();
        private readonly ILogger<LoginController> _logger = Substitute.For<ILogger<LoginController>>();
        private readonly LoginController _controller;


        public LoginControllerTests()
        {
            // Mock the necessary components
            var httpContext = Substitute.For<HttpContext>();
            var authenticationService = Substitute.For<IAuthenticationService>();

            // Set up SignInAsync to simply return a completed Task (indicating success)
            authenticationService.SignInAsync(httpContext, Arg.Any<string>(), Arg.Any<ClaimsPrincipal>(), Arg.Any<AuthenticationProperties>())
                .Returns(Task.CompletedTask);

            // Attach the mocked authentication service to the mocked HttpContext
            httpContext.RequestServices.GetService(typeof(IAuthenticationService))
                .Returns(authenticationService);

            _controller = new LoginController(_dbContext, _logger);
            _controller.ControllerContext.HttpContext = httpContext;

        }


        [Fact]
        public async Task Login_ShouldReturnBadRequest_WhenEmailOrPasswordIsEmpty()
        {
            //Arrange
            //Act
            var result = await _controller.Login(null, "password");
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenUserNotFound()
        {
            //Arrange
            _dbContext.Users.FindAsync("test@email.com").Returns(null as User);

            //Act
            var result = await _controller.Login("test@email.com", "password");

            //Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenUserIsFoundAndPasswordMatches()
        {
            //Arrange
            var user = new User { Name= "Some Name", Email = "test@email.com", Password = "$argon2id$v=19$m=65536,t=2,p=4$b0lXMFA0aGFqaGhCemhNUw$llyKAfjZvkh7C1RZ+nE6gLdvSEs" };
            _dbContext.Users.FindAsync("test@email.com").Returns(user);

            //Act
            var result = await _controller.Login(user.Email, "correct_password");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

    }
    
    
}
