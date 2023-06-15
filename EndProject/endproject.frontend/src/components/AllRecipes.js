import { useEffect, useState } from "react";

// Helper function to fetch data from the API
const fetchData = async (setRecipes) => {
    //try {
    //    const response = await fetch("https://localhost:7289/Recepten");
    //    if (!response.ok) {
    //        throw new Error("Failed to fetch data from the API");
    //    }
    //    const newData = await response.json();
    //    setRecipes(newData);
    //} catch (error) {
    //    console.error("Error fetching data:", error);
    //}
};

function AllRecipes() {
    const [recipes, setRecipes] = useState([]);

    useEffect(() => {
        fetch("https://localhost:7289/Recepten")
            .then(res => res.json())
            .then(
                (data) => {
                    console.log(data)
                    setRecipes(data);
                })
    }, []);

    const handleOnClick = (event) => {
        event.preventDefault();
        fetchData(setRecipes);
    };

    return (
        <div>
            <table>
            <thead>
            <tbody>
                {recipes.map(recipe => (
                    <tr key={recipe.id}>
                        <th scope="row"></th>
                        <td>
                            {recipe.naam}
                        </td>
                        <td>
                            {recipe.stappen}
                        </td>
                        
                    </tr>
                ))}
                    </tbody>
                </thead>
            </table>
            {/*<p onClick={handleOnClick}>Get all recipes</p>*/}
            {/* Render the fetched recipe data */}
            {/*{recipes.map((recipe) => (*/}
            {/*   <div key={recipe.id}>*/}
            {/*       <h3>{recipe.title}</h3>*/}
            {/*        <p>{recipe.description}</p>*/}
            {/*    </div>*/}
            {/*))}*/}
        </div>
    );
}

export default AllRecipes;
