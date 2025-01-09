////////////////////////////////////////////////
// EventAttendance.cs
////////////////////////////////////////////////
public class EventAttendance {
    public Guid Id {get; set; } // should be Guid
    public Guid User_Id {get; set; } // FK
    public Guid Event_Id {get; set; } // FK
    public int Rating {get; set; }
    public string Feedback {get; set; } 

    public EventAttendance () {}
    public EventAttendance (Guid id, Guid user_id, Guid event_id, int rating, string feedback) {
        Id = id;
        User_Id = user_id;
        Event_Id = event_id;
        Rating = rating;
        Feedback = feedback;
    }
}
