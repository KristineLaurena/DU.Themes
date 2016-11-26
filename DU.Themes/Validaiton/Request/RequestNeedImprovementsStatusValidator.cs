using FluentValidation;

namespace DU.Themes.ValidaitonApiFilter.Request
{
    public class RequestNeedImprovementsStatusValidator : AbstractValidator<Models.Request>
    {
        public RequestNeedImprovementsStatusValidator()
        {
            RuleFor(x => x.Status).Equal(Models.RequestStatus.NeedImprovements);
        }
    }
}