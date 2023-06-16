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
        Recept AddRecipe (Recept recept);
        IEnumerable<Recept> GetAllRecipes();
        IEnumerable<Recept> GetRandomRecipes(int count);
        Recept GetOneRecipe(int id);
        Recept UpdateRecipe (Recept recept);
        Recept DeleteRecipe(int id);

        Recept ChosenRecipe(int id);
        IEnumerable<Recept> GetChosenRecipes();
        Recept ResetChosenRecipe(int id);

        /*
         * Ingredients
         * create
         * get all
         * get one
         * update
         * delete
         */
        Ingredient AddIngredient (Ingredient ingredient);
        IEnumerable<Ingredient> GetAllIngredients();
        Ingredient GetOneIngredient(int id);
        Ingredient UpdateIngredient(Ingredient ingredient);
        Ingredient DeleteIngredient(int id);

        /*
        Ingredient BuyIngredient(int id);
        IEnumerable<Ingredient> ShoppingList();
        Ingredient RemoveIngredientFromShoppingList(int id);
        */
    }
}
