﻿using SwipeNFT.Shared.Infrastructure.Response;

namespace SwipeNFT.Contracts.Response.Users
{
    public class UserDetailsResponse : IResponse
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}
