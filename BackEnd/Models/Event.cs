
public class Event
{
    public Guid Id { get; set; } // Guid for unique identification
    public string Title { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string? Start_Time { get; set; }  // Change to string
    public string? End_Time { get; set; }    
    public string Location { get; set; }
    public bool Admin_Approval { get; set; }
    public List<Review>? Reviews { get; set; } = new List<Review>(); // Initialize to avoid null reference

}