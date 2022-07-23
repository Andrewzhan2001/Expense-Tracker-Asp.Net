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
    public class TransactionController : Controller
    {
        private readonly WebDbContext _context;

        public TransactionController(WebDbContext context)
        {
            _context = context;
        }

        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            var webDbContext = _context.NetTransactions.Include(t => t.Category);
            return View(await webDbContext.ToListAsync());
        }

        // GET: Transaction/CreateOrEdit
        public IActionResult CreateOrEdit(int id = 0)
        {
            SendCategories();
            if (id == 0)
            {
                return View(new TransactionModel());
            }
            return View(_context.NetTransactions.Find(id));
        }

        // POST: Transaction/CreateOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit([Bind("TransactionId,Note,Date,CategoryId,Amount")] TransactionModel transactionModel)
        {
            if (ModelState.IsValid)
            {
                if (transactionModel.TransactionId == 0) {
                    _context.Add(transactionModel);
                } else {
                    _context.Update(transactionModel);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            SendCategories();
            // will return back to the same page
            return View(transactionModel);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.NetTransactions == null)
            {
                return Problem("Entity set 'WebDbContext.NetTransactions'  is null.");
            }
            var transactionModel = await _context.NetTransactions.FindAsync(id);
            if (transactionModel != null)
            {
                _context.NetTransactions.Remove(transactionModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionModelExists(int id)
        {
          return _context.NetTransactions.Any(e => e.TransactionId == id);
        }

        [NonAction]
        public void SendCategories()
        {
            var CategoryCollection = _context.NetCategories.ToList();
            // add a default first option
            CategoryModel DefaultCategory = new CategoryModel() { CategoryId = 0, Title = "Choose a Category" };
            // save the default into categories
            CategoryCollection.Insert(0, DefaultCategory);
            ViewBag.Categories = CategoryCollection;
        }
    }
}
