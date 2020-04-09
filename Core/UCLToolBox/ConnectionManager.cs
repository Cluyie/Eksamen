using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace UCLToolBox
{
    public class ConnectionManager
    {
        public static int TimeoutMs { get; set; }

        public static async Task<string> FindUrl(int timeoutMs = 999)
        {
            return "http://81.27.216.103/webAPI/";
            TimeoutMs = timeoutMs;

            //Skal helst uptimeres
            //Offentlig base adresse: http://81.27.216.103/webAPI/
            //Intern base adresse: http://10.56.8.34/webAPI/
            //Lokal base adresse til emulator http://10.0.2.2:5000/

#if DEBUG
            if (await TestUrl("http://10.0.2.2:53524/User/GetProfile")) return "http://10.0.2.2:53524/";
            if (await TestUrl("http://localhost:5000/User/GetProfile")) return "http://localhost:5000/";
#endif
            //If you are on the same Net as the server
            if (await TestUrl("http://10.56.8.34/webAPI/User/GetProfile")) return "http://10.56.8.34/webAPI/";
            //If you are not on the same Net as the server
            if (await TestUrl("http://81.27.216.103/webAPI/User/GetProfile")) return "http://81.27.216.103/webAPI/";

            Thread.Sleep(TimeoutMs + 100);
            return null;
        }

        private static async Task<bool> TestUrl(string url)
        {
            var httpReq = (HttpWebRequest) WebRequest.Create(url);
            httpReq.AllowAutoRedirect = false;
            httpReq.Timeout = TimeoutMs;

            try
            {
                using (var httpRes = (HttpWebResponse) await httpReq.GetResponseAsync())
                {
                    var retval = httpRes.StatusCode != HttpStatusCode.NotFound;
                    // Close the response.
                    httpRes.Close();
                    return retval;
                }

                //WebClient wc = new WebClient();
                //string htmlSource = wc.DownloadString(url);
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}