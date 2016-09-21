using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using SSU.ITA.WorkFlow.DataAccess.EF.Entities;
using SSU.ITA.WorkFlow.Domain.Services;
using System.Collections.Generic;
using Microsoft.Owin.Security;

namespace SSU.ITA.WorkFlow.Application.Web
{
    public class WorkFlowAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IAccountService _accountService;

        public WorkFlowAuthorizationServerProvider(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public override Task ValidateClientAuthentication(
            OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult(0);
        }

        public override async Task GrantResourceOwnerCredentials(
            OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add(
                "Access-Control-Allow-Origin", new[] { "*" });

            UserInformation user = await _accountService.FindUser(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError(
                    "invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            string roleName = await GetRoleNameByRoleId(user.RoleId);

            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, roleName));            
            context.Validated(new AuthenticationTicket(identity, CreateProperties(user.UserId)));
        }

        public AuthenticationProperties CreateProperties(int userId)
        {            
            return new AuthenticationProperties(new Dictionary<string, string>
            {
                {"userId", userId.ToString()}
            });
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }

        private async Task<string> GetRoleNameByRoleId(int roleId)
        {
            List<UserRole> roles = await _accountService.GetRolesList();
            return roles.Find(role => role.RoleId == roleId).Name;
        }
    }
}
