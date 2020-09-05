using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsuranceBALApi.Common
{
    public class RestSharpExternalHit
    {
        public string restSharpApiHit(bool isPost, string postData, string url, string contentType, Dictionary<string, string> headers)
        {
            IRestClient client = new RestClient(url);
            IRestRequest request = new RestRequest();

            if (isPost)
            {
                request.Method = Method.POST;
                request.AddParameter("undefined", postData, ParameterType.RequestBody);
            }
            else
            {
                request.Method = Method.GET;
            }
            request.AddHeader("Content-Type", contentType);
            if (headers != null && headers.Count > 0)
            {
                foreach (KeyValuePair<string, string> entry in headers)
                {
                    request.AddHeader(entry.Key, entry.Value);
                }
            }

            IRestResponse response = client.Execute(request);
            return response.Content;
        }
    }
}