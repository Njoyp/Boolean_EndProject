import Menu from "./Menu";
import { useState, useEffect } from 'react';
import axios from 'axios';

function AllRecipes() {
    const [recipes, setRecipes] = useState([]);
    const [selectedRecipes, setSelectedRecipes] = useState([]);
    //const addRecipeButton = () => {
    //    axios.post()
    //} first need to change the post recipe API

    const deleteButton = (receptid) => {
        axios.delete(`https://localhost:7289/Recepten/${receptid}`).then(() => {
            setSelectedRecipes((prev) => [...prev.filter((r) => r.id !== receptid.id)]);
        });
    }
    const choseButton = (receptid) => {
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
        fetch(`https://localhost:7289/Recepten`)
            .then((res) => res.json())
            .then((data) => {
                /* console.log(data);*/
                setRecipes(data);
            });

    }, [recipes]);

     return (
         <>
        <Menu/>
             <h1>All recipes</h1>
             {/*<button className="addRecipeButton" onClick={() =>addRecipeButton }>Add recipe</button>*/}
             <table>
                 <thead>
                     <tr>
                         {recipes.length > 0 && <th>Action</th>}
                         {recipes.length > 0 && <th>Name</th>}
                         {recipes.length > 0 && <th>Time</th>}
                         {recipes.length > 0 && <th>Steps</th>}
                         {recipes.ingredienten && recipes.ingredienten.length > 0 && <th>Ingredients</th>}
                     </tr>
                 </thead>
                 <tbody>
                     {recipes.map(recipe => (
                         <tr key={recipe.receptid}>
                             <td>{/*<button>Edit</button> */}
                                 <button onClick={() => deleteButton(recipe.receptid)}>Delete</button>
                                 <button onClick={() => choseButton(recipe.receptid)}>Chose</button>
                             </td> 
                             
                             <td>{recipe.naam}</td>
                             <td>{recipe.tijd_min}</td>
                             <td>{recipe.stappen}</td>
                             <table>
                                 <thead>
                                     <tr>{recipe.ingredienten && recipe.ingredienten.length > 0 && <th>Name</th>}
                                         {recipe.ingredienten && recipe.ingredienten.length > 0 && <th>Amount</th>}
                                         {recipe.ingredienten && recipe.ingredienten.length > 0 && <th>Unit</th>}</tr>
                                 </thead>
                                 <tbody>
                                     {recipe.ingredienten && recipe.ingredienten.map(x => (
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
                         </tr>
                     )) }
                 </tbody>
             </table>
         </>
    )
}
export default AllRecipes;