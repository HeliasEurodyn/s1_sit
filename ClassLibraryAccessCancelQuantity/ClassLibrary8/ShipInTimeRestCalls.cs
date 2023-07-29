using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace ClassLibrary8
{
    internal class ShipInTimeRestCalls
    {

        public ShipInTimeRestCalls() { }

       public static async Task<string> GetAccessToken(Dictionary<string, string> requestBody)
        {
            using (HttpClient client = new HttpClient())
            {
                var jsonBody = new StringContent(
                     JsonConvert.SerializeObject(requestBody),
                     Encoding.UTF8,
                     "application/json"
                 );

                var response = await client.PostAsync("http://localhost:15502/api/user/auth", jsonBody);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Response as a dictionary
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, Object>>(responseContent);

                    return (string)jsonResponse["accessToken"];
                }
                else
                {
                    throw new Exception("Request failed with status code: " + response.StatusCode);
                }
            }
        }


        public static async Task<string> RegisterNewUser(Dictionary<string, string> requestBody, String accessToken)
        {
            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);


                var jsonBody = new StringContent(
                     JsonConvert.SerializeObject(requestBody),
                     Encoding.UTF8,
                     "application/json"
                 );

                var response = await client.PostAsync("http://localhost:15502/api/s1-user/register", jsonBody);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // Response as a dictionary
                    //  var responseContent = await response.Content.ReadAsStringAsync();
                    //  var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, Object>>(responseContent);

                    //   return (string)jsonResponse["accessToken"];
                    return "ok";
                }
                else
                {
                    throw new Exception("Request failed with status code: " + response.StatusCode);
                }
            }
        }


    }
}
