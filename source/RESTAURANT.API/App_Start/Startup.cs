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
using System.Linq;

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
            CreateClientDefault();
            CreateRolesDefault();
            CreateUserDefault();
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
                Provider = new T5AuthorizationProvider(),
                RefreshTokenProvider = new T5RefreshTokenProvider(),
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
        
        private void CreateClientDefault()
        {
            using (AuthContext ctx = new AuthContext())
            {
                //https://entityframework.net/as-no-tracking
                var client = ctx.Clients.AsNoTracking().Where(x => x.Id == "web").SingleOrDefault();
                if (client == null)
                {
                    ctx.Clients.Add(new Client
                    {
                        Id = "web",
                        Name = "web",
                        Secret = Guid.NewGuid().ToString().ToLower(),
                        ApplicationType = ApplicationTypes.JavaScript,
                        RefreshTokenLifeTime = 150,
                        Active = true,
                        AllowedOrigin = "*"
                    });
                    ctx.SaveChanges();
                }
            }
        }
        private void CreateRolesDefault()
        {
            using (AuthContext context = new AuthContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (!roleManager.RoleExists("SuperUser"))
                {
                    var role = new IdentityRole();
                    role.Name = "SuperUser";
                    roleManager.Create(role);
                }
                // creating Creating Manager role
                if (!roleManager.RoleExists("Admin"))
                {

                    var role = new IdentityRole();
                    role.Name = "Admin";
                    roleManager.Create(role);
                }

                // creating Creating Employee role
                if (!roleManager.RoleExists("User"))
                {
                    var role = new IdentityRole();
                    role.Name = "User";
                    roleManager.Create(role);
                }
            }
        }
        private void CreateUserDefault()
        {
            using (AuthContext context = new AuthContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var UserManager = new UserManager<RestaurentUser>(new UserStore<RestaurentUser>(context));
                string userPWD = "pass@word1";
                //Here we create a Admin super user who will maintain the website                        
                var user = new RestaurentUser
                {
                    UserName = "SuperAdmin",
                    Email = "xuantiepnguyen@gmail.com",
                    FullName = "SuperAdmin",
                    Created = DateTime.Now,
                    CreatedBy = "System",
                    Modified = DateTime.Now,
                    ModifiedBy = "System"
                };
                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "SuperUser");
                    UserManager.AddToRole(user.Id, "User");
                    UserManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }
}