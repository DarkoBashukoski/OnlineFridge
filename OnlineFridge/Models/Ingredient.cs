namespace OnlineFridge.Models {
    public class Ingredient {
        public int IngredientID {get; set;}
        public string? ingredientName {get; set;}

        public List<Quantity>? quantities {get; set;}
    }
}