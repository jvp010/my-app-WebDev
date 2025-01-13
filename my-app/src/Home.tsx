import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Products, { Product } from "./Assets/Products";
import UserDashBoard from "./UserDashBoard";
import AdminDashBoard from "./AdminDashBoard";

export type User = {
    id: string;
    first_Name?: string;
    last_Name?: string;
    email?: string;
    password?: string;
    recurring_Days: number;
    role: string;
};

const Home = () => {
    const [userInfo, setUserInfo] = useState<User | undefined>(undefined);

    useEffect(() => {
        fetch('http://localhost:5143/api/Auth/CheckUser', {
            method: 'GET',
            credentials: 'include'
        })
            .then((response) => {
                if (response.ok) {
                    return response.json();
                } else {
                    return response.text().then(text => { throw new Error(text) });
                }
            })
            .then((data) => {
                console.log('Data:', data);
                setUserInfo(data);
            })
            .catch(error => {
                console.log('Error:', error);
                window.location.href = '/';

            });
    }, []);

    if (userInfo && userInfo.role !== 'Admin' && userInfo.role !== 'User') {
        window.location.href = '/'; 
        return null; 
    }

    if (!userInfo) {
        return null; 
    }

    const dashboard = () => {
        if(userInfo.role === 'Admin') return (
            <div>
                <AdminDashBoard/>
            </div>
        ) 
        else return (
            <div>
                {/* <UserDashBoard /> */}
                <AdminDashBoard/>

            </div>
        );
    }

    return (
        <div>
            <h1>Welcome back {userInfo.first_Name} {userInfo.last_Name} to the Home Page</h1>
            <p>Role: {userInfo.role}</p>
            {dashboard()}
        </div>
    );
};

export default Home;


