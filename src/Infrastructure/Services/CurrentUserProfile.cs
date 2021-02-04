using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CurrentUserProfile : ICurrentUserProfile
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _user;

        public CurrentUserProfile(IApplicationDbContext context, ICurrentUserService user)
        {
            _context = context;
            _user = user;
        }

        public async Task<Organisation> GetOrganisation()
        {
            var userId = new Guid(_user.GetUserId());
            UserProfile profile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userId);
            if (null == profile)
                return null;
            return profile.Organisation;
        }

        public async Task<UserProfile> GetUserProfile()
        {
            var userId = new Guid(_user.GetUserId());
            UserProfile profile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userId);

            return profile;
        }
    }
}
