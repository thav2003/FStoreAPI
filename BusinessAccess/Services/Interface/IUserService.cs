using BusinessAccess.Filter;
using DataAccess.Model;
using Security.SecurityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Interface
{
    public interface IUserService
    {
        Task<User> getUserByEmail(string email);
        Task<User> getById(int id);
        Task<int> countAll();
        Task<List<User>> FindWithFilter(PaginationFilter filter);
        Task<User> registerUser(UserRegister user);
        Task addUser(User user);
        Task<User> updateUser(User user);
        Task removeUser(User user);

    }
}
