using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using RESTAURANT.API.AppCode;
using RESTAURANT.API.DAL;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace RESTAURANT.API
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private AuthRepository _repo = null;
        protected RestaurentCtx _db { get; set; }
        public AccountController()
        {
            _repo = new AuthRepository();
            //NotificationHub.SayHello();
        }

        // POST api/Account/Register

        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            //_db = new RestaurentModel();
            //List<Status> st = _db.STATUS.ToList();

            if (userModel.Id != null)
            {
                IdentityResult result = await _repo.UpdateUser(userModel);

                IHttpActionResult errorResult = GetErrorResult(result);

                if (errorResult != null)
                {
                    return errorResult;
                }
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                IdentityResult result = await _repo.RegisterUser(userModel);

                IHttpActionResult errorResult = GetErrorResult(result);

                if (errorResult != null)
                {
                    return errorResult;
                }
                return Ok(result);
            }
            return Ok();
        }
        [Authorize]
        [Route("ResetPassword")]
        //https://forums.asp.net/t/2105387.aspx?using+identity+and+owin+how+can+I+change+a+password+without+knowing+the+existing+password
        public async Task<IHttpActionResult> ForgotPassword(ResetPasswordModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = HttpContext.Current.User.Identity.GetUserId();
            IdentityUser user = await _repo.FindUser(HttpContext.Current.User.Identity.Name, resetPasswordModel.OldPassword);
            if (user != null)
            {
                IdentityResult result = await _repo.ResetPassword(user.Id, resetPasswordModel.Password);
                IHttpActionResult errorResult = GetErrorResult(result);
                if (errorResult != null)
                {
                    return errorResult;
                }
            }
            else
                return BadRequest("Invalid User");
            return Ok();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("error", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        //use for printing Kien label in Winapp's Search section
        //[HttpGet]
        //[Route("getlistorg")]
        //public List<ORGANIZATION> GetListOrg()
        //{
        //    using (var svc = new FactoryService())
        //    {
        //        return svc.GetListOrg();
        //    }
        //}

        //[Authorize]
        //[HttpGet]
        //[Route("getpermission/{userName}/{featureCode}")]
        //public List<Permissionuser> GetPermissionUser(string userName, string featureCode)
        //{
        //    using (var svc = new SecurityUserFactoryService())
        //    {
        //        return svc.GetListPermissionUser(userName, featureCode);
        //    }
        //}
        //[HttpGet]
        //[Route("getlistmanhamay")]
        //public List<ORGANIZATION> GetMaNhaMay()
        //{
        //    using (var svc = new FactoryService())
        //    {
        //        return svc.GetListMaNhaMay();
        //    }
        //}

    }

}