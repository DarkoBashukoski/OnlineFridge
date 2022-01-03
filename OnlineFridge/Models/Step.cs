using System.Text.Json.Serialization;

namespace OnlineFridge.Models {
    public class Step {
        [JsonIgnore]
        public int StepID {get; set;}
        [JsonIgnore]
        public int RecipeID {get; set;}
        public int stepNumber {get; set;}
        public string? stepDescription {get; set;}

        [JsonIgnore]
        public Recipe? recipe {get; set;}
    }
}