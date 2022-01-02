using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFridge.Data;
using OnlineFridge.Models;
using OnlineFridge.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace OnlineFridge.Controllers
{
    public class RecipeController : Controller
    {
        private readonly FridgeContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly UserManager<ApplicationUser> _userManager;


        public RecipeController(FridgeContext context, IWebHostEnvironment environment, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _environment = environment;
            _userManager = userManager;
        }

        // GET: Recipe
        public async Task<IActionResult> Index(string searchString, int? pageNumber)
        {
            int pageSize = 4;
            ViewData["CurrentFilter"] = searchString;


            var recipes = from r in _context.Recipes select r;
            if (!String.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(r => r.recipeName.Contains(searchString));
            }
            return View(await PagedList<Recipe>.CreateAsync(recipes.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Recipe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var viewModel = new FullRecipeBook();
            viewModel.recipe = await _context.Recipes.AsNoTracking().FirstOrDefaultAsync(m => m.RecipeID == id);
            viewModel.steps = await _context.Steps.Where(m => m.RecipeID == id).OrderBy(m => m.stepNumber).AsNoTracking().ToListAsync();
            viewModel.quantities = await _context.Quantities.Where(m => m.RecipeID == id).Include(i => i.ingredient).AsNoTracking().ToListAsync();
            if (viewModel.recipe == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Recipe/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // GET: Recipe/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            if (_userManager.GetUserId(User) != recipe.ApplicationUserID) {
                return RedirectToAction("PermissionDenied", "Home");
            }
            return View(recipe);
        }

        // POST: Recipe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RecipeID,foodCategory,recipeName,recipeDesc,prepTime,cookTime")] Recipe recipe)
        {
            if (id != recipe.RecipeID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.RecipeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        // GET: Recipe/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .FirstOrDefaultAsync(m => m.RecipeID == id);
            if (recipe == null)
            {
                return NotFound();
            }
            if (_userManager.GetUserId(User) != recipe.ApplicationUserID) {
                return RedirectToAction("PermissionDenied", "Home");
            }

            return View(recipe);
        }

        // POST: Recipe/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (_userManager.GetUserId(User) != recipe.ApplicationUserID) {
                return RedirectToAction("PermissionDenied", "Home");
            }
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.RecipeID == id);
        }

        public List<string> getIngredients()
        {
            return _context.Ingredients.Select(m => m.ingredientName).AsNoTracking().ToList();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(IFormCollection formCollection, IFormFile img)
        {
            Recipe r = new Recipe
            {
                ApplicationUserID = _userManager.GetUserId(User),
                recipeName = formCollection["recipeName"],
                recipeDesc = formCollection["recipeDesc"],
                prepTime = Convert.ToInt32(formCollection["prepTime"]),
                cookTime = Convert.ToInt32(formCollection["cookTime"]),
                foodCategory = (FoodCategory)Convert.ToInt32(formCollection["foodCategory"])
            };
            _context.Add(r);
            await _context.SaveChangesAsync();

            int i = 1;
            foreach (string step in formCollection["steps"])
            {
                _context.Add(new Step
                {
                    stepDescription = step,
                    stepNumber = i,
                    RecipeID = r.RecipeID
                });
                i++;
            }
            await _context.SaveChangesAsync();

            foreach (string ingredient in formCollection["ingredients"])
            {
                dynamic ing = JObject.Parse(ingredient);
                string name = ing["name"];
                _context.Add(new Quantity
                {
                    RecipeID = r.RecipeID,
                    IngredientID = _context.Ingredients.First(m => m.ingredientName == name).IngredientID,
                    quantity = ing["quantiy"]
                });
            }
            await _context.SaveChangesAsync();

            var path = Path.Combine(this._environment.WebRootPath, "images/RecipeImages");
            if (img.Length > 0)
            {
                string filePath = Path.Combine(path, r.RecipeID + ".png");
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(fileStream);
                }
            }

            return Json(new { id = r.RecipeID });
        }
    }
}