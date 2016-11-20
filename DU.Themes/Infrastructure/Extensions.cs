using DU.Themes.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using System.Data;
using System;

namespace DU.Themes.Infrastructure
{
    public static class Extensions
    {
        public static T ById<T>(this IDbSet<T> source, long Id)
            where T : EntityBase
        {
            return source.FirstOrDefault(x => x.Id == Id);
        }

        public static Task<T> ByIdAsync<T>(this IDbSet<T> source, long Id)
            where T : EntityBase
        {
            return source.FirstOrDefaultAsync(x => x.Id == Id);
        }


        public static void Validate<T>(this T entity, AbstractValidator<T> validator)
            where T : EntityBase
        {
            var result = validator.Validate(entity);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }

        public static DbContextTransaction BeginTran(
            this ApplicationDbContext ctx,
            IsolationLevel isolation = IsolationLevel.ReadCommitted)
        {
            return ctx.Database.BeginTransaction(isolation);
        }

        public static void Touch<T>(this T entity)
            where T : EntityBase
        {
            entity.TouchTime = DateTime.UtcNow;
        }
    }
}