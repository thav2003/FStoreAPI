using BusinessAccess.Filter;
using BusinessAccess.Services.Interface;
using DataAccess.Model;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;


namespace BusinessAccess.Services.Implement
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
            Console.WriteLine(_baseUri);

        }
        public Uri GetPageUri(PaginationFilter filter, string route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            return new Uri(modifiedUri);
        }

        public Uri GetVerifyUri(User user,string token)
        {
         
            var url = $"{_baseUri}/api/auth/verify?id={user.Id}&token={token}";
            return new Uri(url);
        }

    }
}
