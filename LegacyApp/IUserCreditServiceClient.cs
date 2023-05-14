namespace LegacyApp
{
    public interface IUserCreditServiceClient
    {
        int GetCreditLimit(string firstname, string surname, DateTime dateOfBirth);
    }
}