using System.Collections.Generic;
using System.Diagnostics;

namespace CTOnline.OpenServicesFx
{
    public class ServiceResponse<TResponse> : IServiceResponse
    {
        public ServiceResponse()
        {
            Errors = new List<ServiceResponseError>();
            Status = null;
        }

        public TResponse Content { get; set; }

        #region IServiceResponse Members

        public bool HasError { get; set; }
        public string Message { get; set; }
        public IList<ServiceResponseError> Errors { get; set; }
        public StatusCode? Status { get; set; }

        public void AddError(int? code = null, string description = null, StatusCode? status = null)
        {
            Debug.Assert(code.HasValue || !string.IsNullOrWhiteSpace(description), "Error code or description must be provided.");
            if (!code.HasValue && string.IsNullOrWhiteSpace(description)) return;
            if (!HasError) HasError = true;
            Errors.Add(new ServiceResponseError { Code = code, Description = description });
            if (status != null) Status = status;
        }

        public void ClearErrors()
        {
            Message = null;
            Errors.Clear();
            HasError = false;
            Status = null;
        }

        public void CopyErrorsFrom(IServiceResponse source, bool overrideMessage = true)
        {
            if (!source.HasError) return;
            HasError = true;
            if (source.Status.HasValue) Status = source.Status;
            if (overrideMessage && !string.IsNullOrWhiteSpace(source.Message)) Message = source.Message;
            foreach (var err in source.Errors)
            {
                Errors.Add(err);
            }
        }

        #endregion
    }
}
