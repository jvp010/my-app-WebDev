using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class EventsController : Controller
{
    private bool _isAdminLoggedIn;

    // GET /events - Get all events
    [HttpGet("/Events/get")]
    public IActionResult GetEvents()
    {
        var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data/Events.json");

        if (System.IO.File.Exists(jsonFilePath))
        {
            var jsonData = System.IO.File.ReadAllText(jsonFilePath);
            var events = JsonSerializer.Deserialize<List<Event>>(jsonData);
            return Ok(events);
        }

        return NotFound("Events file not found.");
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
        isAdminLoggedIn();
    }

    private void isAdminLoggedIn()
    {
        _isAdminLoggedIn = HttpContext.Session.GetString("Role") == "Admin";
    }

    // GET /events/{id} - Get event by Id
    [HttpGet("{id}")]
    public IActionResult GetEventById(Guid id) // Change to Guid
    {
        if (_isAdminLoggedIn == false) return Unauthorized("only admin can do this");
        var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data/Events.json");

        if (System.IO.File.Exists(jsonFilePath))
        {
            var jsonData = System.IO.File.ReadAllText(jsonFilePath);
            var events = JsonSerializer.Deserialize<List<Event>>(jsonData);
            var eventItem = events.FirstOrDefault(e => e.Id == id);

            if (eventItem != null)
            {
                return Ok(eventItem);
            }

            return NotFound($"Event with Id {id} not found.");
        }

        return NotFound("Events file not found.");
    }

    // Add a review to an event
    [HttpPost("{eventId}/reviews")]
    public IActionResult AddReviewToEvent(Guid eventId, [FromBody] Review review)
    {
        if (_isAdminLoggedIn == false) return Unauthorized("only admin can do this");
        var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data/Events.json");

        if (System.IO.File.Exists(jsonFilePath))
        {
            var jsonData = System.IO.File.ReadAllText(jsonFilePath);
            var events = JsonSerializer.Deserialize<List<Event>>(jsonData);
            var eventToUpdate = events.FirstOrDefault(e => e.Id == eventId);

            if (eventToUpdate == null)
            {
                return NotFound($"Event with Id {eventId} not found.");
            }

            // Set the review Id based on the current count of reviews
            review.Id = eventToUpdate.Reviews.Count + 1; // Simple increment for new review Ids
            eventToUpdate.Reviews.Add(review);

            // Save changes back to the JSON file
            var updatedJsonData = JsonSerializer.Serialize(events, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(jsonFilePath, updatedJsonData);

            return Ok(eventToUpdate);
        }

        return NotFound("Events file not found.");
    }

    // POST /events - Add a new event
    [HttpPost("add")]
    public IActionResult AddEvent([FromBody] Event newEvent)
    {
        // if (_isAdminLoggedIn == false) return Unauthorized("only admin can do this");
        var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data/Events.json");

        if (System.IO.File.Exists(jsonFilePath))
        {
            var jsonData = System.IO.File.ReadAllText(jsonFilePath);
            var events = JsonSerializer.Deserialize<List<Event>>(jsonData);

            newEvent.Id = Guid.NewGuid(); 
            events.Add(newEvent);

            System.IO.File.WriteAllText(jsonFilePath, JsonSerializer.Serialize(events, new JsonSerializerOptions { WriteIndented = true }));

            return Ok(newEvent);
        }

        return NotFound("Events file not found.");
    }

    // PUT /events/{id} - Update an event
    [HttpPut("{id}")]
    public IActionResult UpdateEvent(Guid id, [FromBody] Event updatedEvent) // Change int to Guid
    {
        // if(_isAdminLoggedIn == false) return Unauthorized("only admin can do this");
        var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data/Events.json");

        if (System.IO.File.Exists(jsonFilePath))
        {
            var jsonData = System.IO.File.ReadAllText(jsonFilePath);
            var events = JsonSerializer.Deserialize<List<Event>>(jsonData);

            var eventItem = events.FirstOrDefault(e => e.Id == id);
            if (eventItem != null)
            {
                eventItem.Title = updatedEvent.Title; // Change Name to Title
                eventItem.Date = updatedEvent.Date;
                eventItem.Location = updatedEvent.Location;
                eventItem.Start_Time = updatedEvent.Start_Time; // Add Start_Time
                eventItem.End_Time = updatedEvent.End_Time; // Add End_Time
                eventItem.Admin_Approval = updatedEvent.Admin_Approval; // Add Admin_Approval
                eventItem.Description = updatedEvent.Description;

                System.IO.File.WriteAllText(jsonFilePath, JsonSerializer.Serialize(events, new JsonSerializerOptions { WriteIndented = true }));
                System.Console.WriteLine(updatedEvent.Description);

                System.Console.WriteLine(eventItem.Description);
                return Ok(eventItem);
            }

            return NotFound($"Event with Id {id} not found.");
        }

        return NotFound("Events file not found.");
    }

    // DELETE /events/{id} - Delete an event
    [HttpDelete("{id}")]
    public IActionResult DeleteEventById(Guid id) // Rename this method
    {
        // if(_isAdminLoggedIn == false) return Unauthorized("only admin can do this");
        var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data/Events.json");

        if (System.IO.File.Exists(jsonFilePath))
        {
            var jsonData = System.IO.File.ReadAllText(jsonFilePath);
            var events = JsonSerializer.Deserialize<List<Event>>(jsonData);

            var eventItem = events.FirstOrDefault(e => e.Id == id);
            if (eventItem != null)
            {
                events.Remove(eventItem);

                System.IO.File.WriteAllText(jsonFilePath, JsonSerializer.Serialize(events, new JsonSerializerOptions { WriteIndented = true }));

                return Ok($"Event with Id {id} deleted.");
            }

            return NotFound($"Event with Id {id} not found.");
        }

        return NotFound("Events file not found.");
    }
}