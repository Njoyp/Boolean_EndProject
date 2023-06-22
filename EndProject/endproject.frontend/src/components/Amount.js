import { useState, useEffect } from 'react';
import axios from 'axios';
//import { Link } from "react-router-dom";
//import { useNavigate } from '../../../node_modules/react-router-dom/dist/index';
//import Recipe from './Recipe';

function Amount() {
    const [Amount, setAmount] = useState(0);
    const [recipes, setRecipes] = useState([]);
    const [selectedRecipes, setSelectedRecipes] = useState([]);
    const [ingredients, setIngredients] = useState([]);
    const [singleRecipe, setSingleRecipe] = useState({})
    const buttonHandler = (receptid) => {
        axios.put(`https://localhost:7289/Recepten/Chosen/${receptid}`)
            .then((response) => {
                setSelectedRecipes([...selectedRecipes, response.data]);
            })
            .then(() => {
                setRecipes((recipes) => recipes.filter((recipe) => recipe.receptid !== receptid));
            })
            .catch((error) => {
                console.error(error);
            });
    }
    /*    const navigate = useNavigate();*/

    const detailButton = (receptid) => {
        fetch(`https://localhost:7289/Recepten/${receptid}`)
            .then((res) => res.json())
            .then((data) => {
            console.log(data.ingredienten)
            setSingleRecipe(data);
        });
    }

    const crossOffButton = (receptid) => {
        //const confirm = window.confirm(
        //    `Are your sure you want to cross off ${receptid.naam}?`);
        //if (confirm) {
        axios.delete(`https://localhost:7289/Recepten/RemoveChosen/${receptid}`).then(() => {
            setSelectedRecipes((prev) => [...prev.filter((r) => r.id !== receptid.id)]);
        });
        /*}*/
    }

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
        if (Amount > 0) {
            fetch(`https://localhost:7289/Recepten/random/${Amount}`)
                .then((res) => res.json())
                .then((data) => {
                    /*console.log(data);*/
                    setRecipes(data);
                });
        }
    }, [Amount]);

    useEffect(() => {
        fetch(`https://localhost:7289/Recepten/ChosenRecipes`)
            .then((res) => res.json())
            .then((data) => {
                /* console.log(data);*/
                setSelectedRecipes(data);
            });

    }, [selectedRecipes]);

    useEffect(() => {
        fetch(`https://localhost:7289/Ingredienten/Shoppinglist`)
            .then((res) => res.json())
            .then((data) => {
                setIngredients(data);
            });

    }, [ingredients]);
    return (
        <>
            <form>
                <label>
                    How many recipes do you want?
                    <select value={Amount} onChange={(event) => { setAmount(event.target.value) }}>
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                        <option value="4">4</option>
                        <option value="5">5</option>
                        <option value="6">6</option>
                        <option value="7">7</option>
                    </select>
                </label>
            </form>
            <h3>
                Random recipe name
            </h3>
            <table>
                <thead>
                    <tr>
                        {recipes.length > 0 && <th>Name</th>}
                        {recipes.length > 0 && <th>Time</th>}
                    </tr>
                </thead>
                <tbody>
                    {recipes.map(recipe => (
                        <tr key={recipe.receptid}>
                            <td>
                                {recipe.naam}
                            </td>
                            <td>
                                {recipe.tijd_min}
                            </td>
                            <td>
                                <button onClick={() => buttonHandler(recipe.receptid)}>accept</button>

                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            <h3>Selected recipes</h3>
            <table>
                <thead>
                    <tr>
                        <td>

                        </td>
                    </tr>
                </thead>
                <tbody>
                    {selectedRecipes.map(srecipe => (
                        <tr key={srecipe.receptid}>
                            <th scope="row"></th>
                            <td>
                                {srecipe.naam}
                            </td>
                            <td>
                                {/*<Link to={`/recipe/${srecipe.receptid}`}>*/}
                                <button onClick={() => detailButton(srecipe.receptid)}>Details</button>
                                {/*</Link>*/}
                            </td>
                            <td>
                                <button onClick={() => crossOffButton(srecipe.receptid)}>Cross off</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
            <h3>Shopping list</h3>
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
            <h3>Recipe</h3>
            <p>{singleRecipe.naam}</p>
            <p>{singleRecipe.tijd_min}</p>
            <p>{singleRecipe.stappen}</p>
            <table>
                <thead>
                    <tr>{singleRecipe.ingredienten && singleRecipe.ingredienten.length > 0 && <th>Name</th>}
                    {singleRecipe.ingredienten && singleRecipe.ingredienten.length > 0 && <th>Amount</th>}
                    {singleRecipe.ingredienten && singleRecipe.ingredienten.length > 0 && <th>Unit</th>}</tr>

                </thead>
                <tbody>
                    {singleRecipe.ingredienten && singleRecipe.ingredienten.map(x => (
                        <tr key={x.id}>
                            <th scope="row"></th>
                            <td>
                                {x.naam}
                            </td>
                            <td> {x.hoeveelheid}</td>
                            <td> {x.eenheid}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </>
    );
}

export default Amount;