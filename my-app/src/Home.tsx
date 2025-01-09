import React, { useEffect, useState } from "react";
import { Product } from "./Assets/Products";

export type User = {
    id: string;
    firstName?: string;
    lastName?: string;
    email?: string;
    password?: string;
    recurringDays: number;
    role: string;
};

const Home = () => {
    const [userInfo, setUserInfo] = useState<Product[]>([]);

    useEffect(() => {
        fetch('http://localhost:5143/api/Auth/CheckUser')
        // fetch('http://localhost:5143/products')
        .then(response => response.json())
        .then(json => setUserInfo(json))
        .catch(error => console.log(error));
    });

    const testing = userInfo;
    return (
        <div>
            <h1>Welcome to the Home Page</h1>
            
                <div>
                    <p>ID: {testing[0].id}</p>
                    {/* <p>First Name: {userInfo.firstName}</p>
                    <p>Last Name: {userInfo.lastName}</p>
                    <p>Email: {userInfo.email}</p>
                    <p>Recurring Days: {userInfo.recurringDays}</p>
                    <p>Role: {userInfo.role}</p> */}
                </div>
           
           
        </div>
    );
};

export default Home;
