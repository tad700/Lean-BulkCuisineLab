
using System.ComponentModel.DataAnnotations;

public class Recipe
{
  
    public long RecipeId { get; set; }
    public long IDOfRecipe { get; set; }
    [Required]
    public string Title { get; set; } = null!;
    public int MaxCalories { get; set; }
    public float ProteinAmount { get; set; }
    public float CarbAmount { get; set; }
    public string RecipeImage { get; set; }

}
