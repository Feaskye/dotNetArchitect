using Nest;

namespace ElasticSearchWeb.AppCode
{
    public interface IEsClientProvider
    {
        ElasticClient GetClient();
    }
}