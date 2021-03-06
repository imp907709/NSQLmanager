using System.Net;
using System.Net.Http;

namespace IWebManagers
{

    /// <summary>
    /// Gets response from URL with method
    /// </summary>
    public interface IWebManager
    {
        WebResponse GetResponse(string url, string method);
    }
    /// <summary>
    /// Reads response and converts it to string
    /// </summary>
    public interface IResponseReader
    {
        string ReadResponse(HttpWebResponse response);
        string ReadResponse(WebResponse response);
        string ReadResponse(HttpResponseMessage response);
    }

}