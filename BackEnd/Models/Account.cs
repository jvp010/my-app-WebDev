public class Account
{
    public string Username { get; set; }
    public string Password { get; set; } // Q how to security this TT
    public bool IsAdmin { get; set; }
    // public UserAccount() { }
    public Account(string username, string password, bool isAdmin)
    {
        Username = username;
        Password = password;
        IsAdmin = isAdmin;
    }
}

// public record Account(string Username, string Password);