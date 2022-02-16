using SwipeNFT.Shared.Infrastructure.Command;

namespace SwipeNFT.Contracts.Request.Command.Authentication
{
    public class EnableUserCommand : ICommand
    {
        public string Id { get; set; }
    }
}
