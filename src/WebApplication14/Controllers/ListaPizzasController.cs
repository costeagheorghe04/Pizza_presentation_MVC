using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication14.Data;
using WebApplication14.Models;

namespace WebApplication14.Controllers
{    [Authorize]
    public class ListaPizzasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ListaPizzasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ListaPizzas
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ListaPizza.ToListAsync());
        }

        // GET: ListaPizzas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listaPizza = await _context.ListaPizza
                .SingleOrDefaultAsync(m => m.ID == id);
            if (listaPizza == null)
            {
                return NotFound();
            }

            return View(listaPizza);
        }

        // GET: ListaPizzas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ListaPizzas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Ingredients,Price")] ListaPizza listaPizza)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listaPizza);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(listaPizza);
        }

        // GET: ListaPizzas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listaPizza = await _context.ListaPizza.SingleOrDefaultAsync(m => m.ID == id);
            if (listaPizza == null)
            {
                return NotFound();
            }
            return View(listaPizza);
        }

        // POST: ListaPizzas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Ingredients,Price")] ListaPizza listaPizza)
        {
            if (id != listaPizza.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listaPizza);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListaPizzaExists(listaPizza.ID))
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
            return View(listaPizza);
        }

        // GET: ListaPizzas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listaPizza = await _context.ListaPizza
                .SingleOrDefaultAsync(m => m.ID == id);
            if (listaPizza == null)
            {
                return NotFound();
            }

            return View(listaPizza);
        }

        // POST: ListaPizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listaPizza = await _context.ListaPizza.SingleOrDefaultAsync(m => m.ID == id);
            _context.ListaPizza.Remove(listaPizza);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListaPizzaExists(int id)
        {
            return _context.ListaPizza.Any(e => e.ID == id);
        }
    }
}
