
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using POCPvaultWeb.Models;
using POCPvaultWeb.Services;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCPvaultWeb.Controllers
{
    public class HomeController : Controller
    {
        public readonly IConfiguration _configuration;
        public readonly PEService _peService;

        public HomeController(IConfiguration configuration, PEService peService)
        {
            this._configuration = configuration;
            this._peService = peService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel();

            // If the user is authenticated, then this is how you can get the access_token and id_token
            if (User.Identity.IsAuthenticated)
            {
                string accessToken = await HttpContext.GetTokenAsync("access_token");

                // if you need to check the access token expiration time, use this value
                // provided on the authorization response and stored.
                // do not attempt to inspect/decode the access token
                DateTime accessTokenExpiresAt = DateTime.Parse(
                    await HttpContext.GetTokenAsync("expires_at"),
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind);

                string idToken = await HttpContext.GetTokenAsync("id_token");

                // Now you can use them. For more info on when and how to use the
                // access_token and id_token, see https://auth0.com/docs/tokens
                var WebUserID = GetWebUserIDFromToken(idToken);

                //TEMP
                accessToken = "poidasoi8u9327923684jweoamdPSOIEUfcionmiv890028938mkfdiojmdfgjkili43OITNM32";

                model.Clients = _peService.GetClients(accessToken, WebUserID);
                if(model.Clients.Count > 0)
                {
                    var clientId = model.Clients.FirstOrDefault().ID;
                    model.Vaults = _peService.GetWebUserVault(accessToken, clientId, WebUserID);
                    model.User = _peService.GetWebUser(accessToken, clientId, WebUserID);
                    model.Applications = _peService.GetWebUserApplications(accessToken, clientId, WebUserID);
                }

            }

            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }

        private int GetWebUserIDFromToken(string idToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(idToken);
            var tokenS = jsonToken as JwtSecurityToken;
            int retVal = 0;

            if (tokenS != null && tokenS.Subject.Trim().Length>= 2)
            {
               int.TryParse(tokenS.Subject.Split('|')[1].ToString(),out retVal);
            }
            
            return retVal;
        }
    }
}
