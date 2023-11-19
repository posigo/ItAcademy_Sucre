using FluentValidation;
using Sucre_DataAccess.Repository;
using Sucre_Models;

namespace Sucre.FluentValidation
{
    public class UserRegisterValidator: AbstractValidator<AppUserRegisterM>
    {
        public UserRegisterValidator()
        {
            RuleFor(model => model.Email)
                .NotEmpty()
                .EmailAddress()
                .NotEqual("");
        }

    }
}
