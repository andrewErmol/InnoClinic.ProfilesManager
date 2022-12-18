using FluentValidation;
using ProfilesManager.Contracts.RequestEntity;

namespace ProfilesManager.Presentation.Validators
{
    public class PatientForRequestValidator : AbstractValidator<PatientForRequest>
    {
        public PatientForRequestValidator()
        {
            RuleFor(patient => patient.FirstName).NotNull().NotEmpty();
            RuleFor(patient => patient.LastName).NotNull().NotEmpty();
            RuleFor(patient => patient.MiddleName).NotNull().NotEmpty();
            RuleFor(patient => patient.DateOfBirth).NotNull().NotEmpty().GreaterThan(new DateTime(1900, 1, 1));
            RuleFor(patient => patient.AccountId).NotNull().NotEmpty();
        }        
    }
}
