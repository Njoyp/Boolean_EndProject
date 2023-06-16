import { useState, useEffect } from 'react';

function Amount() {
    const [Amount, setAmount] = useState(0);
    const [recipes, setRecipes] = useState([]);
    const [selectedRecipes, setSelectedRecipes] = useState([]);

    const handleChange = (event) => {
        setAmount(event.target.value);
    };

    useEffect(() => {
        if (Amount > 0) {
            fetch(`https://localhost:7289/Recepten/random/${Amount}`)
                .then((res) => res.json())
                .then((data) => {
                    console.log(data);
                    setRecipes(data);
                });
        }
    }, [Amount]);

    //const handleSubmit = (event) => {
    //    event.preventDefault();
    //    alert("You would like " + Amount + " recipes this week");
    //};
    const buttonHandler = (e) => {
        console.log(e.target.value);
        /*setSelectedRecipes(e.target.value)*/
    }
    return (
        <>
            <form /*onSubmit={handleSubmit}*/>
                <label>
                    How many recipes do you want?
                    <select value={Amount} onChange={handleChange}>
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                        <option value="4">4</option>
                        <option value="5">5</option>
                        <option value="6">6</option>
                        <option value="7">7</option>
                    </select>
                    {/*<input type="submit" value="Submit" />*/}
                </label>
            </form>
            <h3>
                Random recipe name
            </h3>
            <table>
                <thead>
                    <tr>
                        <td>
                        Name
                        </td>
                        <td>
                        Tijd
                        </td>
                    </tr>
                </thead>
                    <tbody>
                        {recipes.map(recipe => (
                            <tr key={recipe.Receptid}>
                                
                                <td>
                                    {recipe.naam}
                                </td>
                                <td>
                                    {recipe.tijd_min }
                                </td>
                                <td>
                                    <button onClick={buttonHandler}>accept</button>
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
                        <tr key={srecipe.Receptid}>
                            <th scope="row"></th>
                            <td>
                                {srecipe.naam}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </>
    );
}

export default Amount;
