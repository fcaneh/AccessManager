using AccessManager.Application.Contracts;

namespace AccessManager.Application.Features.Users.GetAllUsers
{
    public class GetAllUsersHandler
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public GetAllUsersResponse Handle(GetAllUsersQuery request)
        {
            return new GetAllUsersResponse
            {
                Users = _userRepository.GetAllUsers()
            };
        }
    }
}
