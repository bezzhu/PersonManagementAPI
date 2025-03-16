using FluentValidation;
using PersonManagementAPI.DTOs;

namespace PersonManagementAPI.Validations
{
    public class PersonValidator : AbstractValidator<PersonDTO>
    {
        public PersonValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .Length(2, 50).WithMessage("First name must be between 2 and 50 characters")
                .Matches("^[ა-ჰa-zA-Z]+$").WithMessage("First name must contain only Georgian or Latin letters");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters")
                .Matches("^[ა-ჰa-zA-Z]+$").WithMessage("Last name must contain only Georgian or Latin letters");

            RuleFor(p => p.PersonalNumber)
                .NotEmpty().WithMessage("Personal number is required")
                .Length(11).WithMessage("Personal number must be exactly 11 digits")
                .Matches(@"^\d{11}$").WithMessage("Personal number must consist of only digits");

            RuleFor(p => p.BirthDate)
                .LessThan(DateTime.UtcNow.AddYears(-18)).WithMessage("User must be at least 18 years old");

            RuleForEach(p => p.PhoneNumbers).SetValidator(new PhoneNumberValidator());
        }
    }

    public class PhoneNumberValidator : AbstractValidator<PhoneNumberDTO>
    {
        public PhoneNumberValidator()
        {
            RuleFor(p => p.PhoneType)
                .IsInEnum().WithMessage("Phone type must be 'Mobile', 'Office', or 'Home'");

            RuleFor(p => p.Number)
                .NotEmpty().WithMessage("Phone number is required")
                .Length(4, 50).WithMessage("Phone number must be between 4 and 50 characters");
        }
    }
}