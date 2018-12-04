using System;
using System.Linq;
using System.Security.Claims;


namespace RESTAURANT.API.helpers
{
    public class Common
    {
        public static Guid GetOFSKey(ClaimsPrincipal principal)
        {
            var _ofs = principal.Claims.Where(c => c.Type == "ofs").Single().Value;
            Guid _ofsGuid;
            _ofsGuid = _ofs != string.Empty ? new Guid(_ofs) : Guid.Empty;
            return _ofsGuid;
        }
    }
}