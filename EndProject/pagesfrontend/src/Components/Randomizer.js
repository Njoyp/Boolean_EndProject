import { useState, useEffect } from 'react';
import axios from 'axios';

function Randomizer() {
    const [Amount, setAmount] = useState(0);
    const [recipes, setRecipes] = useState([]);
    const [selectedRecipes, setSelectedRecipes] = useState([]);

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
        </>
    )
}

export default Randomizer;