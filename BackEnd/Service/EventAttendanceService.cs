////////////////////////////////////////////////
// EventAttendanceService.cs
////////////////////////////////////////////////


public class EventAttendanceService : IEventAttendanceService{
    // write attendance to attendance.json
    // check availabilty of attendance
    public bool CheckEventExistance(Guid event_Id){
        Event? event_ = this._getEvent(event_Id);
        return event_ is not null; // true if event exists, false if not
    }

    private bool _checkItsBeforeEventStart(Guid event_Id){ // Q null check or not???
        Event? event_ = _getEvent(event_Id);
        if(event_ == null) return false; // If the event doesn't exist, attendance is invalid.

        DateOnly eventDate = DateOnly.FromDateTime(event_.Date);
        TimeOnly eventStartTime = event_.Start_Time;
        DateTime eventDT = eventDate.ToDateTime(eventStartTime);
        DateTime now = DateTime.Now;
        return now < eventDT; // true if its before the starttime, false if not
    }

/*
date must be before or on the day
time must be before if on the day
*/

    private bool _hasUserAttendedEvent(Guid event_Id, Guid user_Id){
        List<EventAttendance> eventAttendances = JsonFileHandler.ReadJsonFile<EventAttendance>("Data/EventAttendance.json");
        foreach (EventAttendance ea in eventAttendances){
            if (ea.Event_Id == event_Id && ea.User_Id == user_Id){
                return true;
            }
        }
        return false;
    }

    public async Task<bool> AttendEventAsync(EventAttendance eventAttendance){
        bool eventExists = CheckEventExistance(eventAttendance.Event_Id);
        bool userHasntAttended = _hasUserAttendedEvent(eventAttendance.Event_Id, eventAttendance.User_Id) == false;
        bool eventHasntStarted = _checkItsBeforeEventStart(eventAttendance.Event_Id);
        if (eventExists && userHasntAttended && eventHasntStarted){
                List<EventAttendance> attendances = JsonFileHandler.ReadJsonFile<EventAttendance>("Data/EventAttendance.json");
                attendances.Add(eventAttendance);
                JsonFileHandler.WriteToJsonFile("Data/EventAttendance.json", attendances);
                return true;
        }
        return false;
    }


    private Event? _getEvent(Guid event_Id){
        List<Event> events = JsonFileHandler.ReadJsonFile<Event>("Data/Events.json");
        foreach (Event e in events){ // Q how to improve this?
            if (e.Id == event_Id){
                return e;
            }
        }
        return null;
    }

    public async Task<List<Guid>> ListEventAttendeesAsync(Guid event_Id){
        List<EventAttendance> eventAttendances = JsonFileHandler.ReadJsonFile<EventAttendance>("Data/EventAttendance.json");
        List<Guid> AttendeesIds = new List<Guid>();
        foreach (EventAttendance ea in eventAttendances){
            if (ea.Event_Id == event_Id){
                AttendeesIds.Add(ea.User_Id);
            }
        }
        return AttendeesIds;
    }

    public async Task<bool> DeleteEventAttendanceAsync(Guid event_Id, Guid user_id){
        List<EventAttendance> eventAttendances = JsonFileHandler.ReadJsonFile<EventAttendance>("Data/EventAttendance.json");
        foreach (EventAttendance ea in eventAttendances){
            if (ea.Event_Id == event_Id && ea.User_Id == user_id){
                if (_checkItsBeforeEventStart(ea.Event_Id)){
                    eventAttendances.Remove(ea);
                    JsonFileHandler.WriteToJsonFile("Data/EventAttendance.json", eventAttendances);
                    return true;
                }
            }
        }
        return false;
    }


}
