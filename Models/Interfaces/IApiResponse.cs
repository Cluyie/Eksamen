using System;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Models.Interfaces
{
    public enum ApiResponseCode
    {
        // 1×× Informational
        [Display(Name = "Continue")]
        Continue = 100, // 0x00000064
        [Display(Name = "Switching Protocols")]
        SwitchingProtocols = 101, // 0x00000065
        [Display(Name = "Processing")]
        Processing = 102, // 0x00000066
        [Display(Name = "Early Hints")]
        EarlyHints = 103, // 0x00000067

        // 2×× Success
        [Display(Name = "OK")]
        OK = 200, // 0x000000C8
        [Display(Name = "Created")]
        Created = 201, // 0x000000C9
        [Display(Name = "Accepted")]
        Accepted = 202, // 0x000000CA
        [Display(Name = "Non-authoritative Information")]
        NonAuthoritativeInformation = 203, // 0x000000CB
        [Display(Name = "No Content")]
        NoContent = 204, // 0x000000CC
        [Display(Name = "Reset Content")]
        ResetContent = 205, // 0x000000CD
        [Display(Name = "Partial Content")]
        PartialContent = 206, // 0x000000CE
        [Display(Name = "Multi-Status")]
        MultiStatus = 207, // 0x000000CF
        [Display(Name = "Already Reported")]
        AlreadyReported = 208, // 0x000000D0
        [Display(Name = "IM Used")]
        IMUsed = 226, // 0x000000E2

        // 3×× Redirection
        Ambiguous = 300, // 0x0000012C
        [Display(Name = "Multiple Choices")]
        MultipleChoices = 300, // 0x0000012C
        Moved = 301, // 0x0000012D
        [Display(Name = "Moved Permanently")]
        MovedPermanently = 301, // 0x0000012D
        [Display(Name = "Found")]
        Found = 302, // 0x0000012E
        Redirect = 302, // 0x0000012E
        RedirectMethod = 303, // 0x0000012F
        [Display(Name = "See Other")]
        SeeOther = 303, // 0x0000012F
        [Display(Name = "Not Modified")]
        NotModified = 304, // 0x00000130
        [Display(Name = "Use Proxy")]
        UseProxy = 305, // 0x00000131
        Unused = 306, // 0x00000132
        RedirectKeepVerb = 307, // 0x00000133
        [Display(Name = "Temporary Redirect")]
        TemporaryRedirect = 307, // 0x00000133
        [Display(Name = "Permanent Redirect")]
        PermanentRedirect = 308, // 0x00000134

        // 4×× Client Error
        [Display(Name = "Bad Request")]
        BadRequest = 400, // 0x00000190
        [Display(Name = "Unauthorized")]
        Unauthorized = 401, // 0x00000191
        [Display(Name = "Payment Required")]
        PaymentRequired = 402, // 0x00000192
        [Display(Name = "Forbidden")]
        Forbidden = 403, // 0x00000193
        [Display(Name = "Not Found")]
        NotFound = 404, // 0x00000194
        [Display(Name = "Method Not Allowed")]
        MethodNotAllowed = 405, // 0x00000195
        [Display(Name = "Not Acceptable")]
        NotAcceptable = 406, // 0x00000196
        [Display(Name = "Proxy Authentication Required")]
        ProxyAuthenticationRequired = 407, // 0x00000197
        [Display(Name = "Request Timeout")]
        RequestTimeout = 408, // 0x00000198
        [Display(Name = "Conflict")]
        Conflict = 409, // 0x00000199
        [Display(Name = "Gone")]
        Gone = 410, // 0x0000019A
        [Display(Name = "Length Required")]
        LengthRequired = 411, // 0x0000019B
        [Display(Name = "Precondition Failed")]
        PreconditionFailed = 412, // 0x0000019C
        [Display(Name = "Payload Too Large")]
        RequestEntityTooLarge = 413, // 0x0000019D
        [Display(Name = "Request-URI Too Long")]
        RequestUriTooLong = 414, // 0x0000019E
        [Display(Name = "Unsupported Media Type")]
        UnsupportedMediaType = 415, // 0x0000019F
        [Display(Name = "Requested Range Not Satisfiable")]
        RequestedRangeNotSatisfiable = 416, // 0x000001A0
        [Display(Name = "Expectation Failed")]
        ExpectationFailed = 417, // 0x000001A1
        [Display(Name = "I'm a teapot")]
        Teapot = 418, // 0x000001A2
        [Display(Name = "Misdirected Request")]
        MisdirectedRequest = 421, // 0x000001A5
        [Display(Name = "Unprocessable Entity")]
        UnprocessableEntity = 422, // 0x000001A6
        [Display(Name = "Locked")]
        Locked = 423, // 0x000001A7
        [Display(Name = "Failed Dependency")]
        FailedDependency = 424, // 0x000001A8
        [Display(Name = "Upgrade Required")]
        UpgradeRequired = 426, // 0x000001AA
        [Display(Name = "Precondition Required")]
        PreconditionRequired = 428, // 0x000001AC
        [Display(Name = "Too Many Requests")]
        TooManyRequests = 429, // 0x000001AD
        [Display(Name = "Request Header Fields Too Large")]
        RequestHeaderFieldsTooLarge = 431, // 0x000001AF
        [Display(Name = "Connection Closed Without Response")]
        ConnectionClosedWithoutResponse = 444,
        [Display(Name = "Unavailable For Legal Reasons")]
        UnavailableForLegalReasons = 451, // 0x000001C3
        [Display(Name = "Client Closed Request")]
        ClientClosedRequest = 499, // 0x000001F3

        // 5×× Server Error
        [Display(Name = "Internal Server Error")]
        InternalServerError = 500, // 0x000001F4
        [Display(Name = "Not Implemented")]
        NotImplemented = 501, // 0x000001F5
        [Display(Name = "Bad Gateway")]
        BadGateway = 502, // 0x000001F6
        [Display(Name = "Service Unavailable")]
        ServiceUnavailable = 503, // 0x000001F7
        [Display(Name = "Gateway Timeout")]
        GatewayTimeout = 504, // 0x000001F8
        [Display(Name = "HTTP Version Not Supported")]
        HttpVersionNotSupported = 505, // 0x000001F9
        [Display(Name = "Variant Also Negotiates")]
        VariantAlsoNegotiates = 506, // 0x000001FA
        [Display(Name = "Insufficient Storage")]
        InsufficientStorage = 507, // 0x000001FB
        [Display(Name = "Loop Detected")]
        LoopDetected = 508, // 0x000001FC
        [Display(Name = "Not Extended")]
        NotExtended = 510, // 0x000001FE
        [Display(Name = "Network Authentication Required")]
        NetworkAuthenticationRequired = 511, // 0x000001FF
        [Display(Name = "Network Connect Timeout Error")]
        NetworkConnectTimeoutError = 599, // 0x00000257
    }

    public interface IApiResponse<T> where T : class
    {
        ApiResponseCode Code { get; set; }

        T Value { get; set; }
    }
}