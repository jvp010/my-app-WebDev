import React, { useState, useEffect } from "react";

type Event = {
    id: string;
    title: string;
    name: string;
    description: string;
    date: string;
    start_Time: string;
    end_Time: string;
    location: string;
    admin_Approval: boolean;
    reviews: Review[];
};

type Review = {
    id: number;
    content: string;
    author: string;
    rating: number;
    date: string;
};

const AdminDashBoard = () => {
    const [events, setEvents] = useState<Event[]>([]);
    const [selectedEvent, setSelectedEvent] = useState<Event | null>(null);
    const [showForm, setShowForm] = useState(false);
    const [newEvent, setNewEvent] = useState<Partial<Event>>({});
    const [error, setError] = useState<string | null>(null);

    const API_BASE_URL = "http://localhost:5143/events";

    useEffect(() => {
        fetchEvents();
    }, []);

    const fetchEvents = async () => {

        const response = await fetch(`${API_BASE_URL}/get`);
        if (!response.ok) throw new Error(await response.text());
        const data = await response.json();
        setEvents(data);

    };

    const addEvent = async (event: Event) => {

        const response = await fetch(`${API_BASE_URL}/add`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(event),
        });
        if (!response.ok) throw new Error(await response.text());
        fetchEvents();
        setShowForm(false);

    };

    const updateEvent = async (id: string, event: Event) => {

        const response = await fetch(`${API_BASE_URL}/${id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(event),
        });
        if (!response.ok) throw new Error(await response.text());
        fetchEvents();
        setShowForm(false);

    };

    const deleteEvent = async (id: string) => {

        const confirmDelete = window.confirm("Are you sure you want to delete this event?");
        if (confirmDelete == false) return;

        const response = await fetch(`${API_BASE_URL}/${id}`, { method: "DELETE" });
        if (!response.ok) throw new Error(await response.text());
        fetchEvents();

    };

    const handleFormSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (selectedEvent) {
            await updateEvent(selectedEvent.id, newEvent as Event);
        } else {
            await addEvent(newEvent as Event);
        }
        setNewEvent({});
        setSelectedEvent(null);
    };

    return (
        <div style={{ padding: "20px" }}>
            <h1>Admin Dashboard</h1>
            <button onClick={() => setShowForm(true)}>Add New Event</button>

            {showForm && (
                <form onSubmit={handleFormSubmit} style={{ margin: "20px 0" }}>
                    <h2>{selectedEvent ? "Edit Event" : "Add Event"}</h2>

                    <input
                        type="text"
                        placeholder="Name"
                        value={newEvent.name || ""}  // Make sure it's bound to newEvent.name
                        onChange={(e) => setNewEvent({ ...newEvent, name: e.target.value })}
                        required
                    />

                    <input
                        type="text"
                        placeholder="Title"
                        value={newEvent.title || ""}
                        onChange={(e) => setNewEvent({ ...newEvent, title: e.target.value })}
                        required
                    />
                    <input
                        type="date"
                        value={newEvent.date || ""}
                        onChange={(e) => setNewEvent({ ...newEvent, date: e.target.value })}
                        required
                    />
                    <input
                        type="time"
                        placeholder="Start Time"
                        value={newEvent.start_Time || ""}
                        onChange={(e) => setNewEvent({ ...newEvent, start_Time: e.target.value })}
                        required
                    />
                    <input
                        type="time"
                        placeholder="End Time"
                        value={newEvent.end_Time || ""}
                        onChange={(e) => setNewEvent({ ...newEvent, end_Time: e.target.value })}
                        required
                    />
                    <input
                        type="text"
                        placeholder="Location"
                        value={newEvent.location || ""}
                        onChange={(e) => setNewEvent({ ...newEvent, location: e.target.value })}
                        required
                    />
                    <textarea
                        placeholder="Description"
                        value={newEvent.description || ""}
                        onChange={(e) => setNewEvent({ ...newEvent, description: e.target.value })}
                        required
                    />
                    <button type="submit">{selectedEvent ? "Update Event" : "Add Event"}</button>
                    <button type="button" onClick={() => setShowForm(false)}>
                        Cancel
                    </button>
                </form>
            )}


            <div>
                {events.map((event) => (
                    <div key={event.id} style={{ border: "1px solid #ccc", padding: "10px", marginBottom: "10px" }}>
                        <h2>{event.title}</h2>
                        <p>{event.description}</p>
                        <p>
                            <strong>Date:</strong> {event.date}
                        </p>
                        <p>
                            <strong>Start Time:</strong> {event.start_Time}
                        </p>
                        <p>
                            <strong>End Time:</strong> {event.end_Time}
                        </p>
                        <p>
                            <strong>Location:</strong> {event.location}
                        </p>
                        <button onClick={() => {
                            setSelectedEvent(event);
                            setNewEvent(event);
                            setShowForm(true);
                        }}>
                            Edit
                        </button>
                        <button onClick={() => deleteEvent(event.id)}>Delete</button>
                    </div>
                ))}
            </div>

            {error && <p style={{ color: "red" }}>{error}</p>}
        </div>
    );
};

export default AdminDashBoard;
