using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SwipeNFT.Contracts.Request.Query.Users;
using SwipeNFT.Contracts.Response.Users;
using SwipeNFT.DAL.Models.Authentication;
using SwipeNFT.Shared.Infrastructure.QueryHandler;

namespace SwipeNFT.Infrastructure.QueryHandlers.Users
{
    public class GetUsersQueryHandler : IAsyncQueryHandler<GetUsersQuery, GetUsersResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetUsersQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<GetUsersResponse> Handle(GetUsersQuery query)
        {
            var users = await _userManager.Users.ToListAsync();
            var response = users
                .OrderBy(user => user.FullName)
                .Skip(query.PageIndex * query.PageSize.Value)
                .Take(query.PageSize.Value)
                .Select(user => new User
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    Locked = user.IsDisabled != null && user.IsDisabled.Value
                });

            return new GetUsersResponse
            {
                Success = true,
                Users = response,
                Count = users.Count
            };
        }

    }
}
