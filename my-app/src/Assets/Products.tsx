import { useEffect, useState } from "react";

export type Product ={
    id: number;
    name: string;
    price: number;
    description: string;
}

function Products() {
    const [product, setProducts] = useState<Product | undefined>();

    useEffect(() => {
        fetch('http://localhost:5143/products')
        .then(response => response.json())
        .then(json => setProducts(json))
        .catch(error => console.log(error));
    });

    

    return (
        <div>
            <h1>Products</h1>
            <p>ID: {product?.id}</p>
            <p>Name: {product?.name}</p>
            <p>Price: {product?.price}</p>
            <p>Description: {product?.description}</p>
        </div>
    );
}

export default Products;