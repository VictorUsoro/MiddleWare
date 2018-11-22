using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MW.Application
{
   public class BaseHttpClient
   {
      public virtual string ContentType { get; set; }
      private readonly HttpClient _client;
      private HttpResponseMessage _responseMessage;

      public BaseHttpClient(bool addheaders = true, string token = null)
      {
         _client = new HttpClient();
         _client.DefaultRequestHeaders.Accept.Clear();
         if (addheaders)
         {
            _client.DefaultRequestHeaders.Accept.Add(new
               MediaTypeWithQualityHeaderValue("application/json"));
         }
         if (!string.IsNullOrEmpty(token))
         {
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
         }
         ContentType = "application/json";
      }

      public virtual async Task<T> GetAsync<T>(string baseUrl, string getdata, string url)
      {
         T returnValue;
         try
         {
            _client.BaseAddress = new Uri(baseUrl);
            StringBuilder sb = new StringBuilder(url);
            if (!string.IsNullOrEmpty(getdata))
            {
               sb.Append(getdata);
            }
            url = sb.ToString();

            _responseMessage = await _client.GetAsync(url);
            _responseMessage.EnsureSuccessStatusCode();
            var responseString = await _responseMessage.Content.ReadAsStringAsync();
            returnValue = JsonConvert.DeserializeObject<T>(responseString);
            return returnValue;
         }
         finally
         {
            _client.Dispose();
            _responseMessage.Dispose();
         }
      }

      public virtual async Task<string> GetStringAsync(string baseUrl, string getdata)
      {
         try
         {
            string returnValue = string.Empty;
            _client.BaseAddress = new Uri(baseUrl);
            _responseMessage = await _client.GetAsync(getdata);
            if (_responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
               returnValue = await _responseMessage.Content.ReadAsStringAsync();
            }
            return returnValue;
         }
         catch (Exception ex)
         {
            return ex.Message;
         }
         finally
         {
            _client.Dispose();
            _responseMessage.Dispose();
         }
      }

      public async Task<T> PostAsync<T>(string baseUrl, object postdata, string url)
      {
         T returnValue;
         try
         {
            _client.BaseAddress = new Uri(baseUrl);
            var param = SerializeData(postdata);
            HttpContent contentPost = new StringContent(param, Encoding.UTF8, ContentType);
            _responseMessage = await _client.PostAsync(url, contentPost);
            _responseMessage.EnsureSuccessStatusCode();
            var responseString = await _responseMessage.Content.ReadAsStringAsync();
            returnValue = JsonConvert.DeserializeObject<T>(responseString);
            return returnValue;
         }
         finally
         {
            _client.Dispose();
            _responseMessage.Dispose();
         }
      }

      public async Task<string> PostStringAsync(string baseUrl, string postdata, string url)
      {
         try
         {
            _client.BaseAddress = new Uri(baseUrl);
            HttpContent contentPost = new StringContent(postdata, Encoding.UTF8, ContentType);
            _responseMessage = await _client.PostAsync(url, contentPost);
            if (_responseMessage.IsSuccessStatusCode)
            {
               return await _responseMessage.Content.ReadAsStringAsync();
            }
            return "An error occued on our system, please try later";
         }
         catch (Exception ex)
         {
            return ex.Message;
         }
         finally
         {
            _client.Dispose();
            _responseMessage.Dispose();
         }
      }

      private string SerializeData(object postdata)
      {
         var resp = "";
         switch (ContentType)
         {
            case "application/json":
               resp = JsonConvert.SerializeObject(postdata);
               break;
            case "application/x-www-form-urlencoded":
               var jo = (JObject)postdata;
               resp = string.Join("&", jo.Properties().Select(property => 
               property.Name + "=" + property.Value.ToString()).ToArray());
               break;
         }
         return resp;
      }      
   }
}
