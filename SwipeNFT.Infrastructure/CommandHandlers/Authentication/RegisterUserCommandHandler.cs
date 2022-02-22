using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SwipeNFT.Contracts.Request.Command.Authentication;
using SwipeNFT.Contracts.Response.Authentication;
using SwipeNFT.DAL.Models.Authentication;
using SwipeNFT.Shared.Infrastructure.CommandHandler;
using SwipeNFT.Shared.Infrastructure.Enums;

namespace SwipeNFT.Infrastructure.CommandHandlers.Authentication
{
    public class RegisterUserCommandHandler : IAsyncCommandHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserCommand command)
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
    }
}
