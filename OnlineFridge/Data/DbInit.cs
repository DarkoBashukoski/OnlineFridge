using OnlineFridge.Models;
using Microsoft.AspNetCore.Identity;

namespace OnlineFridge.Data {
    public static class DbInit {
        public static void init(FridgeContext context) {
            context.Database.EnsureCreated();

            if (context.Recipes.Any()) {
                return;
            }

            var recipes = new Recipe[] {
                new Recipe{recipeName="Pasta Carbonara", recipeDesc="Pasta carbonara is an indulgent yet surprisingly simple recipe. Featuring bacon (or pancetta) with plenty of Parmesan, this recipe takes only 30 minutes to prepare from start to finish!", prepTime=10, cookTime=20, foodCategory=FoodCategory.Dinner},
                new Recipe{recipeName="Green Veggie Burgers", recipeDesc="Serve in buns, over salad, or all on their own. Any way you serve them, don't skip the creamy Green Goddess sauce over top!", prepTime=20, cookTime=15, foodCategory=FoodCategory.Lunch}
            };

            context.Recipes.AddRange(recipes);
            context.SaveChanges();

            var ingredients = new Ingredient[] {
                new Ingredient{ingredientName="egg"},
                new Ingredient{ingredientName="garlic"},
                new Ingredient{ingredientName="paprika"},
                new Ingredient{ingredientName="salt"},
                new Ingredient{ingredientName="breadcrumbs"},
                new Ingredient{ingredientName="mayonnaise"},
                new Ingredient{ingredientName="avocado"},
                new Ingredient{ingredientName="parsley"},
                new Ingredient{ingredientName="lemon juice"},
                new Ingredient{ingredientName="pepper"},
                new Ingredient{ingredientName="onion"},
                new Ingredient{ingredientName="bacon"},
                new Ingredient{ingredientName="butter"},
                new Ingredient{ingredientName="carrot"},
                new Ingredient{ingredientName="olive oil"},
                new Ingredient{ingredientName="spaghetti"}
            };

            context.Ingredients.AddRange(ingredients);
            context.SaveChanges();

            var steps = new Step[] {
                new Step{RecipeID=1, stepNumber=1, stepDescription="Put a large pot of salted water on to boil (1 tablespoon salt for every 2 quarts of water.)"},
                new Step{RecipeID=1, stepNumber=2, stepDescription="While the water is coming to a boil, heat the olive oil or butter in a large sauté pan over medium heat. Add the bacon or pancetta and cook slowly until crispy."},
                new Step{RecipeID=1, stepNumber=3, stepDescription="Add the garlic (if using) and cook another minute, then turn off the heat and put the pancetta and garlic into a large bowl."},
                new Step{RecipeID=1, stepNumber=4, stepDescription="In a small bowl, beat the eggs and mix in about half of the cheese."},
                new Step{RecipeID=1, stepNumber=5, stepDescription="Once the water has reached a rolling boil, add the dry pasta, and cook, uncovered, at a rolling boil."},
                new Step{RecipeID=1, stepNumber=6, stepDescription="When the pasta is al dente (still a little firm, not mushy), use tongs to move it to the bowl with the bacon and garlic. Let it be dripping wet. Reserve some of the pasta water."},
                new Step{RecipeID=2, stepNumber=1, stepDescription="In a food processor, add the chickpeas, grated carrot, garlic, and spices. Pulse the mixture a few times until combined into a rough paste. Add the egg and breadcrumbs, and pulse a few more times until mixture is evenly mixed."},
                new Step{RecipeID=2, stepNumber=2, stepDescription="Transfer the mixture to a bowl, cover, and chill for at least an hour or up to 24 hours. Chilling helps the mixture hold together a little better when forming the patties and when grilling."},
                new Step{RecipeID=2, stepNumber=3, stepDescription="Patties can also be shaped and grilled immediately, but will be fairly delicate. We recommend using a grill pan to grill the patties."},
                new Step{RecipeID=2, stepNumber=4, stepDescription="Divide burger mixture into six even patties, and transfer to a plate or sheet pan to make it easy to carry to the grill. If you're not grilling right away, loosely cover the patties and store in the refrigerator until you're ready to grill."},
                new Step{RecipeID=2, stepNumber=5, stepDescription="Combine all ingredients for the sauce in a food processor with the blade attachment. Process until the sauce is smooth, has a uniform color, and an even consistency. It should be thick enough to hold its shape on a spoon. Taste the sauce and season to your liking with salt and pepper."}
            };

            context.Steps.AddRange(steps);
            context.SaveChanges();

            var measurements = new Measurement[] {
                new Measurement{measurementName=""},
                new Measurement{measurementName="teaspoon"},
                new Measurement{measurementName="tablespoon"},
                new Measurement{measurementName="gram"},
                new Measurement{measurementName="kilogram"},
                new Measurement{measurementName="cup"},
                new Measurement{measurementName="clove"}
            };

            context.Measurements.AddRange(measurements);
            context.SaveChanges();

            var quantities = new Quantity[] {
                new Quantity{RecipeID=2, IngredientID=1, MeasurementID=1, quantity=1},
                new Quantity{RecipeID=2, IngredientID=2, MeasurementID=7, quantity=1},
                new Quantity{RecipeID=2, IngredientID=3, MeasurementID=2, quantity=2},
                new Quantity{RecipeID=2, IngredientID=5, MeasurementID=3, quantity=0.25f},
                new Quantity{RecipeID=2, IngredientID=6, MeasurementID=6, quantity=0.75f},
                new Quantity{RecipeID=1, IngredientID=15, MeasurementID=3, quantity=1},
                new Quantity{RecipeID=1, IngredientID=1, MeasurementID=1, quantity=4},
                new Quantity{RecipeID=1, IngredientID=16, MeasurementID=5, quantity=1},
                new Quantity{RecipeID=1, IngredientID=2, MeasurementID=7, quantity=2},
                new Quantity{RecipeID=1, IngredientID=12, MeasurementID=5, quantity=1}
            };

            context.Quantities.AddRange(quantities);
            context.SaveChanges();

            var user = new ApplicationUser
            {
                firstName = "Test",
                lastName = "User",
                Email = "test@user.com",
                NormalizedEmail = "XXXX@EXAMPLE.COM",
                UserName = "TestUser",
                NormalizedUserName = "test@example.com",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user,"Test1!");
                user.PasswordHash = hashed;
                context.Users.Add(user);
            }

            context.SaveChanges();
        }
    }
}