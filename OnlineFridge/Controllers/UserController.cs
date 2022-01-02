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
    public class UserController : Controller
    {
        private readonly FridgeContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(FridgeContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index() {
            string userId = _userManager.GetUserId(User);
            ProfilePage model = new ProfilePage();
            model.userIngredients = await _context.userIngredients.Where(m => m.ApplicationUserID == userId).Include(m => m.ingredient).ToListAsync();
            model.recipes = await _context.Recipes.Where(m => m.ApplicationUserID == userId).ToListAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string ingredientName, float quantity) {
            _context.Add(new UserIngredient {
                ApplicationUserID = _userManager.GetUserId(User),
                IngredientID = _context.Ingredients.First(m => m.ingredientName == ingredientName).IngredientID,
                quantity = quantity
            });

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id, float change, bool add) {
            UserIngredient userIngredient = await _context.userIngredients.FirstAsync(m => m.UserIngredientID == id);
            if (_userManager.GetUserId(User) != userIngredient.ApplicationUserID) {
                return RedirectToAction("PermissionDenied", "Home");
            }
            userIngredient.quantity = (add) ? userIngredient.quantity + change : userIngredient.quantity - change;
            if (userIngredient.quantity > 0) {
                _context.Update(userIngredient);
            } else {
                _context.Remove(userIngredient);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}