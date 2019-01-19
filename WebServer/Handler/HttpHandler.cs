using System;
using System.Collections.Generic;
using WebServer.HttpCore;
using WebServer.Model;

namespace WebServer.Handler
{
    public class HttpHandler
    {
        SimpleWebServer ws = null;
        public HttpHandler(List<Route> routes, Route notFound)
        {
            ws = new SimpleWebServer(routes, notFound);
        }

        public void Start()
        {
            Console.WriteLine("A simple webserver. Press a key to quit.");
            ws.Run();
        }

        public void Stop()
        {
            ws.Stop();
        }
    }
}
