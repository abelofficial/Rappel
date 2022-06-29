using FluentValidation;

namespace API.Application.Commands;

public sealed class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(30);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(30);
        RuleFor(x => x.Username).NotEmpty().MaximumLength(30);
        RuleFor(p => p.Password).NotEmpty().WithMessage("Your password cannot be empty")
                    .MinimumLength(6).WithMessage("Your password length must be at least 8.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
    }
}