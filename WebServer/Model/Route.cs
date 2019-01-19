using System;
using System.Net;

namespace WebServer.Model
{
    public class Route
    {
        public string Path { get; set; }
        public Func<HttpListenerRequest, string> Method { set; get; }
        public string Type { get; set; }
        public Func<object, object> Callback { get; set; }
        public Route(string path, Func<HttpListenerRequest, string> method, string type = MethodType.GET, Func<object, object> callback = null)
        {
            if (path != null)
            {
                Path = path;
            }
            else
            {
                throw new ArgumentException("path cannot be null or empty.");
            }
            if (method != null)
            {
                Method = method;
            }
            else
            {
                throw new ArgumentException("method cannot be null.");
            }
            Type = type;
            Callback = callback;
        }
        public static class MethodType
        {
            public const string GET = "GET";
            public const string POST = "POST";
        }
    }
}
