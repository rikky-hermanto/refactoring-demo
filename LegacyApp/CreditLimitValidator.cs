namespace LegacyApp
{
    public class CreditLimitValidator : IUserValidator
    {
        public bool Validate(User user)
        {
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            return true;
        }
    }
}