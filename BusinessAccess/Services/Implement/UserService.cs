
using BusinessAccess.Filter;
using BusinessAccess.Repository.Interface;
using BusinessAccess.Services.Interface;
using Common;
using DataAccess.Model;
using Security.SecurityModel;
using Constants = Common.Constants;

namespace BusinessAccess.Services.Implement
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository) 
        {
            _userRepository = userRepository;
        }

        public async Task<int> countAll()
        {
            return await _userRepository.CountAll();
        }
        public async Task<List<User>> FindWithFilter(PaginationFilter filter)
        {
            return await _userRepository.FindWithFilter(filter);
        }
        public async Task<User> getById(int id)
        {
            return await _userRepository.FindOne(o => o.Id == id);
        }
        public async Task<User> getUserByEmail(string email)
        {
            return await _userRepository.FindOne(o => o.Email == email);
        }
        public async Task<User> registerUser(UserRegister data)
        { 
            var user = await getUserByEmail(data.Email);
            if (user != null) {
                throw new BadRequestError(Constants.EMAIL_ALREADY_EXIST);
            }

            User newUser = new User()
            {
                FullName=data.FullName,
                Phone=data.Phone,
                Email = data.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(data.Password),
                Role = Role.Customer
            };
            await _userRepository.Insert(newUser);
            return newUser;
        }
        public async Task addUser(User user)
        {
            await _userRepository.Insert(user);           
        }
        public async Task removeUser(User user)
        {
            await _userRepository.Delete(user);
        }
        public async Task<User> updateUser(User data)
        {
            await _userRepository.Update(data);
            return data;
        }
    }
}
