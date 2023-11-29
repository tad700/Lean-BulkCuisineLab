
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

using FeastFit.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Lean_BultCuisineLab.Controllers
{
    //3c7387a3494f410fb0d2585a5eb6af6d

    public class WebController : Controller
    {
        public IActionResult WebIndex(string cuisine, string query, int maxCalories, int minProtein, int minCarbs, string sortOrder)
        {
            if (User.IsInRole("Admin"))
            {
                FeastFitDbContext db = new FeastFitDbContext();
                var recipes = db.recipes.AsQueryable().ToList();

                if (!recipes.Any())
                {
                    recipes = FetchRecipesFromAPI(cuisine, query, maxCalories, minProtein, minCarbs);
                    db.recipes.AddRange(recipes);
                    db.SaveChanges();
                }

                switch (sortOrder)
                {
                    case "title_desc":
                        recipes = recipes.OrderByDescending(r => r.Title).ToList();
                        break;
                    case "maxCalories":
                        recipes = recipes.OrderBy(r => r.MaxCalories).ToList();
                        break;
                    case "proteinAmount":
                        recipes = recipes.OrderBy(r => r.ProteinAmount).ToList();
                        break;
                    case "carbAmount":
                        recipes = recipes.OrderBy(r => r.CarbAmount).ToList();
                        break;
                    default:
                        recipes = recipes.OrderBy(r => r.Title).ToList();
                        break;
                }

                return View(recipes);
            }
            else if (User.IsInRole("User"))
            {
                FeastFitDbContext db = new FeastFitDbContext();
                var recipes = db.recipes.AsQueryable().ToList();
                switch (sortOrder)
                {
                    case "title_desc":
                        recipes = recipes.OrderByDescending(r => r.Title).ToList();
                        break;
                    case "maxCalories":
                        recipes = recipes.OrderBy(r => r.MaxCalories).ToList();
                        break;
                    case "proteinAmount":
                        recipes = recipes.OrderBy(r => r.ProteinAmount).ToList();
                        break;
                    case "carbAmount":
                        recipes = recipes.OrderBy(r => r.CarbAmount).ToList();
                        break;
                    default:
                        recipes = recipes.OrderBy(r => r.Title).ToList();
                        break;
                }
                return(View(recipes));
            }
          return(View());
        }

      

        private List<Recipe> FetchRecipesFromAPI(string cuisine, string query, int maxCalories, int minProtein, int minCarbs)
        {
            var apiKey = "3c7387a3494f410fb0d2585a5eb6af6d"; // Replace with your Spoonacular API key
            const int NUMBER = 5;

            var url = $"https://api.spoonacular.com/recipes/complexSearch?apiKey={apiKey}&minProtein={minProtein}&minCarbs={minCarbs}&maxCalories={maxCalories}&cuisine={cuisine}&query={query}";

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var body = response.Content.ReadAsStringAsync().Result;
                    JObject data = JObject.Parse(body);

                    var newRecipes = new List<Recipe>();
                    foreach (var result in data["results"].Take(NUMBER))
                    {
                        Recipe n = new Recipe
                        {
                            IDOfRecipe = (int)result["id"],
                            Title = (string)result["title"],
                            MaxCalories = (int)result["nutrition"]["nutrients"][0]["amount"],
                            ProteinAmount = (int)result["nutrition"]["nutrients"][1]["amount"],
                            CarbAmount = (int)result["nutrition"]["nutrients"][2]["amount"],
                            RecipeImage = (string)result["image"]
                        };
                        newRecipes.Add(n);
                    }
                    return newRecipes;
                }

                return new List<Recipe>();
            }
        }

        [HttpPost]
        public IActionResult DeleteAllRecords()
        {
            FeastFitDbContext db = new FeastFitDbContext();
            db.recipes.RemoveRange(db.recipes);
            db.SaveChanges();


            return RedirectToAction("WebIndex");
        }


    }


}


