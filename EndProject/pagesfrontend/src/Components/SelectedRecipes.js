import Menu from "./Menu";
import { Link } from 'react-router-dom';
import { useState, useEffect } from 'react';
import axios from 'axios';

function SelectedRecipes() {
    const [selectedRecipes, setSelectedRecipes] = useState([]);
    const [singleRecipe, setSingleRecipe] = useState({})


    const crossOffButton = (receptid) => {
        //const confirm = window.confirm(
        //    `Are your sure you want to cross off ${receptid.naam}?`);
        //if (confirm) {
        axios.delete(`https://localhost:7289/Recepten/RemoveChosen/${receptid}`).then(() => {
            setSelectedRecipes((prev) => [...prev.filter((r) => r.id !== receptid.id)]);
        });
        /*}*/
    }

    const detailButton = (receptid) => {
        
        fetch(`https://localhost:7289/recepten/${receptid}`)
            .then((res) => res.json())
            .then((data) => {
                console.log(data)
                console.log(receptid)
                setSingleRecipe(data);
                window.location.href = `/recipe/${receptid}`;
            });
    }

    useEffect(() => {
        fetch(`https://localhost:7289/Recepten/ChosenRecipes`)
            .then((res) => res.json())
            .then((data) => {
                /* console.log(data);*/
                setSelectedRecipes(data);
            });

    }, [selectedRecipes]);

    return (
        <>
        <Menu/>
            <h1>Selected recipes</h1>
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
                                {/*<Link to={`/recipe/${srecipe.receptid}`}> Details </Link>*/}
                                <button onClick={() => detailButton(srecipe.receptid)}>Details</button>
                               
                            </td>
                            <td>
                                <button onClick={() => crossOffButton(srecipe.receptid)}>Cross off</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </>
    )
}
export default SelectedRecipes;