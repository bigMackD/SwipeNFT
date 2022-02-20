using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
        private readonly UserManager<AppUser> _userManager;

        public EnableUserCommandHandler(IConfiguration configuration, AuthenticationContext context, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _context = context;
            _userManager = userManager;
        }

        public async Task<EnableUserResponse> Handle(EnableUserCommand command)
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
    }
}
