﻿using System;
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
    public class UserDetailsQueryHandler : IAsyncQueryHandler<GetUserDetailsQuery, UserDetailsResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserDetailsQueryHandler> _logger;

        public UserDetailsQueryHandler(ILogger<UserDetailsQueryHandler> logger, IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<UserDetailsResponse> Handle(GetUserDetailsQuery query)
        {
            try
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
                    Success = true
                };

            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return new UserDetailsResponse
                {
                    Success = false,
                    Errors = new[] { _configuration.GetValue<string>("Messages:ExceptionMessage") }
                };
            }
        }
    }
}
