namespace UnitTestSpike
{
    public interface IUserRepository
    {
        User GetUserByLogin(string login);
    }
}