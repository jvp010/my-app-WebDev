public interface IEventAttendanceService {
    Task<bool> AttendEventAsync(EventAttendance eventAttendance);
    Task<List<Guid>> ListEventAttendeesAsync(Guid event_Id);
    Task<bool> DeleteEventAttendanceAsync(Guid event_Id, Guid user_id);
    bool CheckEventExistance(Guid event_Id);
}
