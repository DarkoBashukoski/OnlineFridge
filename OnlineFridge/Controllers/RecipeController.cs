using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFridge.Data;
using OnlineFridge.Models;
using OnlineFridge.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
namespace OnlineFridge.Controllers
{
    public class RecipeController : Controller
    {
        private readonly FridgeContext _context;

        public RecipeController(FridgeContext context)
        {
            _context = context;
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
            viewModel.quantities = await _context.Quantities.Where(m => m.RecipeID == id).Include(i => i.ingredient).Include(i => i.measurement).AsNoTracking().ToListAsync();
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

        // POST: Recipe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecipeID,foodCategory,recipeName,recipeDesc,prepTime,cookTime")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
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

            return View(recipe);
        }

        // POST: Recipe/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
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
        public async Task<IActionResult> getData(string json)
        {
            dynamic obj = JObject.Parse(json);
            Recipe r = new Recipe
            {
                recipeName = obj.recipeName,
                recipeDesc = obj.recipeDesc,
                prepTime = obj.prepTime,
                cookTime = obj.cookTime,
                foodCategory = (FoodCategory)Convert.ToInt32(obj.foodCategory)
            };
            _context.Add(r);
            await _context.SaveChangesAsync();

            int i = 1;
            foreach (var step in obj.steps)
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

            return Json(new { id = r.RecipeID });
        }
    }
}