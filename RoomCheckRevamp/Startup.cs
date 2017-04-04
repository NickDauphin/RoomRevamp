using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using RoomCheckRevamp.Models;

[assembly: OwinStartupAttribute(typeof(RoomCheckRevamp.Startup))]
namespace RoomCheckRevamp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }
        // In this method we will create default User roles and Admin user for login
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Supervisor"))
            {

                // first we create Admin rool   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Supervisor";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.Email = "Admin@gmail.com";
                user.FName = "FirstAdmin";
                user.LName = "LastAdmin";

                string userPwd = "Tuttle@101";

                var chkUser = UserManager.Create(user, userPwd);

                //Add Default user to Role Supervisor
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Supervisor");
                }
            }
            //creating Creating Technician role
            if (!roleManager.RoleExists("Student Tech"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Student Tech";
                roleManager.Create(role);

            }
            //creating Creating Student Worker role
            if (!roleManager.RoleExists("Student Worker"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Student Worker";
                roleManager.Create(role);
            }
        }

    }
}
