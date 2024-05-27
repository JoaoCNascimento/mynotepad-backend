using Microsoft.Extensions.Logging;
using MyNotepad.Domain.DTOs;
using MyNotepad.Domain.Exceptions;
using MyNotepad.Domain.Interfaces.Repositories;

namespace MyNotepad.Domain.Utils
{
    // Deprecated, this class won't be used for now
    // TODO refactor if necessary to do aditional user validations in future
    [Obsolete("Refactor if necessary to do aditional and specific user validations in the future")]
    public class ValidateUserUtil(UserDTO user, IUserRepository userRepository)
    {
        private ILogger _logger = new LoggerFactory().CreateLogger<ValidateUserUtil>();

        private const string DEFAULT_NAME_ERROR_FEEDBACK = "Name is empty or didn't reach the minimum length of 3 chars.";
        private const string DEFAULT_EMAIL_ERROR_FEEDBACK = "Email is invalid or unavailable.";
        private const string DEFAULT_PASSWORD_ERROR_FEEDBACK = "Password is empty or didn't reach the minimum length of 6 chars.";
        private const string DEFAULT_PASSWORD_CONFIRMATION_ERROR_FEEDBACK = "Entered passwords don't match.";
        private readonly Func<string, string> DEFAULT_TOO_LONG_FIELD_MESSAGE = (string fieldName) =>  $"{fieldName} is too long, maximum length is 255 chars.";
        private const int MAX_TEXT_FIELDS_LENGTH = 255;

        public void Validate()
        {
            ValidateName(user.Name);
            ValidatePassword(user.Password, string.Empty); // always returns false
            ValidateEmail(user.Email);
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name.Trim()) || name.Trim().Length < 3)
                throw new InvalidFieldException(DEFAULT_NAME_ERROR_FEEDBACK, nameof(name), name);

            if (name.Length > MAX_TEXT_FIELDS_LENGTH)
                throw new InvalidFieldException(DEFAULT_TOO_LONG_FIELD_MESSAGE(nameof(name)), nameof(name), name);
        }

        private void ValidatePassword(string password, string passwordConfirm)
        {
            if (string.IsNullOrEmpty(password.Trim()) || password.Length < 6)
                throw new InvalidFieldException(DEFAULT_PASSWORD_ERROR_FEEDBACK, nameof(password), password);

            if (password.Length > MAX_TEXT_FIELDS_LENGTH)
                throw new InvalidFieldException(DEFAULT_TOO_LONG_FIELD_MESSAGE(nameof(password)), nameof(password), password);

            if (!password.Equals(passwordConfirm))
                throw new InvalidFieldException(DEFAULT_PASSWORD_CONFIRMATION_ERROR_FEEDBACK, nameof(passwordConfirm), passwordConfirm);
        }

        private void ValidateEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);

                if (addr.Address != email)
                    throw new FormatException("Invalid email address format.");

                if (addr.Address.Length > MAX_TEXT_FIELDS_LENGTH)
                    throw new FormatException(DEFAULT_TOO_LONG_FIELD_MESSAGE(nameof(email)));

                if (userRepository.Exists(email))
                    throw new Exception("The entered email is already being used.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error ocurred when trying to validate the email address. Error: " + ex.Message);
                throw new InvalidFieldException(DEFAULT_EMAIL_ERROR_FEEDBACK, nameof(email), email);
            }
        }
    }
}