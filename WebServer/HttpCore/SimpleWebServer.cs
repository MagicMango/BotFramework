using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using WebServer.Model;

namespace WebServer.HttpCore
{
    public class SimpleWebServer
    {
        private string BaseUrl { get; set; }
        private string PublicUrl { get; set; }
        private int Port { get; set; }
        private readonly HttpListener _listener = new HttpListener();
        private readonly Dictionary<string, Route> Routes = new Dictionary<string, Route>();
        private static Route NotFound;

        public SimpleWebServer(List<Route> routes, Route notFound ,string baseUrl = "http://localhost", int port = 1337, string publicFolder = "public")
        {
            NotFound = notFound;
            PublicUrl = AppDomain.CurrentDomain.BaseDirectory + publicFolder;
            if (!HttpListener.IsSupported)
                throw new NotSupportedException(
                    "Needs Windows XP SP2, Server 2003 or later.");

            // URI prefixes are required, for example 
            // "http://localhost:8080/index/".
            //if (prefixes == null || prefixes.Length == 0)
            //    throw new ArgumentException("prefixes");

            // A responder method is required
            //if (method == null)
            //    throw new ArgumentException("method");
            foreach (var item in routes)
            {
                Routes.Add(((item.Path=="")?"/":item.Path) + ":" + item.Type, item);
            }
            foreach (KeyValuePair<string, Route> route in Routes)
            {
                if (route.Value.Path.StartsWith("http://") || route.Value.Path.StartsWith("https://"))
                {
                    _listener.Prefixes.Add(route.Value.Path);
                }
                else
                {
                    _listener.Prefixes.Add(baseUrl + ":" + port + "/" + route.Value.Path);
                }

            }
            _listener.Start();
        }
        public void Run()
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                Console.WriteLine("Webserver running...");
                try
                {
                    while (_listener.IsListening)
                    {
                        ThreadPool.QueueUserWorkItem((c) =>
                        {
                            var ctx = c as HttpListenerContext;
                            try
                            {
                                Route r = null;
                                var path = ctx.Request.RawUrl.Substring(1);
                                Routes.TryGetValue(ctx.Request.RawUrl.Substring(1) + "/:"+ ctx.Request.HttpMethod, out r);
                                if (r != null)
                                {
                                    switch (r.Type)
                                    {
                                        case "GET":
                                            HandleGetMethod(r, ctx);
                                            break;
                                        case "POST":
                                            HandlePostMethod(r, ctx);
                                            break;
                                    }
                                }
                                else
                                {
                                    if (!HandleStaticMethod(ctx))
                                    {
                                        ctx.Response.StatusCode = 404;
                                        HandleGetMethod(NotFound, ctx);
                                    }
                                }
                            }
                            catch { } // suppress any exceptions
                            finally
                            {
                                // always close the stream
                                ctx.Response.OutputStream.Close();
                            }
                        }, _listener.GetContext());
                    }
                }
                catch { } // suppress any exceptions
            });
        }
        private void HandlePostMethod(Route r, HttpListenerContext ctx)
        {
            using (var reader = new StreamReader(ctx.Request.InputStream, ctx.Request.ContentEncoding))
            {
                var text = reader.ReadToEnd();
                dynamic objectValues = new ExpandoObject();
                foreach (var item in text.Split('&'))
                {
                    string[] valuePair = item.Split('=');
                    ((IDictionary<String, Object>)objectValues)[valuePair[0]] = valuePair[1];
                }
                r.Callback?.Invoke(objectValues);
            }
        }
        private bool HandleStaticMethod(HttpListenerContext ctx)
        {
            if (File.Exists(PublicUrl + ctx.Request.RawUrl))
            {
                string rstr = File.ReadAllText(PublicUrl + ctx.Request.RawUrl);
                byte[] buf = Encoding.UTF8.GetBytes(rstr);
                ctx.Response.ContentType = GetcontentType(ctx.Request.RawUrl);
                ctx.Response.ContentLength64 = buf.Length;
                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                return true;
            }
            return false;
        }
        private string GetcontentType(string rawUrl)
        {
            String dataType = "text/plain";
            if (rawUrl.EndsWith(".src")) rawUrl = rawUrl.Substring(0, rawUrl.LastIndexOf("."));
            else if (rawUrl.EndsWith(".html")) dataType = "text/html";
            else if (rawUrl.EndsWith(".htm")) dataType = "text/html";
            else if (rawUrl.EndsWith(".css")) dataType = "text/css";
            else if (rawUrl.EndsWith(".js")) dataType = "application/javascript";
            else if (rawUrl.EndsWith(".png")) dataType = "image/png";
            else if (rawUrl.EndsWith(".gif")) dataType = "image/gif";
            else if (rawUrl.EndsWith(".jpg")) dataType = "image/jpeg";
            else if (rawUrl.EndsWith(".ico")) dataType = "image/x-icon";
            else if (rawUrl.EndsWith(".xml")) dataType = "text/xml";
            else if (rawUrl.EndsWith(".pdf")) dataType = "application/pdf";
            else if (rawUrl.EndsWith(".zip")) dataType = "application/zip";
            else if (rawUrl.EndsWith(".json")) dataType = "application/json";
            return dataType;
        }
        private void HandleGetMethod(Route r, HttpListenerContext ctx)
        {
            string rstr = r.Method.Invoke(ctx.Request);
            byte[] buf = Encoding.UTF8.GetBytes(rstr);
            ctx.Response.ContentLength64 = buf.Length;
            ctx.Response.OutputStream.Write(buf, 0, buf.Length);
        }
        public void Stop()
        {
            _listener.Stop();
            _listener.Close();
        }
    }
}