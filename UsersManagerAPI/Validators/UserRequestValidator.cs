using FluentValidation;
using System.Globalization;
using ClientRegistryAPI.Requests;

namespace ClientRegistryAPI.Validators
{
    /// <summary>
    /// Validating CommonUserRequest
    /// </summary>
    public class UserRequestValidator : AbstractValidator<CommonUserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(user => user.Name).NotNull().NotEmpty().MaximumLength(50); // Adjust the maximum length as needed
            RuleFor(user => user.Email).NotNull().NotEmpty().EmailAddress();
        }
    }
}
