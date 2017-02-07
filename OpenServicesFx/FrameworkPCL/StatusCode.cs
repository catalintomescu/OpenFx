
namespace CTOnline.OpenServicesFx
{
    public enum StatusCode
    {
        Continue = 100,
        OK = 200,
        Created = 201,
        NoContent = 204,
        ResetContent = 205,
        PartialContent = 206,
        MultipleChoices = 300,
        Ambiguous = 300,
        Moved = 301,
        Found = 302,
        NotModified = 304,
        Unused = 306,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        NotAcceptable = 406,
        RequestTimeout = 408,
        Conflict = 409,
        RequestEntityTooLarge = 413,
        UnsupportedMediaType = 415,
        RequestedRangeNotSatisfiable = 416,
        ExpectationFailed = 417,
        InternalServerError = 500,
        NotImplemented = 501,
        ServiceUnavailable = 503,
    }
}
