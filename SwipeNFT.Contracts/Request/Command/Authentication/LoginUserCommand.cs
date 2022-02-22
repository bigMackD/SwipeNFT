using SwipeNFT.Shared.Infrastructure.Requests;

namespace SwipeNFT.Contracts.Request.Command.Authentication
{
    public class LoginUserCommand : ICommand
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
