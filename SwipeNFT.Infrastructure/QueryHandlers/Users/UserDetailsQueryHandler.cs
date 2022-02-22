using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SwipeNFT.Contracts.Request.Query.Users;
using SwipeNFT.Contracts.Response.Users;
using SwipeNFT.DAL.Models.Authentication;
using SwipeNFT.Shared.Infrastructure.QueryHandler;

namespace SwipeNFT.Infrastructure.QueryHandlers.Users
{
    public class UserDetailsQueryHandler : IAsyncQueryHandler<GetUserDetailsQuery, UserDetailsResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public UserDetailsQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserDetailsResponse> Handle(GetUserDetailsQuery query)
        {

            var user = await _userManager.Users.FirstAsync(appUser => appUser.Id == query.Id);

            return new UserDetailsResponse
            {
                Email = user.Email,
                FullName = user.FullName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
            };
        }
    }
}
