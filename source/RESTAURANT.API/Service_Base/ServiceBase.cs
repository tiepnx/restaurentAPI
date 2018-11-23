using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace RESTAURANT.API.Service_Base
{
    public class ServiceBase
    {
        private static string _domain = System.Configuration.ConfigurationManager.AppSettings["DomainName"];

        public static HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_domain);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Add the Authorization header with the AccessToken.
            
                //client.DefaultRequestHeaders.Add("Authorization", "bearer " + Utils.GlobalSetting.Token);

            return client;
        }

        public static async Task<T> Get<T>(string uri)
        {
            using (var client = CreateHttpClient())
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<T>();
            }

            return default(T);
        }

        protected async Task<T> Post<T, K>(string uri, K obj)
        {
            using (var client = CreateHttpClient())
            {
                var response = await client.PostAsJsonAsync(uri, obj);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsAsync<T>();
            }

            return default(T);
        }
        //public static async Task<bool> _Login(string username, string password)
        //{
        //    string uri = "token";

        //    using (var client = CreateHttpClient())
        //    {
        //        // Build up the data to POST.
        //        List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
        //        postData.Add(new KeyValuePair<string, string>("grant_type", "password"));
        //        postData.Add(new KeyValuePair<string, string>("client_id", "ngAuthApp"));
        //        postData.Add(new KeyValuePair<string, string>("username", username));
        //        postData.Add(new KeyValuePair<string, string>("password", password));

        //        var content = new FormUrlEncodedContent(postData);

        //        // Post to the Server and parse the response.
        //        var response = await client.PostAsync(uri, content);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string jsonString = await response.Content.ReadAsStringAsync();
        //            object responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);

        //            // return the Access Token.
        //            dynamic r = responseData as dynamic;
        //            Utils.GlobalSetting.Token = r.access_token;
        //            Utils.GlobalSetting.Username = r.userName;

        //            Utils.GlobalSetting.IsLogged = true;
        //            return true;
        //        }
        //    }

        //    return false;
        //}
    }
}
