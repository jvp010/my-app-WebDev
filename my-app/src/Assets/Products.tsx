import { useEffect, useState } from "react";

export type Product ={
    id: number;
    name: string;
    price: number;
    description: string;
}

function Products() {
    const [products, setProducts] = useState<Product[]>([]);

    useEffect(() => {
        fetch('http://localhost:5143/products')
        .then(response => response.json())
        .then(json => setProducts(json))
        .catch(error => console.log(error));
    });

    const rows = products.map((p: Product) =>
        <tr key={p.id}>
            <td>{p.id}</td>
            <td>{p.name}</td>
            <td>{p.price}</td>
            <td>{p.description}</td>
        </tr>
    );

    return (
        <div>
            <h1>Products</h1>
            <table>
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Price</th>
                        <th>Description</th>
                    </tr>
                </thead>
                <tbody>
                    {rows}
                </tbody>
            </table>
        </div>
    );
}

export default Products;