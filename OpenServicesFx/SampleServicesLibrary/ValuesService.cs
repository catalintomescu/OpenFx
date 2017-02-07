using System;
using System.Collections.Generic;
using System.Linq;
using CTOnline.OpenServicesFx;

namespace SampleServicesLibrary
{
    public class ValuesService : ApplicationServiceBase, IValuesService
    {
        private readonly IList<Value> _values = new List<Value>() { 
            new Value{ Id = 1, Text = "value1" },
            new Value{ Id = 2, Text = "value2" },
            new Value{ Id = 3, Text = "value3" },
            new Value{ Id = 4, Text = "value4" },
            new Value{ Id = 5, Text = "value5" }
        };

        private readonly IMetricsService _metrics;

        public ValuesService(IActionExecutionContext context, ILoggerService logger, IMetricsService metrics)
            : base(context, logger)
        {
            _metrics = metrics;
        }

        public ServiceResponse<IEnumerable<Value>> GetAllItems()
        {
            var response = new ServiceResponse<IEnumerable<Value>>();
            using (_metrics.MeasureDuration("values.getAll", new string[] { "webapp", "valuesservice" }))
            {
                Logger.LogInfo("Retrieving all values");
                response.Content = _values;
            }

            return response;
        }

        public ServiceResponse<Value> GetItemById(int id)
        {
            var response = new ServiceResponse<Value>();

            if (!IsNumericParameterGTZero(id, "id", response)) return response;

            Logger.LogInfo(string.Format("Retrieving value with id {0}", id));
            var val = _values.FirstOrDefault(x => x.Id == id);
            if (val == null)
            {
                response.Message = "An error occured while processing your request";
                response.AddError(description: string.Format("Value with ID {0} was not found.", id), status: StatusCode.NotFound);
                return response;
            }
            response.Content = val;

            return response;
        }

        public ServiceResponse<Value> CreateItem(string value)
        {
            var response = new ServiceResponse<Value>();

            if (!IsStringParameterNotNullOrWhiteSpace(value, "value", response)) return response;

            if (_values.Any(x => x.Text.ToLowerInvariant() == value.ToLowerInvariant()))
            {
                response.Message = "An error occured while processing your request";
                response.AddError(description: string.Format("Duplicate value found. Value {0} exists already."),
                    status: StatusCode.Conflict);
                return response;
            }

            var maxId = _values.Max(x => x.Id);
            var newVal = new Value { Id = maxId + 1, Text = value };
            _values.Add(newVal);

            response.Content = newVal;

            return response;
        }

        public ServiceResponse<bool> UpdateItem(int id, string value)
        {
            var response = new ServiceResponse<bool> { Content = false };

            try
            {
                var val = _values.FirstOrDefault(x => x.Id == id);
                // No null checks here will trigger errors
                val.Text = value;
                response.Content = true;
            }
            catch (Exception ex)
            {
                HandleSystemException(ex, response);
            }

            return response;
        }

        public ServiceResponse<bool> RemoveItemById(int id)
        {
            var response = new ServiceResponse<bool> { Content = false };

            try
            {
                var val = _values.FirstOrDefault(x => x.Id == id);
                // No null checks here will trigger errors
                _values.Remove(val);
                response.Content = true;
            }
            catch (Exception ex)
            {
                HandleSystemException(ex, response);
            }

            return response;
        }
    }

}
