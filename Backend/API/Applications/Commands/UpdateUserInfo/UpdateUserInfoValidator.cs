using FluentValidation;

namespace API.Application.Commands;

public sealed class UpdateUserInfoValidator : AbstractValidator<UpdateUserInfoCommand>
{
    public UpdateUserInfoValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(30);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(30);
    }
}