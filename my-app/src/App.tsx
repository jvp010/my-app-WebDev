import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Login from "./LogIn/Login";
import Home from "./Home";
import Products from "./Assets/Products";

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Login />} />
                <Route path="/home" element={<Home />} />
            </Routes>
        </Router>

    );
}

export default App;