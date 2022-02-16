namespace SwipeNFT.Shared.Infrastructure.Response
{
    public interface IResponse<T> : IBaseResponse
    {
        T Content { get; set; }
    }
}
