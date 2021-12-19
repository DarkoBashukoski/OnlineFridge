using System.ComponentModel.DataAnnotations;

namespace OnlineFridge.Models {
    public enum FoodCategory {
        Breakfast, Lunch, Dinner, Salad, Appetizer, Dessert
    }

    public class Recipe {
        public int RecipeID {get; set;}
        public FoodCategory foodCategory {get; set;}
        [Required]
        public string? recipeName {get; set;}
        [Required]
        public string? recipeDesc {get; set;}
        public int prepTime {get; set;}
        public int cookTime {get; set;}

        public List<Step>? steps {get; set;}
        public List<Quantity>? quantities {get; set;}
    }
}