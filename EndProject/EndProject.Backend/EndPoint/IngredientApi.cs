﻿using EndProject.Backend.Models;
using EndProject.Backend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.Backend.EndPoint
{
    public static class IngredientApi
    {
        public static void ConfigureIngredientApi (this WebApplication app)
        {
            app.MapPost("/Ingredienten", PostIngredient);
            app.MapGet("/Ingredienten", GetAllIngredients);
            app.MapGet("/Ingredienten/{RecipeId}", GetRecipeIngredients); 
            app.MapGet("Ingredienten/Shoppinglist", GetShoppingList);
            app.MapPut("/Ingredienten/{RecipeId}", PrepareForShoppinglist);
            app.MapPut("/Ingredienten", UpdateIngredient);
            app.MapDelete("/Ingredienten/{id}", DeleteIngredient);
            app.MapDelete("Ingredienten/Shoppinglist/{id}", RemoveFromShoppinglist);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        private static async Task<IResult> PostIngredient(Ingredient ingredient, IRecipeRepository repository)
        {
            try
            {
                var i = repository.AddIngredient(ingredient);
                return i != null ? Results.Created($"https://localhost:7195/Ingredienten/{ingredient.Id}", i) : Results.BadRequest("Couldn't create a new ingredient, please check your input.");
            }
            catch(Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> GetAllIngredients(IRecipeRepository ingredients)
        {
            try
            {
                return Results.Ok(ingredients.GetAllIngredients());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> GetRecipeIngredients(int Recipeid, IRecipeRepository repository)
        {
            try
            {
                return await Task.Run(() =>
                    {
                        var ingredient = repository.GetIngredientsRecipe(Recipeid);
                        if (ingredient == null) return Results.NotFound("Ingredients not found");
                        return Results.Ok(ingredient);
                    });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> GetShoppingList(IRecipeRepository ingredients)
        {
            try
            {
                return Results.Ok(ingredients.ShoppingList());
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> PrepareForShoppinglist(int RecipeId, IRecipeRepository repository)
        {
            try
            {
                var i = repository.BuyIngredient(RecipeId);
                return i != null ? Results.Ok(i) : Results.NotFound($"Couldn't find ingredient with id: {RecipeId}");
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        private static async Task<IResult> UpdateIngredient(Ingredient ingredient, IRecipeRepository repository)
        {
            try
            {
                var i = repository.UpdateIngredient(ingredient);
                return i != null ? Results.Created($"https://localhost:7195/Ingredienten/{ingredient.Id}", i) : Results.BadRequest("Couldn't update ingredient, please check the input fields.");
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> DeleteIngredient(int id, IRecipeRepository repository)
        {
            try
            {
                var i = repository.DeleteIngredient(id);
                return i != null ? Results.Ok(i): Results.NotFound($"Couldn't find ingredient with id: {id}");
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        private static async Task<IResult> RemoveFromShoppinglist(int id, IRecipeRepository repository)
        {
            try
            {
                var i = repository.RemoveIngredientFromShoppingList(id);
                return i != null ? Results.Ok(i) : Results.NotFound($"Couldn't find ingredient with id: {id}");
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
    }
}
