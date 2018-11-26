using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using RESTAURANT.API.AppCode;
using RESTAURANT.API.Provider;
using log4net.Config;
using System.Configuration;
using RESTAURANT.API.helpers;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RESTAURANT.API.Models;

[assembly: OwinStartup("T5", typeof(RESTAURANT.API.Startup))]

namespace RESTAURANT.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            HttpConfiguration config = new HttpConfiguration();
            ConfigureOAuth(app);
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
            config.MessageHandlers.Add(new CustomResponseHandler());

            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration { };
                map.RunSignalR(hubConfiguration);
            });
            createRolesandUsers();
        }
        public void ConfigureOAuth(IAppBuilder app)
        {
            double timeout;
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["TIMEOUT"]))
            {
                timeout = double.Parse(ConfigurationManager.AppSettings["TIMEOUT"]);
            }
            else
            {
                timeout = 15;
            }
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(timeout),
                Provider = new SIAMAuthorizationProvider(),
                RefreshTokenProvider = new SIAMRefreshTokenProvider(),
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
        private void createRolesandUsers()
        {
            AuthContext context = new AuthContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<RestaurentUser>(new UserStore<RestaurentUser>(context));

            // creating Creating Manager role
            if (!roleManager.RoleExists("Admin"))
            {

                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            // creating Creating Employee role
            if (!roleManager.RoleExists("Employee"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);
            }
            // In Startup iam creating first Admin Role and creating a default Admin User
            if (!roleManager.RoleExists("SuperUser"))
            {

                // first we create Admin rool
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "SuperUser";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                        
                var user = new RestaurentUser {
                    UserName = "SuperAdmin",
                    Email = "xuantiepnguyen@gmail.com",
                    CreatedDate = DateTime.Now
                };
                

                string userPWD = "pass@word1";
                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "SuperUser");
                    UserManager.AddToRole(user.Id, "Employee");
                    UserManager.AddToRole(user.Id, "Admin");
                }
            }

           

          
        }
    }
}