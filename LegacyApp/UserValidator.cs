namespace LegacyApp
{
    public class UserValidator : IUserValidator
    {
        private readonly DateTime _currentDate;
        public UserValidator() : this(DateTime.Now)
        {
            
        }
        public UserValidator(DateTime currentDate)
        {
            _currentDate = currentDate;
        }

        public bool Validate(User user)
        {
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

        private int CalculateAge(DateTime dateOfBirth)
        {
            var now = _currentDate;
            int age = now.Year - dateOfBirth.Year;

            if (now.Month < dateOfBirth.Month || now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)
            {
                age--;
            }

            return age;
        }
    }
}