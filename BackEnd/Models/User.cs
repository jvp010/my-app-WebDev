////////////////////////////////////////////////
// User.cs
////////////////////////////////////////////////
public class User {

    public Guid Id { get; set; } = Guid.NewGuid(); 
    public string? First_Name { get; set; } = ""; 
    public string? Last_Name { get; set; } = ""; 
    public string? Email { get; set; } = ""; 
    public string? Password { get; set; }
    public int Recurring_Days { get; set; }  = 0;

    public string Role {get;set;} = "User";


    public User () {}
    public User (Guid id, string first_name, string last_name, string email, string password, int recurring_days) { // Q how to handle password securely?
        Id = id;
        First_Name = first_name;
        Last_Name = last_name;
        Email = email;
        Password = password;
        Recurring_Days = recurring_days;
    }
}