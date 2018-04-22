using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using backend.Core.Model;
using backend.Core.Interfaces;

namespace backend.Services
{
    public class SecurityManager
    {
        private JwtSettings _settings = null;
        private readonly IEntityFrameworkApplicationRepository _entityRepository;
        public SecurityManager(JwtSettings settings, IEntityFrameworkApplicationRepository entityRepository)
        {
            _settings = settings;
            _entityRepository = entityRepository;
        }

        public AppUserAuth ValidateUser(AppUser user)
        {
            AppUserAuth ret = new AppUserAuth();
            AppUser authUser = null;

            // Attempt to validate user
            authUser = _entityRepository.GetUsers().Where(
              u => u.UserName.ToLower() == user.UserName.ToLower()
              && u.Password == user.Password).FirstOrDefault();

            if (authUser != null)
            {
                // Build User Security Object
                ret = BuildUserAuthObject(authUser);
            }

            return ret;
        }

        protected List<AppUserRole> GetUserRoles(AppUser authUser)
        {
            List<AppUserRole> list = new List<AppUserRole>();

            try
            {
                list = _entityRepository.GetRoles().Where(
                         u => u.UserId == authUser.UserId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Exception trying to retrieve user roles.", ex);
            }

            return list;
        }

        protected AppUserAuth BuildUserAuthObject(AppUser authUser)
        {
            AppUserAuth ret = new AppUserAuth();
            List<AppUserRole> roles = new List<AppUserRole>();

            // Set User Properties
            ret.UserName = authUser.UserName;
            ret.IsAuthenticated = true;
            ret.BearerToken = new Guid().ToString();

            // Get all claims for this user
            ret.Roles = GetUserRoles(authUser);

            // Set JWT bearer token
            ret.BearerToken = BuildJwtToken(ret);

            return ret;
        }

        protected string BuildJwtToken(AppUserAuth authUser)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes(_settings.Key));

            // Create standard JWT claims
            List<Claim> jwtClaims = new List<Claim>();
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Sub,
                authUser.UserName));
            jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString()));

            // Add custom roles
            foreach (var role in authUser.Roles)
            {
                jwtClaims.Add(new Claim(role.RoleType, role.RoleValue));
            }

            // Create the JwtSecurityToken object
            var token = new JwtSecurityToken(
              issuer: _settings.Issuer,
              audience: _settings.Audience,
              claims: jwtClaims,
              notBefore: DateTime.UtcNow,
              expires: DateTime.UtcNow.AddMinutes(
                  _settings.MinutesToExpiration),
              signingCredentials: new SigningCredentials(key,
                          SecurityAlgorithms.HmacSha256)
            );

            // Create a string representation of the Jwt token
            return new JwtSecurityTokenHandler().WriteToken(token); ;
        }
    }
}
