namespace OnlineFridge.Models {
    public class Quantity {
        public int QuantityID {get; set;}
        public int RecipeID {get; set;}
        public int IngredientID {get; set;}
        public float quantity {get; set;}

        public Recipe? recipe {get; set;}
        public Ingredient? ingredient {get; set;}
    }
}