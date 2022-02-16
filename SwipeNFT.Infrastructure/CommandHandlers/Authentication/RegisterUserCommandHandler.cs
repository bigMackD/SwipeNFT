using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SwipeNFT.Contracts.Request.Command.Authentication;
using SwipeNFT.Contracts.Response.Authentication;
using SwipeNFT.DB.Models.Authentication;
using SwipeNFT.Shared.Infrastructure.CommandHandler;
using SwipeNFT.Shared.Infrastructure.Enum;

namespace SwipeNFT.Infrastructure.CommandHandlers.Authentication
{
    public class RegisterUserCommandHandler : IAsyncCommandHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public RegisterUserCommandHandler(UserManager<AppUser> userManager, IConfiguration configuration, ILogger<RegisterUserCommandHandler> logger)
        {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserCommand command)
        {
            try
            {
                var appUser = new AppUser()
                {
                    UserName = command.UserName,
                    Email = command.Email,
                    FullName = command.FullName
                };

                var result = await _userManager.CreateAsync(appUser, command.Password);
                await _userManager.AddToRoleAsync(appUser, Role.User.ToString());
                return new RegisterUserResponse
                {
                    Success = result.Succeeded,
                    Errors = result.Errors.Select(x => x.Description).ToArray()
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new RegisterUserResponse
                {
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
