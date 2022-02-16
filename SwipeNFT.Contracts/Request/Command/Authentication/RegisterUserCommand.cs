using SwipeNFT.Shared.Infrastructure.Command;

namespace SwipeNFT.Contracts.Request.Command.Authentication
{
    public class RegisterUserCommand : ICommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
