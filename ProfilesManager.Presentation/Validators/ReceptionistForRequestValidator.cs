using FluentValidation;
using ProfilesManager.Presentation.RequestEntity;

namespace ProfilesManager.Presentation.Validators
{
    public class ReceptionistForRequestValidator : AbstractValidator<ReceptionistForRequest>
    {
        public ReceptionistForRequestValidator()
        {
            RuleFor(receptionist => receptionist.FirstName).NotNull().NotEmpty();
            RuleFor(receptionist => receptionist.LastName).NotNull().NotEmpty();
            RuleFor(receptionist => receptionist.MiddleName).NotNull().NotEmpty();
            RuleFor(receptionist => receptionist.DateOfBirth).NotNull().NotEmpty().GreaterThan(new DateTime(1900, 1, 1));
            RuleFor(receptionist => receptionist.AccountId).NotNull().NotEmpty();
            RuleFor(doctor => doctor.OfficeId).NotNull().NotEmpty();
        }
    }
}
