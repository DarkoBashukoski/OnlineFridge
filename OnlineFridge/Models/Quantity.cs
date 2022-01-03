using System.Text.Json.Serialization;

namespace OnlineFridge.Models {
    public class Quantity {
        [JsonIgnore]
        public int QuantityID {get; set;}
        [JsonIgnore]
        public int RecipeID {get; set;}
        [JsonIgnore]
        public int IngredientID {get; set;}
        public float quantity {get; set;}
        [JsonIgnore]

        public Recipe? recipe {get; set;}
        public Ingredient? ingredient {get; set;}
    }
}