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

        public IEnumerable<Ingredient> BuyIngredient(int recipeId) 
        {
            using(var db = new RecipeContext())
            {
                var ingredients = db.Ingredienten.Where(x => x.Receptid == recipeId).ToList();
                foreach(var ingredient in ingredients)
                {
                    ingredient.Kopen = true;
                    db.Ingredienten.Update(ingredient);
                }
                db.SaveChanges();
                return ingredients;
            }
        }

        public Recept ChosenRecipe(int id)
        {
            using (var db = new RecipeContext())
            {
                var result = db.Recepten.Find(id);
                result.Gekozen = DateTime.UtcNow;
                db.Ingredienten.Where(x => x.Receptid == id).ToList().ForEach(i => { i.Kopen = true; });
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
                    db.SaveChanges();
                    return ingredient;
                }
                return null;
            }
        }

        public Recept DeleteRecipe(int id) 
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

        public IEnumerable<Ingredient> GetAllIngredients() 
        {
            using (var db = new RecipeContext())
            {
                  return db.Ingredienten.Where(i => i.Verwijderd == null).ToList(); 
            }
        }

        public IEnumerable<Recept> GetAllRecipes() //order ingredienten by name
        {
            using (var db = new RecipeContext())
            { 
                return db.Recepten.Include(r => r.ingredienten).Where(r =>r.Verwijderd == null).ToList();
            }
        }

        public IEnumerable<Recept> GetChosenRecipes()
        {
            using (var db = new RecipeContext())
            {
                var results = db.Recepten.Where(x => x.Gekozen != null).ToList();
                return results;
            }
        }

        public IEnumerable<Ingredient> GetIngredientsRecipe(int RecipeId) 
        {
            using (var db = new RecipeContext())
            {
                return db.Ingredienten.Where(x => x.Receptid == RecipeId).Where(i => i.Verwijderd == null).ToList();    
            }
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
            var r = this.GetAllRecipes().Where(x =>x.Gekozen == null).ToList(); 
            for (int i = 0; i < count; i++)
            {
                int x = random.Next(r.Count);
                RandomRecipes.Add(r[x]); 
                r.RemoveAt(x);
            }
            return RandomRecipes;
        }

        public Ingredient RemoveIngredientFromShoppingList(int id)
        {
            using (var db = new RecipeContext())
            {
                var result = db.Ingredienten.Find(id);
                result.Kopen = false;
                db.Ingredienten.Update(result);
                db.SaveChanges();
                return result;
            }

        }

        public Recept ResetChosenRecipe(int id)
        {
            using (var db = new RecipeContext())
            {
                var results = db.Recepten.Include(r => r.ingredienten).ToList();
                var recipe = results.SingleOrDefault(i => i.Receptid == id);
                if (recipe != null)
                {
                    recipe.Gekozen = null;
                    recipe.ingredienten.ToList().ForEach(i => { i.Kopen = false; });
                    db.SaveChanges();
                    return recipe;
                }
                return null;
            }
            //using (var db = new RecipeContext())
            //{
            //    var result = db.Recepten.Find(id);
            //    result.Gekozen = null;
            //    db.Recepten.Update(result);
            //    db.SaveChanges();
            //    return result;
            //}
        }

        public IEnumerable<Ingredient> ShoppingList() // for the same products groupby naam and add amount & unit??
        {
            using (var db = new RecipeContext())
            {
                var results = db.Ingredienten.Where(x => x.Kopen == true).ToList();
                results.Sort((x, y) => string.Compare(x.Naam, y.Naam));
                return results;
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
