public interface ILoginService
{
    (bool,User) Login(User user);
    bool CheckSession(string username);
    // Account GetLoggedInUser(string username);
    void LogOut(string username);
    bool Register(User user);
    string GetUserRole(string username);

    User FindByID(Guid id);
}