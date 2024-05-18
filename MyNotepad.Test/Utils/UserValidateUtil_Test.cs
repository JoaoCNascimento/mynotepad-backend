using Microsoft.Extensions.DependencyInjection;
using MyNotepad.Domain.DTOs;
using MyNotepad.Domain.Exceptions;
using MyNotepad.Domain.Interfaces.Repositories;
using MyNotepad.Domain.Utils;

namespace MyNotepad.Test.Utils
{
    public class UserValidateUtil_Test : IClassFixture<DependencySetupFixture>
    {
        private ServiceProvider _serviceProvide;

        public UserValidateUtil_Test(DependencySetupFixture fixture)
        {
            _serviceProvide = fixture.ServiceProvider;
        }

        [Fact(DisplayName = "Should validate user sucessfully")]
        public void ShouldValidateUserSuccessfully()
        {
            using (var scope = _serviceProvide.CreateScope())
            {
                IUserRepository userRepository = scope.ServiceProvider.GetService<IUserRepository>()!;

                var userToValidate = new UserDTO
                {
                    Name = "Test",
                    Email = "Test@gmail.com",
                    Password = "password",
                    PasswordConfirmation = "password"
                };

                try
                {
                    new ValidateUserUtil(userToValidate, userRepository).Validate();
                }
                catch (Exception ex)
                {
                    Assert.True(false, $"An unexpected exception ocurred: {ex.Message}");
                }
            }
        }

        [Fact(DisplayName = "Should invalidate user name")]
        public void ShouldInvalidateUserName()
        {
            using (var scope = _serviceProvide.CreateScope())
            {
                IUserRepository userRepository = scope.ServiceProvider.GetService<IUserRepository>()!;

                var userToValidate = new UserDTO
                {
                    Name = "Te", // This name should not pass the validation
                    Email = "teste@gmail.com",
                    Password = "password",
                    PasswordConfirmation = "password"
                };

                Assert.Throws<InvalidFieldException>(new ValidateUserUtil(userToValidate, userRepository).Validate);
            }
        }

        [Fact(DisplayName = "Should Invalidate Email")]
        public void ShouldInvalidateEmail()
        {
            using (var scope = _serviceProvide.CreateScope())
            {
                IUserRepository userRepository = scope.ServiceProvider.GetService<IUserRepository>()!;

                var userToValidate = new UserDTO
                {
                    Name = "Test",
                    Email = "@gmail.com", // This email should not pass the validation
                    Password = "password",
                    PasswordConfirmation = "password"
                };

                Assert.Throws<InvalidFieldException>(new ValidateUserUtil(userToValidate, userRepository).Validate);
            }
        }

        [Fact(DisplayName = "Should Invalidate User Password")]
        public void ShouldInvalidateUserPassword()
        {
            using (var scope = _serviceProvide.CreateScope())
            {
                IUserRepository userRepository = scope.ServiceProvider.GetService<IUserRepository>()!;

                var userToValidate = new UserDTO
                {
                    Name = "Test",
                    Email = "Test@gmail.com",
                    Password = "password",
                    PasswordConfirmation = "different-password"
                };

                Assert.Throws<InvalidFieldException>(new ValidateUserUtil(userToValidate, userRepository).Validate);
            }
        }

        [Fact(DisplayName = "Should Invalidate User Name That Overflows The Max Length")]
        public void ShouldInvalidateUserNameThatOverflowsTheMaxLength()
        {
            using (var scope = _serviceProvide.CreateScope())
            {
                IUserRepository userRepository = scope.ServiceProvider.GetService<IUserRepository>()!;

                var userToValidate = new UserDTO
                {
                    Name = string.Concat(Enumerable.Repeat("T", 256)),
                    Email = "Test@gmail.com",
                    Password = "password",
                    PasswordConfirmation = "password"
                };

                Assert.Throws<InvalidFieldException>(new ValidateUserUtil(userToValidate, userRepository).Validate);
            }
        }

        [Fact(DisplayName = "Should Invalidate Email That Overflows The Max Length")]
        public void ShouldInvalidateEmailThatOverflowsTheMaxLength()
        {
            using (var scope = _serviceProvide.CreateScope())
            {
                IUserRepository userRepository = scope.ServiceProvider.GetService<IUserRepository>()!;

                var userToValidate = new UserDTO
                {
                    Name = "Test",
                    Email = string.Concat(Enumerable.Repeat("T", 256)) + "@gmail.com",
                    Password = "password",
                    PasswordConfirmation = "password"
                };

                Assert.Throws<InvalidFieldException>(new ValidateUserUtil(userToValidate, userRepository).Validate);
            }
        }
    }
}
