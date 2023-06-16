using EndProject.Backend.Data;
using EndProject.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Backend.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        public Ingredient AddIngredient(Ingredient ingredient)
        {
            using (var db = new RecipeContext())
            {
                db.Ingredienten.Add(ingredient);
                db.SaveChanges();
                return ingredient;
            }
        }

        public Recept AddRecipe(Recept recept) // also want to add the ingredients belonging to this recept
        {
           using (var db = new RecipeContext())
            {
                db.Recepten.Add(recept);
                db.SaveChanges();
                return recept;
            }
        }

        public Recept ChosenRecipe(int id)
        {
            using (var db = new RecipeContext())
            {
                var result = db.Recepten.Find(id);
                result.Gekozen = DateTime.UtcNow;
                db.Recepten.Update(result);
                db.SaveChanges();
                return result;
            }
        }

        public Ingredient DeleteIngredient(int id)
        {
            using (var db = new RecipeContext())
            {
                var ingredient = db.Ingredienten.Find(id);
                if (ingredient != null)
                {
                    ingredient.Verwijderd = DateTime.UtcNow;
//                    db.Ingredients.Remove(ingredient);
                    db.SaveChanges();
                    return ingredient;
                }
                return null;
            }
        }

        public Recept DeleteRecipe(int id) // when deleting a recipe also delete ingredient with that recipe_id
        {
            using (var db = new RecipeContext())
            {
                var results = db.Recepten.Include(r => r.ingredienten).ToList();
                var recipe = results.SingleOrDefault(i => i.Receptid == id);
                if (recipe != null)
                {
                    recipe.Verwijderd = DateTime.UtcNow;
                    recipe.ingredienten.ToList().ForEach(i => { i.Verwijderd = DateTime.UtcNow; });
                    db.SaveChanges(); 
                    return recipe;
                }
                return null;
            }
        }

        public IEnumerable<Ingredient> GetAllIngredients() // except for the ones where verwijderd != null
        {
            using (var db = new RecipeContext())
            {
                  return db.Ingredienten.ToList(); 
            }
        }

        public IEnumerable<Recept> GetAllRecipes()
        {
            using (var db = new RecipeContext())
            {
                return db.Recepten.Include(r => r.ingredienten).Where(r =>r.Verwijderd == null).ToList();
            }
        }

        public IEnumerable<Recept> GetChosenRecipes()
        {
            throw new NotImplementedException();
        }

        public Ingredient GetOneIngredient(int id) // except for when verwijderd != null 
        {
            Ingredient result;
            using (var db = new RecipeContext())
            {
                result = db.Ingredienten.Find(id);    
            }
            return result;
        }

        public Recept GetOneRecipe(int id) 
        {
            Recept result;
            using (var db = new RecipeContext())
            {
                var results = db.Recepten.Include(r => r.ingredienten).ToList(); ;
                result = results.SingleOrDefault(i => i.Receptid == id && i.Verwijderd == null);
            }
            return result;
        }

        public IEnumerable<Recept> GetRandomRecipes(int count)
        {
            List<Recept> RandomRecipes = new List<Recept>();
            Random random = new Random();
            var r = this.GetAllRecipes().ToList(); //where recipe in recipe_chosen date_chosen != date from past 7 days && where Receptid is not already in there
            for (int i = 0; i < count; i++)
            {
                RandomRecipes.Add(r[random.Next(r.Count)]);
            }
            return RandomRecipes;
        }

        public Recept ResetChosenRecipe(int id)
        {
            using (var db = new RecipeContext())
            {
                var result = db.Recepten.Find(id);
                result.Gekozen = null;
                db.Recepten.Update(result);
                db.SaveChanges();
                return result;
            }
        }

        public Ingredient UpdateIngredient(Ingredient ingredient)
        {
            using (var db = new RecipeContext())
            {
                db.Ingredienten.Update(ingredient);
                db.SaveChanges();
                return ingredient;
            }
        }

        public Recept UpdateRecipe(Recept recept)
        {
            using (var db = new RecipeContext())
            {
                db.Recepten.Update(recept);
                db.SaveChanges();
                return recept;
            }
        }
    }
}
