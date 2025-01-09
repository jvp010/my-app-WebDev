public class MoodTracker
{
    public Guid Id { get; set; }
    public Guid User_Id { get; set; }
    public DateTime Date { get; set; }
    public MoodOption Mood { get; set; }

    public MoodTracker() { }

    public MoodTracker(Guid id, Guid user_id, DateTime date, MoodOption mood)
    {
        Id = id;
        User_Id = user_id;
        Date = date;
        Mood = mood;
    }
}