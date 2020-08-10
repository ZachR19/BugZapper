using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BugZapper.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace BugZapper.Services
{
    public interface IUserService
    {
        Task<AppUser> GetUserByID(string ID);
        Task<AppUser> GetUserByClaims(ClaimsPrincipal claims);

        Task<AppUser> BuildUser(AppUser user);
        Task SetPermissions(AppUser user);
    }

    //Retrieves and builds AppUsers
    public class UserService : IUserService
    {
        private readonly BugZapperContext _context;
        private readonly UserManager<AppUser> _userMan;

        public UserService(BugZapperContext context, UserManager<AppUser> userman)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), $"{nameof(context)} cannot be null");
            _userMan = userman ?? throw new ArgumentNullException(nameof(userman), $"{nameof(userman)} cannot be null");
        }

        /// <summary>
        /// Builds and retrieves the <see cref="AppUser"/> for a given ID
        /// </summary>
        /// <param name="ID">The UserID</param>
        /// <returns><see cref="AppUser"/></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<AppUser> GetUserByID(string ID)
        {
            AppUser user = await _userMan.FindByIdAsync(ID);

            if (user == null)
                throw new NullReferenceException("User cannot be null");

            await BuildUser(user);

            return user;
        }

        /// <summary>
        /// Builds and retrieves the <see cref="AppUser"/> based on a <see cref="ClaimsPrincipal"/>
        /// </summary>
        /// <param name="claims">The ClaimsPrincipal</param>
        /// <returns><see cref="AppUser"/></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<AppUser> GetUserByClaims(ClaimsPrincipal claims)
        {
            AppUser user = await _userMan.GetUserAsync(claims);

            if (user == null)
                throw new NullReferenceException("User cannot be null");

            return await BuildUser(user);
        }


        public async Task<AppUser> BuildUser(AppUser user)
        {
            if (user == null)
                throw new NullReferenceException("User cannot be null");

            await SetPermissions(user);

            return user;
        }

        public async Task SetPermissions(AppUser user)
        {
            //Get list of team perms
            user.TeamPerms = await _context.TeamPermission
                                            .Where(x => x.UserID == user.Id)
                                            .ToListAsync();

            //Get list of project perms
            user.ProjPerms = await _context.ProjectPermission
                .Where(x => x.UserID == user.Id)
                .ToListAsync();
        }
    }
}
