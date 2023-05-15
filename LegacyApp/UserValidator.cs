namespace LegacyApp
{
    public class UserValidator : IUserValidator
    {
        private readonly IClock _clock;
        public UserValidator() : this(new Clock(DateTime.Now))
        {
            
        }
        public UserValidator(IClock clock)
        {
            _clock = clock;
        }

        public bool Validate(User user)
        {
            if(user==null)
                return false;

            if (!ValidateName(user.Firstname) || !ValidateName(user.Surname))
            {
                return false;
            }

            if (!ValidateEmail(user.EmailAddress))
            {
                return false;
            }

            if (!ValidateAge(user.DateOfBirth))
            {
                return false;
            }

            return true;
        }

        private static bool ValidateName(string name)
        {
            return !string.IsNullOrEmpty(name);
        }

        private static bool ValidateEmail(string email)
        {
            return email.Contains('@') && email.Contains('.');
        }

        private bool ValidateAge(DateTime dateOfBirth)
        {
            int age = CalculateAge(dateOfBirth);

            return age >= 21;
        }

        public int CalculateAge(DateTime dateOfBirth)
        {
            var now = _clock.Now;
            int age = now.Year - dateOfBirth.Year;

            if (now.Month < dateOfBirth.Month || now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)
            {
                age--;
            }

            return age;
        }
    }
}