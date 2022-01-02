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

            string[] items = new string[] {"Canola Oil","Chia Seeds","Hazelnut","Pine Nuts",
                "Mustard Oil","Sunflower Seeds","Sesame Oil","Pistachio","Olive Oil","Mustard Seeds","Poppy Seeds",
                "Sesame Seeds","Peanuts","Chironji","Cashew Nuts","Blanched Almonds", "Almonds","Walnuts", // nuts and oilseed
                "Brown Sugar","Sugar Candy","Icing Sugar","Honey","Jaggery","Golden Syrup",
                "Sugar","Castor Sugar","Caramel","Cane Sugar", // Sugar
                "Shrimp","Tuna Fish","Shellfish","Shark","Hilsa","Sardines",
                "Salmon","Prawns","Pomfret","Perch","Mussels","Mullet","Squids","Haddock", "Flounder","Fish Stock",
                "Fish","Fish Fillet","Cuttle fish","Cod","Clams", "Cat fish","Mackerel","Anchovies", // Fish, Seafood
                "Almond Milk","Red Wine Vinegar","Red Wine","Margarine","Soy Milk","White Wine","Yeast",
                "White Pepper","Rice Vinegar","Sea Salt","Hoisin Sauce","Malt Vinegar","Chocolate Chips",
                "Quinoa","Rice Flour","Polenta","Oyster Sauce","Guchchi",
                "Flat Noodles","Balsamic Vinegar","Coconut Oil","Barfi","Rice Noodles","Coffee","Beer","Chocolate",
                "Sake","Vinaigrette","Vanilla Essence","Tortilla","Tomato Puree","Vegetable Oil",
                "Tartaric Acid","Soya Sauce","Vinegar","Sharbat","Vermicelli","Sev","Rum",
                "Roux","Petha","Pasta","Papad","Paan","Meringue","Mayonnaise","Melon Seeds","Lotus Seeds",
                "Vetiver","Screw Pine","Jus","Jelly","Rose Water","Gold Leaves","Glycerine","Gelatin",
                "Fish Sauce","Desiccated Coconut","Cranberry Sauce","Cornflour", "Cognac","Coconut Water",
                "Coconut Milk","Cocoa","Tea","Brown Sauce","Baking Soda","Tofu","Baking Powder",
                "Arrowroot","Egg","Alum","Marzipan","Ajinomoto","Agar", // Other
                "Cranberry","Kiwi","Blueberries","Mango","Watermelon","Tomato",
                "Strawberry","Water Chestnut","Papaya","Orange Rind","Orange","Olives","Pear","Sultana",
                "Mulberry","Lychee","Lemon Juice","Lemon Rind","Lemon","Raisins","Jamun","Tamarind","Grapefruit",
                "Indian Gooseberry","Dried Fruit","Dates","Custard Apple",
                "Currant","Cooking Apples","Coconut","Cherry","Cape Gooseberry","Banana","Peach","Apricots",
                "Apples","Figs","Grapes","Pomegranate","Pineapple","Guava","Plum", // Fruits
                "Gruyere Cheese","Gouda Cheese","Feta Cheese","Milk","Brie Cheese","Cream Cheese",
                "Ricotta Cheese","Parmesan Cheese","Blue Cheese","Cheddar Cheese","Mascarpone Cheese",
                "Cream","Provolone Cheese","Mozzarella Cheese","Khoya","Hung Curd","Yogurt","Cottage Cheese",
                "Condensed Milk","Clarified Butter","Buttermilk","Butter","Sour Cream", // Diary products
                "Beef","Turkey","Skinned Chicken","Pork", "Partridge","Meat Stock","Keema","Mutton Liver","Ham",
                "Kidney Meat","Crab","Chicken Stock","Chicken Liver","Chicken","Chops","Lamb Meat","Quail","Mutton",
                "Bacon", // Meat
                "Amaranth","Flour","Muesli","Oats","Jowar","Brown Rice",
                "Arborio Rice","Water Chestnut flour","Barnyard Millet","Tapioca","Semolina","Finger Millet",
                "Puffed Rice","Buckwheat","Kidney Beans","Green Gram","Flour","Husked Black Gram","Husked Green Gram",
                "Couscous","Cornmeal","Pressed Rice","Rice","Breadcrumbs","Bread","Black-eyed Beans","Black Gram",
                "Black Beans","Gram flour","Bengal Gram","Chickpeas","Basmati Rice","Barley","Pearl Millet",
                "Beansprouts","Pigeon Pea", // Cereal and Pulses
                "Coriander Powder","Chives","Galangal","Tulsi","Sage","Rosemary","Oregano","Nasturtium","Salt",
                "Mustard Powder","Paprika","Mint Leaves",
                "Marjoram","Lemongrass","Red Chilli","Saffron","Dried Fenugreek Leaves","Kashmiri Mirch","Onion Seeds",
                "Mace","Nutmeg","Herbs","Thyme","Turmeric","Garam Masala","Five Spice Powder",
                "Fenugreek Seeds","Fennel","Green Cardamom","Dry Ginger Powder","Dill","Curry Leaves",
                "Cumin Seeds","Coriander Seeds","Coriander Leaves","Cloves","Cinnamon","Cayenne","Caraway Seeds",
                "Cajun Spices", "Rock Salt","Black Pepper","Bay Leaf","Basil","Black Cardamom","Asafoetida","Aniseed",
                "Carom Seeds","Parsley","Acacia", // Spices and Herbs
                "Bok Choy","Snake Beans","Sorrel Leaves","Rocket Leaves",
                "Drumstick","Cherry Tomatoes","Kaffir Lime","Plantain","Turnip","Sweet Potatoes","Round Gourd",
                "Ridge Gourd","Pimiento","Spinach","Onion","Mustard Leaves","Mushroom","Radish",
                "Shallots","Lettuce","Leek","Pumpkin","Yam","Jalapeno","Jackfruit","Horseradish","Spring Onion",
                "Green Peas","Green Chillies","Gherkins","Garlic","French Beans","Fenugreek","Cucumber","Zucchini",
                "Corn","Celery","Cauliflower","Carrot","Capsicum","Capers","Broccoli","Bottle Gourd","Bitter Gourd",
                "Lotus Stem","Pepper","Beetroot","Pigweed","Cabbage","Bamboo Shoot","Baby Corn","Avocado"
                ,"Eggplant","Asparagus","Artichoke","Colocasia","Potatoes","Ginger"};

            Ingredient[] ingredients = new Ingredient[items.Length];
            for(int i=0; i<items.Length; i++) {
                ingredients[i] = new Ingredient {ingredientName=items[i]};
            }

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

            var quantities = new Quantity[] {
                new Quantity{RecipeID=2, IngredientID=1, quantity=1},
                new Quantity{RecipeID=2, IngredientID=2, quantity=1},
                new Quantity{RecipeID=2, IngredientID=3, quantity=2},
                new Quantity{RecipeID=2, IngredientID=5, quantity=0.25f},
                new Quantity{RecipeID=2, IngredientID=6, quantity=0.75f},
                new Quantity{RecipeID=1, IngredientID=15, quantity=1},
                new Quantity{RecipeID=1, IngredientID=1, quantity=4},
                new Quantity{RecipeID=1, IngredientID=16, quantity=1},
                new Quantity{RecipeID=1, IngredientID=2, quantity=2},
                new Quantity{RecipeID=1, IngredientID=12, quantity=1}
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