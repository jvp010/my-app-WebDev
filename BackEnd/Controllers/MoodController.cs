using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
[ApiController]
[Route("[controller]")]
public class MoodController : Controller
{
    private readonly MoodTrackerService moodTrackerService;
    public MoodController()
    {
        var moodTrackers = JsonFileHandler.ReadJsonFile<List<MoodTracker>>("MoodTracker.json")?.FirstOrDefault() ?? new List<MoodTracker>();
        var eventAttendances = JsonFileHandler.ReadJsonFile<List<Attendance>>("EventAttendance.json")?.FirstOrDefault() ?? new List<Attendance>();
        var admins = JsonFileHandler.ReadJsonFile<List<Account>>("Accounts.json")?.FirstOrDefault() ?? new List<Account>();
        moodTrackerService = new MoodTrackerService(moodTrackers, eventAttendances, admins);
    }


    [HttpPost("submit")]
    public IActionResult SubmitMood([FromBody] MoodSubmissionRequest model)
    {
        Guid userId = model.UserId;
        string mood = model.Mood;

        moodTrackerService.SubmitMood(userId, mood);
        return Ok("Mood submitted successfully.");
    }

    [HttpGet("all")]
    public IActionResult GetAllMoods()
    {
        var moods = moodTrackerService.GetAllMoods();
        return Ok(moods);
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetUserMoods(Guid userId)
    {
        var userMoods = moodTrackerService.GetUserMoods(userId);
        return Ok(userMoods);
    }
}