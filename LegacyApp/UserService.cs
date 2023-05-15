namespace LegacyApp
{
    public class UserService : IUserService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserValidator _userValidator;
        private readonly IUserValidator _creditLimitValidator;
        private readonly ICreditLimitProvider _creditLimitProvider;

        public UserService() : this(new ClientRepository(), new UserRepository(), new CreditLimitProvider(), new UserValidator(), new CreditLimitValidator())
        {
            
        }

        public UserService(IClientRepository clientRepository, IUserRepository userRepository, ICreditLimitProvider creditLimitProvider, 
            IUserValidator userValidator, IUserValidator creditLimitValidator)
        {
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            _creditLimitProvider = creditLimitProvider;
            _userValidator = userValidator;
            _creditLimitValidator = creditLimitValidator;
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


            if (!_creditLimitValidator.Validate(user))
                return false;


            _userRepository.AddUser(user); 

            return true;
        }
    }
}