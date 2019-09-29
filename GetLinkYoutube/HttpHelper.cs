using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GetLinkYoutube
{
    public static class HttpHelper
    {
        public const string UA = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.132 Safari/537.36";

        public async static Task<string> GetHTML(string link, int timeoutMilis = 30000)
        {
            var handler = new HttpClientHandler();
            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip;
            }
            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Add("user-agent", UA);
                client.DefaultRequestHeaders.Add("referer", link);

                var cts = new System.Threading.CancellationTokenSource();
                cts.CancelAfter(timeoutMilis);
                var response = await client.GetAsync(link, cts.Token);
                var html = await response.Content.ReadAsStringAsync();
                return html;
            }
        }

        public static async Task<bool> RemoteFileExistsWithTimeout(string url, int timeoutMilis)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";
                var myTask = request.GetResponseAsync();
                var resultTask = await Task.WhenAny(myTask, Task.Delay(timeoutMilis));
                var response = myTask.Result as HttpWebResponse;
                return (response.StatusCode.ToString().ToUpper() == "OK");
            }
            catch
            {
                return false;
            }
        }
    }
}
