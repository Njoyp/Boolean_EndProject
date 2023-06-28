import { useState, useEffect } from 'react';
import Menu from "./Menu";
function Recipe() {
    const [singleRecipe, setSingleRecipe] = useState({})


    //`https://localhost:7289/Recepten/${receptid}`
    return (
        <>
        <Menu/>
            <h1>Recipe</h1>
        </>
    )
}
export default Recipe;