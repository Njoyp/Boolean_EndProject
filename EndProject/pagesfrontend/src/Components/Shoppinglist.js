import Menu from "./Menu";
import { useState, useEffect } from 'react';
import axios from 'axios';
function Shoppinglist() {
    const [ingredients, setIngredients] = useState([]);

    const crossOffIngredient = (id) => {
        //const confirm = window.confirm(
        //    `Are your sure you want to cross off ${id.naam}?`);
        //if (confirm) {
        axios.delete(`https://localhost:7289/Ingredienten/Shoppinglist/${id}`).then(() => {
            setIngredients((prev) => [...prev.filter((i) => i.id !== id.id)]);
        });
        /* }*/
    }

    useEffect(() => {
        fetch(`https://localhost:7289/Ingredienten/Shoppinglist`)
            .then((res) => res.json())
            .then((data) => {
                setIngredients(data);
            });

    }, [ingredients]);
    return (
        <>
        <Menu/>
            <h1>Shopping list</h1>
            <table>
                <thead>
                    <tr>
                        {ingredients.length > 0 && <th>Name</th>}
                        {ingredients.length > 0 && <th>Amount</th>}
                        {ingredients.length > 0 && <th>Unit</th>}
                    </tr>
                </thead>
                <tbody>
                    {ingredients.map(shopping => (
                        <tr key={shopping.id}>
                            <th scope="row"></th>
                            <td>
                                {shopping.naam}
                            </td>
                            <td> {shopping.hoeveelheid}</td>
                            <td> {shopping.eenheid}</td>
                            <td>
                                <button onClick={() => crossOffIngredient(shopping.id)}>Cross off</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </>
    )
}
export default Shoppinglist;