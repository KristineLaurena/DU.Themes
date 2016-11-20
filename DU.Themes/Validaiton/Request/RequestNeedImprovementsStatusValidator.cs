using FluentValidation;

namespace DU.Themes.Validaiton.Request
{
    public class RequestNeedImprovementsStatusValidator : AbstractValidator<Models.Request>
    {
        public RequestNeedImprovementsStatusValidator()
        {
            RuleFor(x => x.Status).Equal(Models.RequestStatus.NeedImprovements);
        }
    }
}