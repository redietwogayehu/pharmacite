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
    public class MedicinesSoldsController : Controller
    {
        private readonly pharmacyVaultContext _context;

        public MedicinesSoldsController(pharmacyVaultContext context)
        {
            _context = context;
        }

        // GET: MedicinesSolds
        public async Task<IActionResult> Index()
        {
            var pharmacyVaultContext = _context.MedicinesSold.Include(m => m.SoldByNavigation).Include(m => m.SoldMedicineNavigation).Include(m => m.SoldToNavigation);
            return View(await pharmacyVaultContext.ToListAsync());
        }

        // GET: MedicinesSolds/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicinesSold = await _context.MedicinesSold
                .Include(m => m.SoldByNavigation)
                .Include(m => m.SoldMedicineNavigation)
                .Include(m => m.SoldToNavigation)
                .FirstOrDefaultAsync(m => m.SoldId == id);
            if (medicinesSold == null)
            {
                return NotFound();
            }

            return View(medicinesSold);
        }

        // GET: MedicinesSolds/Create
        public IActionResult Create()
        {
            ViewData["SoldBy"] = new SelectList(_context.Pharmacist, "PharmacistId", "PharmacistName");
            ViewData["SoldMedicine"] = new SelectList(_context.MedicinesStored, "MedicineId", "Category");
            ViewData["SoldTo"] = new SelectList(_context.Users, "UserId", "Pwd");
            return View();
        }

        // POST: MedicinesSolds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoldId,SoldBy,SoldMedicine,SoldDate,SoldTo")] MedicinesSold medicinesSold)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicinesSold);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SoldBy"] = new SelectList(_context.Pharmacist, "PharmacistId", "PharmacistName", medicinesSold.SoldBy);
            ViewData["SoldMedicine"] = new SelectList(_context.MedicinesStored, "MedicineId", "Category", medicinesSold.SoldMedicine);
            ViewData["SoldTo"] = new SelectList(_context.Users, "UserId", "Pwd", medicinesSold.SoldTo);
            return View(medicinesSold);
        }

        // GET: MedicinesSolds/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicinesSold = await _context.MedicinesSold.FindAsync(id);
            if (medicinesSold == null)
            {
                return NotFound();
            }
            ViewData["SoldBy"] = new SelectList(_context.Pharmacist, "PharmacistId", "PharmacistName", medicinesSold.SoldBy);
            ViewData["SoldMedicine"] = new SelectList(_context.MedicinesStored, "MedicineId", "Category", medicinesSold.SoldMedicine);
            ViewData["SoldTo"] = new SelectList(_context.Users, "UserId", "Pwd", medicinesSold.SoldTo);
            return View(medicinesSold);
        }

        // POST: MedicinesSolds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("SoldId,SoldBy,SoldMedicine,SoldDate,SoldTo")] MedicinesSold medicinesSold)
        {
            if (id != medicinesSold.SoldId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicinesSold);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicinesSoldExists(medicinesSold.SoldId))
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
            ViewData["SoldBy"] = new SelectList(_context.Pharmacist, "PharmacistId", "PharmacistName", medicinesSold.SoldBy);
            ViewData["SoldMedicine"] = new SelectList(_context.MedicinesStored, "MedicineId", "Category", medicinesSold.SoldMedicine);
            ViewData["SoldTo"] = new SelectList(_context.Users, "UserId", "Pwd", medicinesSold.SoldTo);
            return View(medicinesSold);
        }

        // GET: MedicinesSolds/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicinesSold = await _context.MedicinesSold
                .Include(m => m.SoldByNavigation)
                .Include(m => m.SoldMedicineNavigation)
                .Include(m => m.SoldToNavigation)
                .FirstOrDefaultAsync(m => m.SoldId == id);
            if (medicinesSold == null)
            {
                return NotFound();
            }

            return View(medicinesSold);
        }

        // POST: MedicinesSolds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var medicinesSold = await _context.MedicinesSold.FindAsync(id);
            _context.MedicinesSold.Remove(medicinesSold);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicinesSoldExists(long id)
        {
            return _context.MedicinesSold.Any(e => e.SoldId == id);
        }
    }
}
