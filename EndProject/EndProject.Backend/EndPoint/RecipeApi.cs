﻿using EndProject.Backend.Models;
using EndProject.Backend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Backend.EndPoint
{
    public static class RecipeApi
    {
        public static void ConfigureRecipeApi(this WebApplication app)
        {
            app.MapPost("/Recepten", PostRecipe);
            app.MapGet("/Recepten", GetAllRecipes);
            app.MapGet("/Recepten/{id}", GetARecipe);
            app.MapGet("/Recepten/random/{count}", GetRandomRecipes);
            app.MapPut("/Recepten", UpdateRecipe);
            app.MapDelete("/Recepten/{id}", DeleteRecipe);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        private static async Task<IResult> PostRecipe(Recepten recipe, IRecipeRepository repository)
        {
            try
            {
                var r = repository.AddRecipe(recipe);
                return r != null ? Results.Created($"https://localhost:7195/Recepten/{recipe.id}", r) : Results.BadRequest("Couldn't create a new recipe, please check your input.");
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> GetAllRecipes(IRecipeRepository recipes)
        {
            try
            {
                return Results.Ok(recipes.GetAllRecipes());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> GetRandomRecipes(int count, IRecipeRepository recipes)
        {

            try
            {
                return Results.Ok(recipes.GetRandomRecipes(count));
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
            
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetARecipe(int id, IRecipeRepository repository)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var recipe = repository.GetOneRecipe(id);
                    if (recipe == null) return Results.NotFound();
                    return Results.Ok(recipe);
                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        private static async Task<IResult> UpdateRecipe(Recepten recipe, IRecipeRepository repository)
        {
            try
            {
                var r = repository.UpdateRecipe(recipe);
                return r != null ? Results.Created($"https://localhost:7195/Ingredienten/{recipe.id}", r) : Results.BadRequest("Couldn't update recipe, please check the input fields.");
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> DeleteRecipe(int id, IRecipeRepository repository)
        {
            try
            {
                var r = repository.DeleteRecipe(id);
                return r != null ? Results.Ok(r) : Results.NotFound($"Couldn't find recipe with id: {id}");
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

    }
}
