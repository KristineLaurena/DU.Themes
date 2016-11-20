using DU.Themes.Infrastructure;
using DU.Themes.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using DU.Themes.Validaiton.Request;
using System.Threading.Tasks;

namespace DU.Themes.Api
{
    [Authorize]
    public class RequestApiController : ApiController
    {
        private ApplicationUserManager _userManager;

        public RequestApiController()
        {
            
        }

        public RequestApiController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {           
            get
            {               
                return _userManager ?? this.Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [HttpPost]
        [Authorize(Roles = Roles.Student)]
        //[AllowAnonymous]
        public async void test()
        {
            var userId = Convert.ToInt64(this.User.Identity.GetUserId());     

            using (var ctx = new ApplicationDbContext())
            {
                using (var tran = ctx.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
                {

                    var requestDB = new Request
                    {
                        Student = ctx.Users.FirstOrDefault(x => x.Id == userId),
                        CreatedOn = DateTime.UtcNow,
                        Teacher = ctx.Users.FirstOrDefault(x => x.Id == 2),
                        ThemeLV = "super tema",
                        ThemeENG = "super theme",
                        Status = RequestStatus.New,
                        RespondedOn = DateTime.UtcNow,
                    };
                    requestDB.Touch();
                    // TODO: Validation!
                    requestDB.Validate(new NewRequestValidator(ctx));

                    ctx.Requests.Add(requestDB);

                    await ctx.SaveChangesAsync();
                    tran.Commit();
                }
            }
            
        }


        [HttpPost]
        [Authorize(Roles = Roles.Student)]
        public async void Create(Request request)
        {
            var user = this.UserManager.FindById(Convert.ToInt64(this.User.Identity.GetUserId()));

            var requestDB = new Request
            {
                Student = user,
                CreatedOn = DateTime.UtcNow,
                Teacher = this._userManager.FindById(Convert.ToInt64(request?.Teacher?.Id)),
                ThemeLV = request.ThemeLV,
                ThemeENG = request.ThemeENG,
                Status = RequestStatus.New
            };

            var ctx = new ApplicationDbContext();
            using (var tran = ctx.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
            {
                // TODO: Validation!
                request.Validate(new NewRequestValidator(ctx));
                ctx.Requests.Add(requestDB);

                await ctx.SaveChangesAsync().ContinueWith((Task<int> result) =>
                {
                    tran.Commit();
                });
                            
            }
        }

        [HttpPost]
        [Authorize(Roles = Roles.Student + ", " + Roles.Teacher)]
        //public async Task<IHttpActionResult> Update(Request request)
        public async void Update(Request request)
        {
            var userId = Convert.ToInt64(this.User.Identity.GetUserId());

            if (request.Teacher.Id != userId && request.Student.Id != userId)
            {
                //TODO right exception
                throw new Exception("not in role");
            }

            var ctx = new ApplicationDbContext();
            using (var tran = ctx.BeginTran())
            {
                var requestDB = await ctx.Requests.ByIdAsync(request.Id);

                this.UpdateRequest(requestDB, request, ctx);                
                requestDB.Touch();
                requestDB.Validate(new RequestValidator());

                await ctx.SaveChangesAsync();
                tran.Commit();
            }
        }

        private void UpdateRequest(Request requestDB, Request request, ApplicationDbContext ctx)
        {
            if (User.IsInRole(Roles.Teacher) || User.IsInRole(Roles.SystemAdministrator))
            {
                this.FullUpdate(requestDB, request, ctx);
            }

            if (User.IsInRole(Roles.Student))
            {
                // Student can update theme only if it has status - Need Improvements
                this.UpdateThemes(requestDB, request);
            }
        }

        private void FullUpdate(Request requestDB, Request request, ApplicationDbContext ctx)
        {
            var teacherId = Convert.ToInt64(request.Teacher?.Id);
            requestDB.Teacher = ctx.Users.FirstOrDefault(x => x.Id == teacherId);

            var studentId = request.Student?.Id;
            requestDB.Student = ctx.Users.FirstOrDefault(x => x.Id == studentId);

            requestDB.RespondedOn = DateTime.UtcNow;
            requestDB.SeenByStudent = false;
            requestDB.ThemeENG = request.ThemeENG;
            requestDB.ThemeLV = request.ThemeLV;
        }

        private void UpdateThemes(Request requestDB, Request request)
        {
            requestDB.Validate(new RequestNeedImprovementsStatusValidator());

            requestDB.ThemeENG = request.ThemeENG;
            requestDB.ThemeLV = request.ThemeLV;
            requestDB.SeenByStudent = true;
            requestDB.SeenByTeacher = false;
        }

       
    }
}
