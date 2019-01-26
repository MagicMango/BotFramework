using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace BotCore.Controller
{
    public static class WebApiController
    {
        /// <summary>
        /// Get a random Chuck Norris joke from url: https://api.chucknorris.io/jokes/random
        /// </summary>
        /// <returns></returns>
        public static string GetRandomChuckNorrrisJoke()
        {
            /* Task<string> t = Task.Run(async () =>
              {
                  HttpClient client = new HttpClient();
                  HttpResponseMessage response = await client.GetAsync("https://api.chucknorris.io/jokes/random");
                  if (response.IsSuccessStatusCode)
                  {
                      return await response.Content.ReadAsStringAsync();
                  }
                  return "";
              });
              t.Wait();
              if(t.Exception != null)
              {
                  throw t.Exception;
              }
              dynamic result = JsonConvert.DeserializeObject(t.Result);
              //Match m = Regex.Match(, @"\""value\""\:\""([^""]*)");
              //return m.Groups[1].Value;
              return (string)result?.value;*/
            return "";
        }
    }
}
