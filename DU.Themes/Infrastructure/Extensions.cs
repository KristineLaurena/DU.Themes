using DU.Themes.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using System.Data;
using System;
using AutoMapper;

namespace DU.Themes.Infrastructure
{
    /// <summary>
    /// Application Extensions for Casting, Validating,
    /// Searching by Id, Transaction Opening, Touching entity
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Extensions for finding special entity by Id
        /// </summary>
        /// <typeparam name="T">any entity defined in <see cref="ApplicationDbContext"/> from <see cref="Models"/></typeparam>
        /// <param name="source">sortable source</param>
        /// <param name="Id">Entity Id</param>
        /// <returns>If source contains entity with given id, then return entity, otherwise default value (for many cases it's null)</returns>
        public static T ById<T>(this IDbSet<T> source, long Id)
            where T : EntityBase
        {
            return source.FirstOrDefault(x => x.Id == Id);
        }

        /// <summary>
        /// Extensions for finding special entity by Id
        /// </summary>
        /// <typeparam name="T">any entity defined in <see cref="ApplicationDbContext"/> from <see cref="Models"/></typeparam>
        /// <param name="source">sortable source</param>
        /// <param name="Id">Entity Id</param>
        /// <returns>If source contains entity with given id, then returns Task of entity entity, otherwise Task of default value(for many cases it's null)</returns>
        public static Task<T> ByIdAsync<T>(this IDbSet<T> source, long Id)
            where T : EntityBase
        {
            return source.FirstOrDefaultAsync(x => x.Id == Id);
        }

        /// <summary>
        /// Performs Validation based on FluentValidation rules from <see cref="DU.Themes.ValidaitonApiFilter"/>
        /// <para>Throws <see cref="ValidationException"/> if any mistake occurs</para>
        /// </summary>
        /// <typeparam name="T">type  derived from <see cref="EntityBase"/> class</typeparam>
        /// <param name="entity">Entity to be valdiated</param>
        /// <param name="validator">Validator instance</param>
        public static void Validate<T>(this T entity, AbstractValidator<T> validator)
            where T : EntityBase
        {
            var result = validator.Validate(entity);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
        }

        /// <summary>
        /// Opens Transaction, by default in <see cref="IsolationLevel.ReadCommitted"/>
        /// </summary>
        /// <param name="ctx">context</param>
        /// <param name="isolation">Isolation Level</param>
        /// <returns><see cref="DbContextTransaction"/> instance</returns>
        public static DbContextTransaction BeginTran(
            this ApplicationDbContext ctx,
            IsolationLevel isolation = IsolationLevel.ReadCommitted)
        {
            return ctx.Database.BeginTransaction(isolation);
        }

        /// <summary>
        /// Assigns <see cref="EntityBase.TouchTime"/> to <see cref="DateTime.UtcNow"/>
        /// </summary>
        /// <typeparam name="T">Type derived from <see cref="EntityBase"/></typeparam>
        /// <param name="entity">Entity tobe "Touched"</param>
        public static void Touch<T>(this T entity)
            where T : EntityBase
        {
            entity.TouchTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Converts entity from one type to another, i.e. "Maps" it based on mappings or <see cref="Profile"/>.
        /// If Source entity doesn't have Destinations type properties those properties have default value
        /// e.g. <see cref="int"/> -> 0, <see cref="DateTime"/> -> <see cref="DateTime.MinValue"/>, reference type -> <see cref="null"/>
        /// </summary>
        /// <typeparam name="TSource">Source entity type</typeparam>
        /// <typeparam name="TDestination">Destinatio nentity type</typeparam>
        /// <param name="source">source entity</param>
        /// <returns>entity of Destination type with properties obtained from entity of source type</returns>
        public static TDestination CastTo<TSource, TDestination>(this TSource source)
            where TDestination : class
        {            
            return Mapper.Map<TSource, TDestination>(source);
        }

        public static string FromResource<T>(this T source)
            where T : struct
        {
            var propName = $"{typeof(T).Name}_{source.ToString()}";

            return DU.Themes.Models.ModelResources.ResourceManager.GetString(propName);
        }
    }
}