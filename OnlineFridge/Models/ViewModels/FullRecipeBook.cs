namespace OnlineFridge.Models.ViewModels {
    public class FullRecipeBook {
        public Recipe? recipe {get; set;}
        public List<Quantity>? quantities {get; set;}
        public List<Step>? steps {get; set;}
    }
}