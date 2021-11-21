namespace OnlineFridge.Models.ViewModels {
    public class FullRecipeBook {
        public Recipe? recipe {get; set;}
        public IEnumerable<Quantity>? quantities {get; set;}
        public IEnumerable<Step>? steps {get; set;}
        public IEnumerable<Ingredient>? ingredients {get; set;}
        public IEnumerable<Measurement>? measurements {get; set;}
    }
}