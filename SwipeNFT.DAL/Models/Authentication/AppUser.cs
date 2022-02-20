using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SwipeNFT.DAL.Models.Authentication
{
    public class AppUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }

        public bool? IsDisabled { get; set; }
    }
}
