

using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace RESTAURANT.API.helpers
{
    public class ResponseMetadata
    {
        public string Version { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public object Result { get; set; }
        public DateTime Timestamp { get; set; }
        public long? Size { get; set; }
        public object Content { get; internal set; }
    }
    public class CustomResponseHandler : DelegatingHandler
    {
        //protected async override Task<HttpResponseMessage> SendAsync(
        //HttpRequestMessage request, CancellationToken cancellationToken)
        //{
        //    Debug.WriteLine("Process request");
        //    // Call the inner handler.
        //    var response = await base.SendAsync(request, cancellationToken);
        //    Debug.WriteLine("Process response");
        //    return response;
        //}
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            try
            {
                return GenerateResponse(request, response);
            }
            catch (Exception ex)
            {
                return request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        private HttpResponseMessage GenerateResponse(HttpRequestMessage request, HttpResponseMessage response)
        {
            string errorMessage = null;
            HttpStatusCode statusCode = response.StatusCode;
            object responseContent;

            ResponseMetadata responseMetadata = new ResponseMetadata();
            responseMetadata.Version = "1.0";
            responseMetadata.StatusCode = statusCode;
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            responseMetadata.Timestamp = dt;

            //if (!IsResponseValid(response))
            //{
            //    return request.CreateResponse(HttpStatusCode.BadRequest, "Invalid response..");
            //}

            
            if (response.TryGetContentValue(out responseContent))
            {
                HttpError httpError = responseContent as HttpError;
                if (httpError != null)
                {
                    errorMessage = httpError.Message;
                    statusCode = HttpStatusCode.InternalServerError;
                    responseContent = (object)httpError.MessageDetail ?? httpError.ModelState;
                }
            }
           

            responseMetadata.Content = responseContent;            
            responseMetadata.ErrorMessage = errorMessage;
            responseMetadata.Size = responseContent.ToString().Length;

            var result = request.CreateResponse(response.StatusCode, responseMetadata);
            return result;
        }
        private bool IsResponseValid(HttpResponseMessage response)
        {
            if ((response != null) && (response.StatusCode == HttpStatusCode.OK))
                return true;
            return false;
        }
    }
}