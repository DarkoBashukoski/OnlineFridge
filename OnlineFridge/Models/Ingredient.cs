using System.Text.Json.Serialization;

namespace OnlineFridge.Models {
    public class Ingredient {
        public int IngredientID {get; set;}
        public string? ingredientName {get; set;}

        [JsonIgnore]
        public List<Quantity>? quantities {get; set;}
    }
}