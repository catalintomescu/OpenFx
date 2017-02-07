using System.Collections.Generic;

namespace CTOnline.OpenServicesFx
{
    public interface IServiceResponse
    {
        bool HasError { get; set; }
        string Message { get; set; }
        IList<ServiceResponseError> Errors { get; set; }
        StatusCode? Status { get; set; }
        void AddError(int? code = null, string description = null, StatusCode? status = null);
        void ClearErrors();
    }
}
