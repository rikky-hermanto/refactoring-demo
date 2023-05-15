namespace LegacyApp
{
    public interface IUserValidator
    {
        bool Validate(User user);
        bool ValidateCreditLimit(User user);
    }
}