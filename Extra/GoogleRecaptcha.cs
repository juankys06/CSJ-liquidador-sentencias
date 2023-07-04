using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace liquidador_web.Extra
{
    public class GoogleReCaptcha
    {
        public string key { get; set; }
        public string secret { get; set; }

        public static bool ReCaptchaPassed(string gRecaptchaResponse, string secret, ILogger logger)
        {
            HttpClient httpClient = new HttpClient();
            var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={gRecaptchaResponse}", HttpCompletionOption.ResponseHeadersRead).Result;
            if (res.StatusCode != HttpStatusCode.OK)
            {
                logger.LogError("Error while sending request to ReCaptcha");
                return false;
            }
            

            string JSONres = res.Content.ReadAsStringAsync().Result;
            dynamic JSONdata = Newtonsoft.Json.Linq.JObject.Parse(JSONres);
            if (JSONdata.success != "true")
                return false;

            return true;
        }
    }
}
