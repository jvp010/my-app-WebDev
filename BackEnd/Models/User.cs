////////////////////////////////////////////////
// User.cs
////////////////////////////////////////////////
public class User {

    public Guid Id { get; set; } = Guid.NewGuid(); 
    public string? First_Name { get; set; }
    public string? Last_Name { get; set; } 
    public string? Email { get; set; } 
    public string? Password { get; set; }
    public int Recurring_Days { get; set; }  

    public string? Role {get;set;}


   
}