using Furion.RemoteRequest;

namespace QinSoft.Demo.Furion
{
    [Client("Baidu")]
    [RetryPolicy(3, 1000)]
    public interface IHttp: IHttpDispatchProxy
    {
        [Get()]
        Task<string> GetBaidu();
    }
}
