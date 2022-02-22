using SwipeNFT.Shared.Infrastructure.Requests;

namespace SwipeNFT.Contracts.Request.Command.Authentication
{
    public class EnableUserCommand : ICommand
    {
        public string Id { get; set; }
    }
}
