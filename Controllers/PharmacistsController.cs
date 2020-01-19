using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmaCite_Pharmacy_Managment_System.Models;

namespace PharmaCite_Pharmacy_Managment_System.Controllers
{
    public class PharmacistsController : Controller
    {
        private readonly pharmacyVaultContext _context;

        public PharmacistsController(pharmacyVaultContext context)
        {
            _context = context;
        }

        // GET: Pharmacists
        public async Task<IActionResult> Index()
        {
            var pharmacyVaultContext = _context.Pharmacist.Include(p => p.HiredByNavigation);
            return View(await pharmacyVaultContext.ToListAsync());
        }

        // GET: Pharmacists/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacist = await _context.Pharmacist
                .Include(p => p.HiredByNavigation)
                .FirstOrDefaultAsync(m => m.PharmacistId == id);
            if (pharmacist == null)
            {
                return NotFound();
            }

            return View(pharmacist);
        }

        // GET: Pharmacists/Create
        public IActionResult Create()
        {
            ViewData["HiredBy"] = new SelectList(_context.Manager, "ManagerId", "ManagerName");
            return View();
        }

        // POST: Pharmacists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PharmacistId,PharmacistName,HiredBy,HiredDate,Pwd")] Pharmacist pharmacist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pharmacist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HiredBy"] = new SelectList(_context.Manager, "ManagerId", "ManagerName", pharmacist.HiredBy);
            return View(pharmacist);
        }

        // GET: Pharmacists/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacist = await _context.Pharmacist.FindAsync(id);
            if (pharmacist == null)
            {
                return NotFound();
            }
            ViewData["HiredBy"] = new SelectList(_context.Manager, "ManagerId", "ManagerName", pharmacist.HiredBy);
            return View(pharmacist);
        }

        // POST: Pharmacists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("PharmacistId,PharmacistName,HiredBy,HiredDate,Pwd")] Pharmacist pharmacist)
        {
            if (id != pharmacist.PharmacistId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pharmacist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PharmacistExists(pharmacist.PharmacistId))
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
            ViewData["HiredBy"] = new SelectList(_context.Manager, "ManagerId", "ManagerName", pharmacist.HiredBy);
            return View(pharmacist);
        }

        // GET: Pharmacists/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pharmacist = await _context.Pharmacist
                .Include(p => p.HiredByNavigation)
                .FirstOrDefaultAsync(m => m.PharmacistId == id);
            if (pharmacist == null)
            {
                return NotFound();
            }

            return View(pharmacist);
        }

        // POST: Pharmacists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var pharmacist = await _context.Pharmacist.FindAsync(id);
            _context.Pharmacist.Remove(pharmacist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PharmacistExists(long id)
        {
            return _context.Pharmacist.Any(e => e.PharmacistId == id);
        }
    }
}
