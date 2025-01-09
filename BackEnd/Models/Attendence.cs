////////////////////////////////////////////////
// Attendance.cs
////////////////////////////////////////////////
public class Attendance {
    public Guid Id {get; set; } // should be Guid
    public Guid User_Id {get; set; } // FK
    public DateTime Date {get; set; }

    public Attendance () {}
    public Attendance (Guid id, Guid user_id, DateTime date) {
        Id = id;
        User_Id = user_id;
        Date = date;
    }
}