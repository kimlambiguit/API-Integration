#define DEVELOPMENT_ONLY

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json.Linq;  // Available from http://json.codeplex.com/ for parsing server responses.

namespace Tempurer.Web.Api.Example
{
    /// <summary>
    /// C# API integration sample v1.0
    /// </summary>
    public class TempurerClientCredentialsExample
    {
        // Set your client name and secret from http://www.tempurer.com/API/Applications here:
        private const string _clientName = "TODO";
        private const string _clientSecret = "TODO";

        // Production api and authorisation urls
        private const string _baseApiUrl = "api.tempurer.com";
        private const string _baseAuthUrl = "auth.tempurer.com";
        private readonly string _getAccessTokenUrl = String.Format("https://{0}/oauth2/token", _baseAuthUrl);

        public static void Main(string[] args)
        {
#if DEVELOPMENT_ONLY
            // Development only to avoid "Could not establish trust relationship for the SSL/TLS secure channel" when hosting authentication server in local IIS server with self signed https certificate.
            // Not to be used in production.
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
#endif
            var application = new TempurerClientCredentialsExample();
            try
            {
                application.ListVacancies();
                application.SearchVacancies();
                application.SummaryVacancies();
                application.GetVacancy();
            }
            catch (WebException ex)
            {
                Console.WriteLine(String.Format("HTTP Status: {0}", ex.Status));
                Console.WriteLine(String.Format("WebException thrown: {0}", ex));
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Exception thrown: {0}", ex));
            }
        }

        /// <summary>
        /// Call to /services/vacancy.svc/list
        /// </summary>
        public void ListVacancies()
        {
            var scopes = new List<string>() { "basic", "/services/vacancy.svc/list" };
            var serviceUrl = String.Format("http://{0}/services/vacancy.svc/list", _baseApiUrl);

            var httpReturnValue = CallApiServiceUsingHttp(scopes, serviceUrl);
            
            Debug.WriteLine(httpReturnValue);
            Console.WriteLine(httpReturnValue);
        }

        /// <summary>
        /// Call to /services/vacancy.svc/search
        /// </summary>
        public void SearchVacancies()
        {
            var scopes = new List<string>() { "basic", "/services/vacancy.svc/search" };
            var serviceUrl = String.Format("http://{0}/services/vacancy.svc/search?query=test", _baseApiUrl);

            var httpReturnValue = CallApiServiceUsingHttp(scopes, serviceUrl);

            Debug.WriteLine(httpReturnValue);
            Console.WriteLine(httpReturnValue);
        }

        /// <summary>
        /// Call to /services/vacancy.svc/getsummary
        /// </summary>
        public void SummaryVacancies()
        {
            var scopes = new List<string>() { "basic", "/services/vacancy.svc/getsummary" };
            var serviceUrl = String.Format("http://{0}/services/vacancy.svc/getsummary", _baseApiUrl);

            var httpReturnValue = CallApiServiceUsingHttp(scopes, serviceUrl);

            Debug.WriteLine(httpReturnValue);
            Console.WriteLine(httpReturnValue);
        }

        /// <summary>
        /// Call to /services/vacancy.svc/get
        /// </summary>
        public void GetVacancy()
        {
            var scopes = new List<string>() { "basic", "/services/vacancy.svc/get" };
            var serviceUrl = String.Format("http://{0}/services/vacancy.svc/get/faf03b36-b42e-e211-8a1b-842b2b6577a5", _baseApiUrl);

            var httpReturnValue = CallApiServiceUsingHttp(scopes, serviceUrl);

            Debug.WriteLine(httpReturnValue);
            Console.WriteLine(httpReturnValue);
        }

        /// <summary>
        /// Call Api using pure HTTP calls only.
        /// </summary>
        private string CallApiServiceUsingHttp(IEnumerable<string> scopes, string serviceUrl)
        {
            Console.WriteLine(String.Format("\n\nCalling API URL: {0} with Scope: {1}", serviceUrl, String.Join(" ", serviceUrl)));

            // ------------------------------------------------
            // STEP 1: GetClientAccessToken
            // ------------------------------------------------

            // Create POST data and convert it to a byte array.
            Console.WriteLine("Auth token url: " + _getAccessTokenUrl);
            var request1 = WebRequest.Create(_getAccessTokenUrl);
            request1.Method = "POST";
            request1.ContentType = "application/x-www-form-urlencoded";

            var postDataString = "grant_type=client_credentials";
            var scopesString = HttpUtility.UrlEncode(String.Join(" ", scopes));
            if (scopesString.Length > 0)
            {
                postDataString = postDataString + "&scope=" + scopesString;
            }

            var postData = Encoding.UTF8.GetBytes(postDataString);
            request1.ContentLength = postData.Length;

            var base64details = Base64Encode(String.Format("{0}:{1}", _clientName, _clientSecret));
            var authHeader = String.Format("Basic {0}", base64details);
            request1.Headers[HttpRequestHeader.Authorization] = authHeader;

            // Get the request stream.
            var dataStream = request1.GetRequestStream();
            dataStream.Write(postData, 0, postData.Length);
            dataStream.Close();

            // Get the response.
            var response1 = request1.GetResponse();
            var status = ((HttpWebResponse)response1).StatusDescription;
            dataStream = response1.GetResponseStream();
            var reader1 = new StreamReader(dataStream);
            var responseFromServer1 = reader1.ReadToEnd();
            reader1.Close();
            if (dataStream != null)
            {
                dataStream.Close();
            }
            response1.Close();

            var json = JObject.Parse(responseFromServer1);   // Server response string: { "access_token":"your_token_value", "token_type":"bearer", "expires_in":"3600", "scope":"basic \/services\/vacancy.svc\/list" }
            var accessToken = json["access_token"].Value<string>();


            // ------------------------------------------------
            // STEP 2: Call webservice with Auth token in Header
            // ------------------------------------------------
            Console.WriteLine("Api token url: " + serviceUrl);
            var request2 = (HttpWebRequest)WebRequest.Create(serviceUrl);
            request2.Headers[HttpRequestHeader.Authorization] = String.Format("Bearer {0}", accessToken);

            var response2 = (HttpWebResponse)request2.GetResponse();
            var reader2 = new StreamReader(response2.GetResponseStream());
            var responseFromServer2 = reader2.ReadToEnd();
            return responseFromServer2;
        }

        private string Base64Encode(string input)
        {
            var toEncodeAsBytes = Encoding.ASCII.GetBytes(input);
            return Convert.ToBase64String(toEncodeAsBytes);
        }
    }
}