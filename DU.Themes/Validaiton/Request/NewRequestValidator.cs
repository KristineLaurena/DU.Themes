using DU.Themes.Models;
using FluentValidation;
using System;
using FluentValidation.Results;
using System.Linq;

namespace DU.Themes.ValidaitonApiFilter.Request
{
    public class NewRequestValidator : AbstractValidator<Models.Request>
    {
        public NewRequestValidator(ApplicationDbContext ctx)
        {
            RuleFor(x => x.Student).NotNull();
            RuleFor(x => x.Teacher).NotNull();
            RuleFor(x => x.Status).Equal(Models.RequestStatus.New);
            RuleFor(x => x.SeenByStudent).Equal(false);
            RuleFor(x => x.SeenByTeacher).Equal(false);
            RuleFor(x => x.CreatedOn).NotEqual(DateTime.MinValue);
            //RuleFor(x => x.Student).Must( x => x.Id == Thread)
            Custom(x => OnlyOneActiveRequest(ctx, x));

        }

        private ValidationFailure OnlyOneActiveRequest(ApplicationDbContext ctx, Models.Request request)
        {
            if (ctx.Requests.Where(x => x.Student.Id == request.Student.Id && x.Status != RequestStatus.Cancelled && x.Year == null).Any())
            {
                return new ValidationFailure("", "Already have one");
            }

            else
            {
                return null;
            }
        }
    }
}