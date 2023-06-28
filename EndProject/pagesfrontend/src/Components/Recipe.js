import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import Menu from "./Menu";
function Recipe() {
    const { id } = useParams();
    const [singleRecipe, setSingleRecipe] = useState({})

    useEffect(() => {
        fetch(`https://localhost:7289/Recepten/${id}`)
            .then((res) => res.json())
            .then((data) => {
                /* console.log(data);*/
                setSingleRecipe(data);
            });

    }, [singleRecipe]);  
   
   
    return (
        <>
        <Menu/>
            <h1>Recipe</h1>
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
    )
}
export default Recipe;