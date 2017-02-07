using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace CTOnline.OpenServicesFx.WebApi
{
    public class FailedActionResult : IHttpActionResult
    {
        private readonly HttpStatusCode _statusCode;
        private readonly FailedActionDto _error;

        public FailedActionResult(IServiceResponse response)
        {
            _statusCode = response.Status.HasValue ? (HttpStatusCode)response.Status.Value : HttpStatusCode.BadRequest;
            _error = new FailedActionDto
            {
                Message = !string.IsNullOrWhiteSpace(response.Message) ? 
                    response.Message : "An error occured while processing your request.",
                Errors =
                    response.Errors.Select(e => new ActionErrorDto { Code = e.Code, Description = e.Description })
                        .ToList()
            };
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            var response = new HttpResponseMessage(_statusCode)
            {
                Content = new ObjectContent<FailedActionDto>(_error, ActionResultHelpers.JsonFormatter)
            };

            return response;
        }
    }
}
