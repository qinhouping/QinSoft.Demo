using Furion.RemoteRequest;

namespace QinSoft.Demo.Furion
{
    [Client("Baidu")]
    [RetryPolicy(3, 1000)]
    public interface IApiHttp: IHttpDispatchProxy
    {
        [Get()]
        Task<string> IndexAsync();
    }
}
