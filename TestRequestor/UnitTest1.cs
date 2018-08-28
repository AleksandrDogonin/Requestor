using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RequestorLib;

namespace TestRequestor
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Requestor requestor = new Requestor();
            string urlMyScoreBasket = @"https://d.myscore.com.ua/x/feed/f_3_0_3_ru_1";
            Dictionary<string, string> headers = new Dictionary<string, string>();
            //headers.Add("referer", "https://d.myscore.com.ua/x/feed/proxy-local");
            //headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/66.0.3359.139 Safari/537.36");
            headers.Add("x-fsign", "SW9D1eZo");
            headers.Add("x-referer", "https://www.myscore.com.ua/basketball/");
            headers.Add("x-requested-with", "XMLHttpRequest");
            Dictionary<string, string> cookies = new Dictionary<string, string>();
            cookies.Add("_ga", "GA1.3.269134732.1527160408");
            cookies.Add("_gid", "GA1.3.471431654.1527160408");
            cookies.Add("_gat_UA-821699-48", "1S");
            string strResp = requestor.GetStrRespGetReq(urlMyScoreBasket, headers: headers, cookies: cookies);
            string[] strSeporators = new string[] { "¬" };
            strResp.Split(strSeporators, StringSplitOptions.None);
        }
    }
}
//test