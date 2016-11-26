using DU.Themes.Models;
using System.Collections;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using DU.Themes.Infrastructure;
using System;
using System.Threading;
using DU.Themes.Infrastructure.Comparers;

namespace DU.Themes.Api
{
    [Authorize]
    public class PersonController : ApiController
    {
        /// <summary>
        /// Gets Supervisor list
        /// </summary>
        /// <param name="search">First Name or Last Name</param>
        /// <returns>first 10 of list ordered by last Name</returns>
        [HttpGet]
        public IEnumerable<PersonModel> Supervisors(string search = null)
        {
            if (search != null)
            {
                search = search.TrimEnd().TrimStart();
            }

            //Thread.Sleep(TimeSpan.FromSeconds(3));
            var result = new List<PersonModel>();

            using (var ctx = new ApplicationDbContext())
            {

                var supervisorRole = ctx.Roles.First(x => x.Name == Roles.Teacher);
                var role = ctx.CustomUserRole.First(x => x.RoleId == supervisorRole.Id);

                if (string.IsNullOrEmpty(search))
                {
                    var founded = ctx.Users.Where(x => x.Roles.Where(r => r.RoleId == supervisorRole.Id).Any())
                       .OrderBy(x => x.LastName)
                       .Take(10) // TODO -> 10 from cfg
                       .ToList()
                       .Select(x => x.CastTo<Person, PersonModel>());

                    result.AddRange(founded);
                }
                else
                {
                    var enterances = search.Split(' ');

                    // one query would be too complex and execution time will be enourmously big
                    foreach (var enterance in enterances)
                    {
                        // by FirstName
                        var byFName = ctx.Users
                            .Where(x => x.Roles.Where(r => r.RoleId == supervisorRole.Id).Any())
                            .Where(x => x.FirstName.StartsWith(enterance))
                            .Take(10)
                            .ToList()
                            .Select(x => x.CastTo<Person, PersonModel>());

                        result.AddRange(byFName);

                        // By LastName
                        var byLName = ctx.Users
                            .Where(x => x.Roles.Where(r => r.RoleId == supervisorRole.Id).Any())
                            .Where(x => x.LastName.StartsWith(enterance))
                            .Take(10)
                            .ToList()
                            .Select(x => x.CastTo<Person, PersonModel>());

                        result.AddRange(byLName);
                    }
                }

                return result
                    .Distinct(Comparers<PersonModel>.ModelIdComparer)
                    .OrderBy(x => x.LastName)
                    .Take(10);
            }
        }
    }
}
