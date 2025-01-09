import { useState } from "react";

interface DemoProps{}

export default function Demo({}: DemoProps){
    const [count ,setCount] = useState(0);
    return(
        <div>
            <h1>Count: {count}</h1>
            <button onClick={() => setCount(count + 1)}>Increment</button>
        </div>
    )
}