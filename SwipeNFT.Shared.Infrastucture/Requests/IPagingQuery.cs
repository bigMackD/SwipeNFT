namespace SwipeNFT.Shared.Infrastructure.Requests
{
    public interface IPagingQuery : IRequest
    {
        string SortDirection { get; set; }
        int PageIndex { get; set; }
        int? PageSize { get; set; }
    }
}
