using EndProject.Backend.Data;
using EndProject.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Backend.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        public Ingredienten AddIngredient(Ingredienten ingredient)
        {
            using (var db = new RecipeContext())
            {
                db.Ingredients.Add(ingredient);
                db.SaveChanges();
                return ingredient;
            }
        }

        public Recepten AddRecipe(Recepten recept) // also want to add the ingredients belonging to this recept
        {
           using (var db = new RecipeContext())
            {
                db.Recipes.Add(recept);
                db.SaveChanges();
                return recept;
            }
        }

        public Ingredienten DeleteIngredient(int id)
        {
            using (var db = new RecipeContext())
            {
                var ingredient = db.Ingredients.Find(id);
                if (ingredient != null)
                {
                    ingredient.verwijderd = DateTime.UtcNow;
//                    db.Ingredients.Remove(ingredient);
                    db.SaveChanges();
                    return ingredient;
                }
                return null;
            }
        }

        public Recepten DeleteRecipe(int id) // when deleting a recipe also delete ingredient with that recipe_id
        {
            using (var db = new RecipeContext())
            {
                var recipe = db.Recipes.Find(id);
                if (recipe != null)
                {
                    recipe.verwijderd = DateTime.UtcNow;
                    db.SaveChanges(); 
                    return recipe;
                }
                return null;
            }
        }

        public IEnumerable<Ingredienten> GetAllIngredients() // except for the ones where verwijderd != null
        {
            using (var db = new RecipeContext()) 
            {
                return db.Ingredients.ToList();
            } ;
        }

        public IEnumerable<Recepten> GetAllRecipes() // except for the ones where verwijderd != null
        {
            using (var db = new RecipeContext())
            {
                return db.Recipes.Include(r => r.ingredienten).ToList();
            }
        }

        public Ingredienten GetOneIngredient(int id) // except for when verwijderd != null
        {
            Ingredienten result;
            using (var db = new RecipeContext())
            {
                result = db.Ingredients.Find(id);
            }
            return result;
        }

        public Recepten GetOneRecipe(int id) // including the ingredients
        {
            Recepten result;
            using (var db = new RecipeContext())
            {
                result = db.Recipes.Find(id);
            }
            return result;
        }

        public IEnumerable<Recepten> GetRandomRecipes(int count)
        {
            List<Recepten> RandomRecipes = new List<Recepten>();
            Random random = new Random();
            var r = this.GetAllRecipes().ToList();
            for (int i = 0; i < count; i++)
            {
                RandomRecipes.Add(r[random.Next(r.Count)]);
            }
            return RandomRecipes;
        }

        public Ingredienten UpdateIngredient(Ingredienten ingredient)
        {
            using (var db = new RecipeContext())
            {
                db.Ingredients.Update(ingredient);
                db.SaveChanges();
                return ingredient;
            }
        }

        public Recepten UpdateRecipe(Recepten recept)
        {
            using (var db = new RecipeContext())
            {
                db.Recipes.Update(recept);
                db.SaveChanges();
                return recept;
            }
        }
    }
}
