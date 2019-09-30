using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using RESTAURANT.API.Models;
using RESTAURANT.API.DAL.Services;
using RESTAURANT.API.DAL;
//using RESTAURANT.API.DAL;
//using RESTAURANT.API.DAL.Services;

namespace RESTAURANT.API.AppCode
{
    public class AuthRepository : IDisposable
    {
        private AuthContext _ctx;

        //private UserManager<IdentityUser> _userManager;
        private UserManager<RestaurentUser> _userManager;

        public AuthRepository()
        {
            _ctx = new AuthContext();
            //_userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
            _userManager = new UserManager<RestaurentUser>(new UserStore<RestaurentUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            //IdentityUser user = new IdentityUser
            //{
            //    UserName = userModel.UserName,
            //    Email = userModel.Email,
            //    PhoneNumber = userModel.PhoneNumber,
            //};
            Guid ofsKey = Guid.NewGuid();
            RestaurentUser user = new RestaurentUser
            {
                UserName = userModel.UserName,
                Email = userModel.Email,
                PhoneNumber = userModel.PhoneNumber,
                OFSKey = ofsKey
            };
            
            var result = await _userManager.CreateAsync(user, userModel.Password);
            _userManager.AddToRole(user.Id, "Admin");
            user.OFSKey = ofsKey;
            OFS ofs = new OFS(ofsKey);
            OFSService ofsSv = new OFSService();
            ofsSv.Insert(ofs, user.UserName);
            return result;
        }

        public async Task<IdentityResult> UpdateUser(UserModel userModel)
        {
            //IdentityUser user = new IdentityUser
            //{
            //    Id = userModel.Id,
            //    UserName = userModel.UserName,
            //    Email = userModel.Email,
            //    PhoneNumber = userModel.PhoneNumber
            //};
            //var securityUserFactory = new SecurityUserFactory();

            //securityUserFactory.UserName = userModel.UserName;
            //securityUserFactory.MaNhaMay = userModel.MaOrg;
            //securityUserFactory.IsAdmin = userModel.IsAdmin;
            //using (var svc = new SecurityUserFactoryService())
            //{
            //    svc.Update(securityUserFactory, securityUserFactory.CreatedBy);
            //}

            //IdentityUser user = _userManager.FindById(userModel.Id);
            
            RestaurentUser user = _userManager.FindById(userModel.Id);
            user.Email = userModel.Email;
            user.PhoneNumber = userModel.PhoneNumber;
            
            if (userModel.Password != null&& userModel.Password.Length>=6 &&(userModel.Password.Equals(userModel.ConfirmPassword))){
                _userManager.RemovePassword(userModel.Id);
                _userManager.AddPassword(userModel.Id, userModel.Password);
            }
            var result = await _userManager.UpdateAsync(user);            
            return result;
        }

        public async Task<IdentityResult> ResetPassword(string userId, string passWord)
        {
            _userManager.RemovePassword(userId);
            return _userManager.AddPassword(userId, passWord);
        }
        public async Task<RestaurentUser> FindUser(string userName, string password)
        {
            RestaurentUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
        public Client FindClient(string clientId)
        {
            var client = _ctx.Clients.Find(clientId);

            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            var existingToken = _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            _ctx.RefreshTokens.Add(token);

            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                _ctx.RefreshTokens.Remove(refreshToken);
                return await _ctx.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _ctx.RefreshTokens.Remove(refreshToken);
            return await _ctx.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _ctx.RefreshTokens.ToList();
        }
    }
}