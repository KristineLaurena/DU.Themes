using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DU.Themes.Models
{
    public class CustomUserRole : IdentityUserRole<long> { }
    public class CustomUserClaim : IdentityUserClaim<long> { }
    public class CustomUserLogin : IdentityUserLogin<long> { }

    public class CustomRole : IdentityRole<long, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }

    public class CustomUserStore : UserStore<Person, CustomRole, long,
    CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, long, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class Person : IdentityUser<long, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Person, long> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string Year { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentIdentifier { get; set; }
        public string ProgramName { get; set; }
        public string ProgramLevel { get; set; }
        public string StudyForm { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<Person, CustomRole,
    long, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Request> Requests { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<CustomUserRole> CustomUserRole { get; set; }
        public DbSet<CustomUserClaim> CustomUserClaims { get; set; }
        //public DbSet<CustomRole> CustomRoles { get; set; }
        public DbSet<CustomUserLogin> CustomUserLogins { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Customer>()
            //    .HasIndex("IX_Customers_Name",          // Provide the index name.
            //        e => e.Property(x => x.LastName),   // Specify at least one column.
            //        e => e.Property(x => x.FirstName))  // Multiple columns as desired.

            //    .HasIndex("IX_Customers_EmailAddress",  // Supports fluent chaining for more indexes.
            //        IndexOptions.Unique,                // Supports flags for unique and clustered.
            //        e => e.Property(x => x.EmailAddress));

            //modelBuilder.Entity<Request>()
            modelBuilder.Entity<Person>().ToTable("Users");
            modelBuilder.Entity<CustomUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<CustomUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<CustomUserClaim>().ToTable("UserClaims");
            //modelBuilder.Entity<CustomRole>().ToTable("Roles");

        }
    }
}