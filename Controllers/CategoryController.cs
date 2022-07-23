using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseRecorder.Models;

namespace ExpenseRecorder.Controllers
{
    public class CategoryController : Controller
    {
        private readonly WebDbContext _context;

        //constructor, the value will be passed by the dependency injection
        public CategoryController(WebDbContext context)
        {
            _context = context;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {       
              // the view here will sync to the same name as function to the file in Views
              return View(await _context.NetCategories.ToListAsync());
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.NetCategories == null)
            {
                return NotFound();
            }

            var categoryModel = await _context.NetCategories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            return View(categoryModel);
        }

        // GET: Category/CreateOrEdit
        public IActionResult CreateOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new CategoryModel());
            } else
            {
                return View(_context.NetCategories.Find(id));
            }
        }

        // POST: Category/CreateOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit([Bind("CategoryId,Title,Icon,TransactionType")] CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                // when we new object, id default to be 0
                if (categoryModel.CategoryId == 0) {
                    _context.Add(categoryModel);
                } else {
                    _context.Update(categoryModel);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryModel);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NetCategories == null)
            {
                return Problem("Entity set 'WebDbContext.NetCategories'  is null.");
            }
            var categoryModel = await _context.NetCategories.FindAsync(id);
            if (categoryModel != null)
            {
                _context.NetCategories.Remove(categoryModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryModelExists(int id)
        {
          return _context.NetCategories.Any(e => e.CategoryId == id);
        }

        
    }
}
