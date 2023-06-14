using EndProject.Backend.Models;

namespace EndProject.Backend.Repository
{
    public interface IRecipeRepository
    {
        /*
         * Recipes
         * create
         * get all
         * get one
         * update
         * delete
         */
        Recepten AddRecipe (Recepten recept);
        IEnumerable<Recepten> GetAllRecipes();
        IEnumerable<Recepten> GetRandomRecipes(int count);
        Recepten GetOneRecipe(int id);
        Recepten UpdateRecipe (Recepten recept);
        Recepten DeleteRecipe(int id);

        /*
         * Ingredients
         * create
         * get all
         * get one
         * update
         * delete
         */
        Ingredienten AddIngredient (Ingredienten ingredient);
        IEnumerable<Ingredienten> GetAllIngredients();
        Ingredienten GetOneIngredient(int id);
        Ingredienten UpdateIngredient(Ingredienten ingredient);
        Ingredienten DeleteIngredient(int id);
    }
}
