using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SwipeNFT.Contracts.Request.Query.Users;
using SwipeNFT.Contracts.Response.Users;
using SwipeNFT.DAL.Models.Authentication;
using SwipeNFT.Shared.Infrastructure.QueryHandler;

namespace SwipeNFT.Infrastructure.QueryHandlers.Users
{
    public class GetUsersQueryHandler : IAsyncQueryHandler<GetUsersQuery, GetUsersResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public GetUsersQueryHandler(UserManager<AppUser> userManager, IConfiguration configuration, ILogger<GetUsersQueryHandler> logger)
        {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<GetUsersResponse> Handle(GetUsersQuery query)
        {
            try
            {
                //TODO
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
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new GetUsersResponse
                {
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
