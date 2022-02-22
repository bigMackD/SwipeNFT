using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SwipeNFT.Contracts.Request.Command.Authentication;
using SwipeNFT.Contracts.Response.Authentication;
using SwipeNFT.DAL.Context;
using SwipeNFT.DAL.Models.Authentication;
using SwipeNFT.Shared.Infrastructure.CommandHandler;
using SwipeNFT.Shared.Infrastructure.Exceptions;

namespace SwipeNFT.Infrastructure.CommandHandlers.Authentication
{
    public class DisableUserCommandHandler : IAsyncCommandHandler<DisableUserCommand, DisableUserResponse>
    {
        private readonly AuthenticationContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public DisableUserCommandHandler(IConfiguration configuration, AuthenticationContext context, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _context = context;
            _userManager = userManager;
        }

        public async Task<DisableUserResponse> Handle(DisableUserCommand command)
        {
            var user = await _userManager.FindByIdAsync(command.Id);
            if (user != null)
            {
                user.IsDisabled = true;
                await _context.SaveChangesAsync();

                return new DisableUserResponse();
            }

            throw new InputValidationException(_configuration.GetValue<string>("Messages:Users:UserNotFound"));
        }
    }
}
