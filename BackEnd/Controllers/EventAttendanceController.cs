using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

////////////////////////////////////////////////
// EventControllers.cs
////////////////////////////////////////////////
[Route("api/EventAttendance")]
public class EventAttendanceController : Controller {
    private readonly IEventAttendanceService _eventService;
    private bool _isLoggedIn;

    public EventAttendanceController(IEventAttendanceService eventService) {
        _eventService = eventService;
    }

    public override void OnActionExecuting(ActionExecutingContext context) {
        base.OnActionExecuting(context);
        loggedinuser();
    }

    private void loggedinuser() {
        _isLoggedIn = HttpContext.Session.GetString("UserMail") != null;
    }

    [HttpGet("h")]
    public IActionResult Testt() {
        return Ok("worked.");
    }

    [HttpPost("Attend")]
    public async Task<IActionResult> AttendEvent([FromBody] EventAttendance eventAttendance) {
        if (_isLoggedIn == false) return Unauthorized("No user logged in.");
        
        bool result = await _eventService.AttendEventAsync(eventAttendance);
        if (result) {
            return Ok($"Attendance to event with ID {eventAttendance.Event_Id} succeeded.");
        } else {
            return BadRequest($"Attendance to event with ID {eventAttendance.Event_Id} failed.");
        }
    }

    [HttpGet("FindEventAttendees/{event_id}")]
    public async Task<IActionResult> FindEventAttendees(Guid event_id) {
        if (_isLoggedIn == false) return Unauthorized("No user logged in.");

        if (!_eventService.CheckEventExistance(event_id)) {
            return NotFound($"Event with ID {event_id} not found.");
        }

        List<Guid> result = await _eventService.ListEventAttendeesAsync(event_id);
        return Ok(result);
    }

    [HttpGet("DeleteEventAttendance/{event_id}/{user_id}")]
    public async Task<IActionResult> DeleteEventAttendance(Guid event_id, Guid user_id) {
        if (_isLoggedIn == false) return Unauthorized("No user logged in.");

        bool result = await _eventService.DeleteEventAttendanceAsync(event_id, user_id);
        if (result) {
            return Ok($"Deletion of event attendance to event with ID {event_id} and user ID {user_id} succeeded.");
        } else {
            return BadRequest($"Deletion of event attendance with event ID {event_id} and user ID {user_id} failed.");
        }
    }
}
