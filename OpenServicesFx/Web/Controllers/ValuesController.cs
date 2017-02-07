using System.Web.Http;
using CTOnline.OpenServicesFx;
using CTOnline.OpenServicesFx.WebApi;
using SampleServicesLibrary;

namespace Web.Controllers
{
    public class ValuesController : ApiControllerBase
    {
        private readonly IActionExecutionContext _executionContext;
        private readonly IValuesService _valuesService;

        public ValuesController(IValuesService valuesService,
            IActionExecutionContext executionContext, IMetricsService metricsService) 
            : base(metricsService)
        {
            _executionContext = executionContext;
            _valuesService = valuesService;
        }

        // GET api/<controller>
        public IHttpActionResult Get([FromUri]ApiActionQueryOptions options)
        {
            ActionContext.ResolveActionExecutionContext(options, _executionContext);
            var response = _valuesService.GetAllItems();
            return GenerateActionResult(response);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id, [FromUri]ApiActionQueryOptions options)
        {
            ActionContext.ResolveActionExecutionContext(options, _executionContext);
            var response = _valuesService.GetItemById(id);
            return GenerateActionResult(response);
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]string value, [FromUri]ApiActionQueryOptions options)
        {
            ActionContext.ResolveActionExecutionContext(options, _executionContext);
            var response = _valuesService.CreateItem(value);
            return GenerateActionResult(response);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, [FromBody]string value, [FromUri]ApiActionQueryOptions options)
        {
            ActionContext.ResolveActionExecutionContext(options, _executionContext);
            var response = _valuesService.UpdateItem(id, value);
            return GenerateActionResult(response);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id, [FromUri]ApiActionQueryOptions options)
        {
            ActionContext.ResolveActionExecutionContext(options, _executionContext);
            var response = _valuesService.RemoveItemById(id);
            return GenerateActionResult(response);
        }
    }
}