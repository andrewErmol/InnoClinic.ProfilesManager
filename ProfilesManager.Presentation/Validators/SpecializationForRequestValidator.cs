using FluentValidation;
using ProfilesManager.Contracts.RequestEntity;

namespace ProfilesManager.Presentation.Validators
{
    public class SpecializationForRequestValidator : AbstractValidator<SpecializationForRequest>
    {
        public SpecializationForRequestValidator()
        {
            RuleFor(receptionist => receptionist.Name).NotNull().NotEmpty();
        }
    }
}
