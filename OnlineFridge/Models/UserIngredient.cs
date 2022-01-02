using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineFridge.Models {
    public class UserIngredient {
        public int UserIngredientID {get; set;}
        public string? ApplicationUserID {get; set;}
        public int IngredientID {get; set;}
        public float quantity {get; set;}

        [ForeignKey("ApplicationUserID")]
        public ApplicationUser? applicationUser {get; set;}
        public Ingredient? ingredient {get; set;}
    }
}