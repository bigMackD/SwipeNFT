using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SwipeNFT.Contracts.Request.Command.Authentication;
using SwipeNFT.Contracts.Response.Authentication;
using SwipeNFT.DAL.Context;
using SwipeNFT.DAL.Models.Authentication;
using SwipeNFT.Shared.Infrastructure.CommandHandler;

namespace SwipeNFT.Infrastructure.CommandHandlers.Authentication
{
    public class EnableUserCommandHandler : IAsyncCommandHandler<EnableUserCommand, EnableUserResponse>
    {
        private readonly AuthenticationContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<EnableUserCommandHandler> _logger;
        private readonly UserManager<AppUser> _userManager;

        public EnableUserCommandHandler(IConfiguration configuration, ILogger<EnableUserCommandHandler> logger, AuthenticationContext context, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<EnableUserResponse> Handle(EnableUserCommand command)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(command.Id);
                if (user != null)
                {
                    user.IsDisabled = false;
                    await _context.SaveChangesAsync();

                    return new EnableUserResponse
                    {
                        Success = true
                    };
                }

                return new EnableUserResponse
                {
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:Users:UserNotFound") }
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new EnableUserResponse
                {
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
