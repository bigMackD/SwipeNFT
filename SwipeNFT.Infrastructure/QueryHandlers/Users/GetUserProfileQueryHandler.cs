using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SwipeNFT.Contracts.Request.Query.Users;
using SwipeNFT.Contracts.Response.Users;
using SwipeNFT.DAL.Models.Authentication;
using SwipeNFT.Shared.Infrastructure.QueryHandler;

namespace SwipeNFT.Infrastructure.QueryHandlers.Users
{
    public class GetUserProfileQueryHandler : IAsyncQueryHandler<GetUserProfileQuery, GetUserProfileResponse>
    {
        private readonly UserManager<AppUser> _userManager;
      
        public GetUserProfileQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetUserProfileResponse> Handle(GetUserProfileQuery query)
        {
            var user = await _userManager.FindByIdAsync(query.UserId);
            return new GetUserProfileResponse
            {
                FullName = user.FullName
            };
        }
    }
}
