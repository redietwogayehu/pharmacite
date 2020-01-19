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
    public class MedicinesStoredsController : Controller
    {
        private readonly pharmacyVaultContext _context;

        public MedicinesStoredsController(pharmacyVaultContext context)
        {
            _context = context;
        }

        // GET: MedicinesStoreds
        public async Task<IActionResult> Index()
        {
            return View(await _context.MedicinesStored.ToListAsync());
        }

        // GET: MedicinesStoreds/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicinesStored = await _context.MedicinesStored
                .FirstOrDefaultAsync(m => m.MedicineId == id);
            if (medicinesStored == null)
            {
                return NotFound();
            }

            return View(medicinesStored);
        }

        // GET: MedicinesStoreds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MedicinesStoreds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedicineId,MedicineName,Category,UnitPrice,AvailableQuantity,StoredDate")] MedicinesStored medicinesStored)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medicinesStored);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicinesStored);
        }

        // GET: MedicinesStoreds/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicinesStored = await _context.MedicinesStored.FindAsync(id);
            if (medicinesStored == null)
            {
                return NotFound();
            }
            return View(medicinesStored);
        }

        // POST: MedicinesStoreds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MedicineId,MedicineName,Category,UnitPrice,AvailableQuantity,StoredDate")] MedicinesStored medicinesStored)
        {
            if (id != medicinesStored.MedicineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicinesStored);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicinesStoredExists(medicinesStored.MedicineId))
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
            return View(medicinesStored);
        }

        // GET: MedicinesStoreds/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicinesStored = await _context.MedicinesStored
                .FirstOrDefaultAsync(m => m.MedicineId == id);
            if (medicinesStored == null)
            {
                return NotFound();
            }

            return View(medicinesStored);
        }

        // POST: MedicinesStoreds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var medicinesStored = await _context.MedicinesStored.FindAsync(id);
            _context.MedicinesStored.Remove(medicinesStored);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicinesStoredExists(long id)
        {
            return _context.MedicinesStored.Any(e => e.MedicineId == id);
        }
    }
}
