import { useState } from "react";
// 2.1. The student needs to implement a React component that represents the login screen
// •	The component needs to consume the login API build earlier. 
// •	The component needs to display messages it receives from the API (i.e. Password is incorrect)
// •	When the admin user is logged in the component should redirect to the dashboard page
// •	When an unauthorized user tries to reach the administrator dashboard page the application should redirect to the login screen

function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [message, setMessage] = useState("");

    const handleLogin = async (event: React.FormEvent) => {
        event.preventDefault();
        const response = await fetch('http://localhost:5143/api/Auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include', // Include credentials (cookies) in the request
            body: JSON.stringify({ email, password })
        });

        if (response.ok) {
            const data = await response.text();
            setMessage(data);
            window.location.href = '/home'; 
            return <h1>Home</h1>;
        } else {
            const error = await response.text();
            setMessage(error);
        }
    };

    return (
        <div>
            <h1>Login</h1>
            <form onSubmit={handleLogin}>
                <div>
                    <label htmlFor="email">Email:</label>
                    <input
                        type="email"
                        id="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                </div>
                <div>
                    <label htmlFor="password">Password:</label>
                    <input
                        type="password"
                        id="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                </div>
                <button type="submit">Login</button>
            </form>
            {message && <p>{message}</p>}
        </div>
    );
}

export default Login;