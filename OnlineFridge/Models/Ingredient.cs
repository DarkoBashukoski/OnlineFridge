namespace OnlineFridge.Models {
    public class Ingredient {
        public int IngredientID {get; set;}
        public string? ingredientName {get; set;}

        public ICollection<Quantity>? quantities {get; set;}
    }
}