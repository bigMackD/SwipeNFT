using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SwipeNFT.DB.Models.Authentication;

namespace SwipeNFT.DB.Context
{
    public class AuthenticationContext : IdentityDbContext<AppUser>
    {
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
        {

        }

    }

}
