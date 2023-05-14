namespace LegacyApp
{
    internal interface IUserValidator
    {
        bool Validate(User user);
        bool ValidateCreditLimit(User user);
    }

    public class UserValidator : IUserValidator
    {
        public bool Validate(User user)
        {
            if (string.IsNullOrEmpty(user.Firstname) || string.IsNullOrEmpty(user.Surname))
            {
                return false;
            }

            if (user.EmailAddress.Contains("@") && !user.EmailAddress.Contains("."))
            {
                return false;
            }

            var now = DateTime.Now;
            int age = now.Year - user.DateOfBirth.Year;

            if (now.Month < user.DateOfBirth.Month || now.Month == user.DateOfBirth.Month && now.Day < user.DateOfBirth.Day)
            {
                age--;
            }

            if (age < 21)
            {
                return false;
            }

            return true;
        }

        public bool ValidateCreditLimit(User user)
        {
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            return true;
        }
    }
}