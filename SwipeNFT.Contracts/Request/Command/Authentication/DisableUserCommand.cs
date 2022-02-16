using SwipeNFT.Shared.Infrastructure.Command;

namespace SwipeNFT.Contracts.Request.Command.Authentication
{
    public class DisableUserCommand : ICommand
    {
        public string Id { get; set; }
    }
}
