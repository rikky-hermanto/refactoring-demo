using System;

namespace LegacyApp
{
    public class UserService : IUserService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserValidator _userValidator;
        private readonly ICreditLimitProvider _creditLimitProvider;

        public UserService() : this(new ClientRepository(), new UserDataAccess(), new CreditLimitProvider())
        {
            
        }

        public UserService(IClientRepository clientRepository, IUserRepository userRepository, ICreditLimitProvider creditLimitProvider)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            _creditLimitProvider = creditLimitProvider;
        }


        public bool AddUser(string firname, string surname, string email, DateTime dateOfBirth, int clientId)
        {
            var user = new User
            {
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = firname,
                Surname = surname
            };

            if (!_userValidator.Validate(user))
            {
                return false;
            }

            var client = _clientRepository.GetById(clientId);
            user.Client = client;

            _creditLimitProvider.ApplyCreditLimit(user, client);


            if (!_userValidator.ValidateCreditLimit(user))
                return false;


            _userRepository.AddUser(user); 

            return true;
        }
    }
}