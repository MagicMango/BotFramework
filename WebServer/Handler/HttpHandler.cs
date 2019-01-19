using System.Collections.Generic;
using WebServer.HttpCore;
using WebServer.Model;

namespace WebServer.Handler
{
    /// <summary>
    /// simple Wrapper which will start a SimpleWebServer an
    /// </summary>
    public class HttpHandler
    {
        private SimpleWebServer ws = null;

        public HttpHandler(List<Route> routes, Route notFound)
        {
            ws = new SimpleWebServer(routes, notFound);
        }

        public void Start()
        {
            ws.Run();
        }

        public void Stop()
        {
            ws.Stop();
        }
    }
}
