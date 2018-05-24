using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Globalization;
using System.Net;
using System.IO;
using NLog;

namespace RequestorLib
{
    public class Requestor
    {
        static Logger logger = LogManager.GetLogger("Requestor");
        static Regex regRFDomen = new Regex(@"\.рф(\/|\?|$)", RegexOptions.IgnoreCase);
        public string GetStrRespGetReq(string url, Dictionary<string, string> headers = null, Dictionary<string, string> cookies = null)
        {
            return MakeReq(url, headers: headers, cookies: cookies);
        }
        public string GetStrRespPostReq(string url, Dictionary<string, string> headers = null)
        {
            return "";
        }
        static string MakeReq(string url, string referer = "", string userAgent = "", string accept = "",
            Dictionary<string, string> headers = null, Dictionary<string, string> cookies = null)
        {
            try
            {
                Uri uri = new Uri(url.StartsWith("http") ? url : $"http://{url}");
                if (regRFDomen.IsMatch(url))
                {
                    IdnMapping idnMapping = new IdnMapping();
                    //Uri uriCur = new Uri(url);
                    UriBuilder uriBuilder = new UriBuilder(uri);
                    uriBuilder.Host = idnMapping.GetAscii(uriBuilder.Host);
                    uri = uriBuilder.Uri;
                }
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(uri);
                if (headers != null && headers.Count > 0)
                {
                    foreach (var header in headers)
                    {
                        webRequest.Headers.Add(header.Key, header.Value);
                    }
                }
                if(cookies != null && cookies.Count > 0)
                {
                    foreach (var cookie in cookies)
                    {
                        if (webRequest.CookieContainer == null)
                            webRequest.CookieContainer = new CookieContainer();
                        webRequest.CookieContainer.Add(new Cookie(cookie.Key, cookie.Value));
                    }
                }
                //162.212.168.134:80 proxyhubsik hkftlim3dp
                //webRequest.Proxy = new WebProxy("162.212.168.134", 80);
                //webRequest.Proxy.Credentials = new NetworkCredential("proxyhubsik", "hkftlim3dp");
                WebResponse webResponse = webRequest.GetResponse();
                string strResp;
                using (Stream stream = webResponse.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        strResp = streamReader.ReadToEnd();
                    }
                }
                webResponse.Close();
                return strResp;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.ToString());
                return null;
            }
        }
    }
}
