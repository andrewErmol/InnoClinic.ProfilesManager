using FluentValidation;
using ProfilesManager.Contracts.RequestEntity;

namespace ProfilesManager.Presentation.Validators
{
    public class DoctorForRequestValidator : AbstractValidator<DoctorForRequest>
    {
        public DoctorForRequestValidator()
        {
            RuleFor(doctor => doctor.FirstName).NotNull().NotEmpty();
            RuleFor(doctor => doctor.LastName).NotNull().NotEmpty();
            RuleFor(doctor => doctor.MiddleName).NotNull().NotEmpty();
            RuleFor(doctor => doctor.DateOfBirth).NotNull().NotEmpty().GreaterThan(new DateTime(1900, 1, 1));
            RuleFor(doctor => doctor.AccountId).NotNull().NotEmpty();
            RuleFor(doctor => doctor.SpecializationId).NotNull().NotEmpty();
            RuleFor(doctor => doctor.OfficeId).NotNull().NotEmpty();
            RuleFor(doctor => doctor.CareerStartYear).NotNull().NotEmpty().GreaterThan(doctor => doctor.DateOfBirth.AddYears(18));
            RuleFor(doctor => doctor.Status).NotNull().NotEmpty();
        }        
    }
}
