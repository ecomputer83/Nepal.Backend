
using Nepal.Abstraction.ServiceModel;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nepal.Business.Service
{
    public class RestService
    {
        public static T RestClientCall<T>(Config config, string resource, Method method, BaseRequest req, bool isKYC = false, bool noresp = true, int type = 0) where T : new()
        {

            var client = new RestClient(config.Url);
            var contentType = "";
            //if (type == 0)
            //{
                contentType = "application/json";
            //}
            //else
            //{
            //    contentType = "application/xml";
            //}

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");

            var request = new RestRequest(resource, method);
            request.AddHeader("Content-Type", contentType);
            if ((method == Method.PUT || method == Method.DELETE))
            {
                request.AddHeader("If-Match", "*");
            }

            if (isKYC)
            {
                var _username = config.UserName;
                var _password = config.Password;

                var byteArray = Encoding.ASCII.GetBytes(_username + ":" + _password);
                request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(byteArray));

            }

            if (req != null)
            {
                var data = JsonConvert.SerializeObject(req);
                request?.AddParameter(contentType, data, ParameterType.RequestBody);
            }
            var response = client.Execute<T>(request);

            ValidateHttpResponse(response);

            if (response.Data != null)
            {
                return response.Data;
            }

            //return (T)Convert.ChangeType(response.Data, typeof(T));

            if (response.Content != null && noresp)
                return JsonConvert.DeserializeObject<T>(response.Content);

            return default(T);

        }

        public static object RestClientCall(string baseUrl, string resource, Method method, BaseRequest req, bool noresp = true)
        {

            var client = new RestClient(baseUrl);
            var contentType = "application/json";

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");

            var request = new RestRequest(resource, method);
            request.AddHeader("Content-Type", "application/json");

            
            if (req != null)
                request?.AddParameter(contentType, JsonConvert.SerializeObject(req), ParameterType.RequestBody);

            var response = client.Execute(request);

            //ValidateHttpResponse(response);

            if (response.Content != null)
                return response.Content;

            return null;

        }

        private static void ValidateHttpResponse<T>(IRestResponse<T> response) where T : new()
        {
            bool hasError = false;

            if(response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.OK)
            {
                hasError = false;
            }
            else
            if (response.ResponseStatus == ResponseStatus.Error)
            {
                if (response.ErrorException.Message.ToUpper().Contains("THE REMOTE NAME COULD NOT BE RESOLVED") && response.Content == "")
                {
                    throw new Exception("Dear valued customer, Connect to or check your INTERNET and try again.");
                }
                else if (response.ErrorException.Message.ToUpper().Contains("SYSTEM.EXCEPTION") && response.Content == "")
                {
                    throw new Exception("Dear valued customer, Connection failed try again later.");
                }
                else
                {
                    hasError = true;

                }
            }
            else
            {
                hasError = true;
            }

            if (hasError)
            {
                if (String.IsNullOrEmpty(response.Content))
                {
                    if (!String.IsNullOrEmpty(response.StatusDescription))
                    {
                        throw new Exception(response.StatusDescription);
                    }
                }
                else
                {
                    throw new Exception(response.Content);
                }

            }

        }

     }
}
