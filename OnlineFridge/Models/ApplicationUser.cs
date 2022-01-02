using Microsoft.AspNetCore.Identity;

namespace OnlineFridge.Models {
    public class ApplicationUser : IdentityUser {
        public string? firstName {get; set;}
        public string? lastName {get; set;}

        public List<Recipe>? recipes {get; set;}
        public List<UserIngredient>? ingredients {get; set;}
    }
}