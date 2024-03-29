﻿using System.Collections.Generic;
using SwipeNFT.Shared.Infrastructure.Response;

namespace SwipeNFT.Contracts.Response.Users
{
    public class GetUsersResponse : IResponse, IPagingResponse
    {
        public IEnumerable<User> Users { get; set; }
        public int Count { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool Locked { get; set; }

    }
}
