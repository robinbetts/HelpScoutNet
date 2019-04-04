using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;

namespace HelpScout
{
    public interface IApiResponse
    {
        object Record { get; }
        bool IsSuccessful { get; }
        IList<string> Errors { get; }
        HttpStatusCode StatusCode { get; }
        HttpResponseHeaders ResponseHeader { get; }
    }

    public interface IApiResponse<out T> : IApiResponse
    {
        new T Record { get; }
    }


    public class PagedResult<T>
    {
        public IList<T> Items { get; set; } = new List<T>();
        public int Size { get; set; }
        public int TotalElements { get; set; }
        public int TotalPages { get; set; }
        public int Number { get; set; }
    }
}