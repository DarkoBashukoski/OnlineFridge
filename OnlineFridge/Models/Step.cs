namespace OnlineFridge.Models {
    public class Step {
        public int StepID {get; set;}
        public int RecipeID {get; set;}
        public int stepNumber {get; set;}
        public string? stepDescription {get; set;}

        public Recipe? recipe {get; set;}
    }
}