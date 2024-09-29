
namespace WebApp_Development_dotNET_Eight.Data
{
    public interface IWebApiExecuter
    {
        Task<T?> InvokeGet<T>(string relativeUrl);
    }
}