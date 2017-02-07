using System;
using System.Web.Http;

namespace CTOnline.OpenServicesFx.WebApi
{
    public class ApiControllerBase : ApiController
    {
        protected IMetricsService MetricsService { get; private set; }

        public ApiControllerBase(IMetricsService metricsService)
        {
            MetricsService = metricsService;
        }

        protected virtual IHttpActionResult SuccessActionResult<T>(ServiceResponse<T> response, string successStatName = null)
        {
            if (!string.IsNullOrWhiteSpace(successStatName))
                MetricsService.Increment(successStatName);

            return Ok(response.Content);
        }

        protected virtual IHttpActionResult FailedActionResult<T>(ServiceResponse<T> response, string failedStatName = null)
        {
            if (!string.IsNullOrWhiteSpace(failedStatName))
                MetricsService.Increment(failedStatName);

            if (!response.Status.HasValue) response.Status = OpenServicesFx.StatusCode.InternalServerError;
            return new FailedActionResult(response);
        }

        protected virtual IHttpActionResult GenerateActionResult<T>(ServiceResponse<T> response,
            Uri newResourceUriLocation = null, string successStatName = null, string failedStatName = null)
        {
            return response.HasError
                ? FailedActionResult(response, failedStatName)
                : SuccessActionResult(response, successStatName);
        }
    }
}
