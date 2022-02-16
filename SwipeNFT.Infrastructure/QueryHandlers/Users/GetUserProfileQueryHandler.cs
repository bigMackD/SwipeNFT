using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SwipeNFT.Contracts.Request.Query.Users;
using SwipeNFT.Contracts.Response.Users;
using SwipeNFT.DB.Models.Authentication;
using SwipeNFT.Shared.Infrastructure.QueryHandler;

namespace SwipeNFT.Infrastructure.QueryHandlers.Users
{
    public class GetUserProfileQueryHandler : IAsyncQueryHandler<GetUserProfileQuery, GetUserProfileResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public GetUserProfileQueryHandler(UserManager<AppUser> userManager, ILogger<GetUserProfileQueryHandler> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<GetUserProfileResponse> Handle(GetUserProfileQuery query)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(query.UserId);
                return new GetUserProfileResponse
                {
                    Success = true,
                    FullName = user.FullName
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new GetUserProfileResponse
                {
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
