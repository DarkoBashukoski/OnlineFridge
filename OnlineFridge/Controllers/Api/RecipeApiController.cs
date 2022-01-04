using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFridge.Data;
using OnlineFridge.Models;

namespace OnlineFridge.Controllers_Api
{
    [Route("api/Recipe")]
    [ApiController]
    public class RecipeApiController : ControllerBase
    {
        private readonly FridgeContext _context;
        private readonly IWebHostEnvironment _environment;


        public RecipeApiController(FridgeContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/Recipe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            return await _context.Recipes.Include(m => m.steps).Include(m => m.quantities).ThenInclude(m => m.ingredient).ToListAsync();
        }

        // GET: api/Recipe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _context.Recipes.Where(m => m.RecipeID == id).Include(m => m.steps).Include(m => m.quantities).ThenInclude(m => m.ingredient).FirstAsync();

            if (recipe == null)
            {
                return NotFound();
            }

            return recipe;
        }

        // PUT: api/Recipe/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.RecipeID)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Recipe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            var steps = recipe.steps;
            var quantities = recipe.quantities;
            recipe.steps = null;
            recipe.quantities = null;

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            foreach (Step s in steps) {
                s.RecipeID = recipe.RecipeID;
            }
            _context.Steps.AddRange(steps);

            foreach (Quantity q in quantities) {
                q.RecipeID = recipe.RecipeID;
                var temp = q.ingredient.IngredientID;
                q.ingredient = null;
                q.IngredientID = temp;
            }
            _context.Quantities.AddRange(quantities);

            await _context.SaveChangesAsync();
            return await _context.Recipes.Where(m => m.RecipeID == recipe.RecipeID).Include(m => m.steps).Include(m => m.quantities).ThenInclude(m => m.ingredient).FirstAsync();
        }

        // DELETE: api/Recipe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            var path = Path.Combine(this._environment.WebRootPath, "images/RecipeImages");
            var filePath = Path.Combine(path, id + ".png");
            new FileInfo(filePath).Delete();

            return NoContent();
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.RecipeID == id);
        }
    }
}
