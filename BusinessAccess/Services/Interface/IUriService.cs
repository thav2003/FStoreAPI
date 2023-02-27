using BusinessAccess.Filter;
using DataAccess.Model;

namespace BusinessAccess.Services.Interface
{
    public interface IUriService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
        public Uri GetVerifyUri(User user,string token);
    }
}
