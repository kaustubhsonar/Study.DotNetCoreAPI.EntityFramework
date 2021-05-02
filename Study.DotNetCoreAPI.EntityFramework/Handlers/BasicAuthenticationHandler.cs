using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Study.DotNetCoreAPI.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Study.DotNetCoreAPI.EntityFramework.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly BookStoresDBContext _context;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            BookStoresDBContext context)
            : base(options, logger, encoder, clock) 
        {
            _context = context;
        }

        
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //Basic authentication implementation where encoded username and password is passed by header

            if(!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Authorization header missing.");

            try
            {

                var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);
                string[] credentials = Encoding.UTF8.GetString(bytes).Split(":");
                string userId = credentials[0];
                string password = credentials[1];

                User user = _context.Users.Where(user => user.UserId == userId && user.Password == password).FirstOrDefault();

                if (user == null)
                    return AuthenticateResult.Fail("Invalid username or password.");
                else {
                    var claims = new[] { new Claim(ClaimTypes.Name, user.UserId) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);
                    return AuthenticateResult.Success(ticket);
                }

            }
            catch (Exception)
            {
                return AuthenticateResult.Fail("Error has occured.");
            }

            //return AuthenticateResult.Fail("Need to implement");
        }
    }
}
