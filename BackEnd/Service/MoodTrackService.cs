public class MoodTrackerService
{
    private List<MoodTracker> moodTrackers = new List<MoodTracker>(); // save in json
    private List<Attendance> attendances = new List<Attendance>(); // attendance data from json
    private List<Account> admins = new List<Account>(); // admins list

    public MoodTrackerService(List<MoodTracker> moodTrackers, List<Attendance> attendances, List<Account> admins)
    {
        this.moodTrackers = moodTrackers;
        this.attendances = attendances;
        this.admins = admins;
    }

    public void SubmitMood(Guid userId, string mood)
    {
        var today = DateTime.Today;
        var existingMood = moodTrackers.FirstOrDefault(m => m.User_Id == userId && m.Date.Date == today);

        if (existingMood != null)
        {
            Console.WriteLine("Mood already submitted today");
            return;
        }

        // eventattendance om te checken of user aanwezig was
        var attendance = attendances.FirstOrDefault(a => a.User_Id == userId && a.Date.Date == today);
        if (attendance == null)
        {
            Console.WriteLine("User was not present today, cannot submit mood");
            return;
        }

        // mood wordt in json opgeslagen
        var moodOption = Enum.Parse<MoodOption>(mood);
        var newMood = new MoodTracker(Guid.NewGuid(), userId, today, moodOption);
        moodTrackers.Add(newMood);
        SaveMoodTrackersToFile();

        if (mood == "Bad" || mood == "Really Bad")
        {
            NotifyAdmins(userId, mood);
        }
    }


    public List<MoodTracker> GetAllMoods()
    {
        return moodTrackers;
    }

    public List<MoodTracker> GetUserMoods(Guid userId)
    {
        return moodTrackers.Where(m => m.User_Id == userId).ToList();
    }

    private void NotifyAdmins(Guid userId, string mood)
    {
        foreach (var admin in admins.Where(a => a.IsAdmin))
        {
            Console.WriteLine($"Admin {admin.Username} notified: User {userId} submitted a '{mood}' mood.");
        }
    }

    private void SaveMoodTrackersToFile()
    {
        string path = "MoodTracker.json";
        JsonFileHandler.WriteToJsonFile(path, moodTrackers);
    }
}