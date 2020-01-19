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
    public class UserCompliantsController : Controller
    {
        private readonly pharmacyVaultContext _context;

        public UserCompliantsController(pharmacyVaultContext context)
        {
            _context = context;
        }

        // GET: UserCompliants
        public async Task<IActionResult> Index()
        {
            var pharmacyVaultContext = _context.UserCompliants.Include(u => u.ComplaintByNavigation).Include(u => u.ComplaintOnNavigation);
            return View(await pharmacyVaultContext.ToListAsync());
        }

        // GET: UserCompliants/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCompliants = await _context.UserCompliants
                .Include(u => u.ComplaintByNavigation)
                .Include(u => u.ComplaintOnNavigation)
                .FirstOrDefaultAsync(m => m.ComplaintId == id);
            if (userCompliants == null)
            {
                return NotFound();
            }

            return View(userCompliants);
        }

        // GET: UserCompliants/Create
        public IActionResult Create()
        {
            ViewData["ComplaintBy"] = new SelectList(_context.Users, "UserId", "Pwd");
            ViewData["ComplaintOn"] = new SelectList(_context.MedicinesStored, "MedicineId", "Category");
            return View();
        }

        // POST: UserCompliants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComplaintId,ComplaintBy,ComplaintOn,ComplaintDate,CompaintText")] UserCompliants userCompliants)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userCompliants);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComplaintBy"] = new SelectList(_context.Users, "UserId", "Pwd", userCompliants.ComplaintBy);
            ViewData["ComplaintOn"] = new SelectList(_context.MedicinesStored, "MedicineId", "Category", userCompliants.ComplaintOn);
            return View(userCompliants);
        }

        // GET: UserCompliants/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCompliants = await _context.UserCompliants.FindAsync(id);
            if (userCompliants == null)
            {
                return NotFound();
            }
            ViewData["ComplaintBy"] = new SelectList(_context.Users, "UserId", "Pwd", userCompliants.ComplaintBy);
            ViewData["ComplaintOn"] = new SelectList(_context.MedicinesStored, "MedicineId", "Category", userCompliants.ComplaintOn);
            return View(userCompliants);
        }

        // POST: UserCompliants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("ComplaintId,ComplaintBy,ComplaintOn,ComplaintDate,CompaintText")] UserCompliants userCompliants)
        {
            if (id != userCompliants.ComplaintId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userCompliants);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserCompliantsExists(userCompliants.ComplaintId))
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
            ViewData["ComplaintBy"] = new SelectList(_context.Users, "UserId", "Pwd", userCompliants.ComplaintBy);
            ViewData["ComplaintOn"] = new SelectList(_context.MedicinesStored, "MedicineId", "Category", userCompliants.ComplaintOn);
            return View(userCompliants);
        }

        // GET: UserCompliants/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userCompliants = await _context.UserCompliants
                .Include(u => u.ComplaintByNavigation)
                .Include(u => u.ComplaintOnNavigation)
                .FirstOrDefaultAsync(m => m.ComplaintId == id);
            if (userCompliants == null)
            {
                return NotFound();
            }

            return View(userCompliants);
        }

        // POST: UserCompliants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var userCompliants = await _context.UserCompliants.FindAsync(id);
            _context.UserCompliants.Remove(userCompliants);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserCompliantsExists(long id)
        {
            return _context.UserCompliants.Any(e => e.ComplaintId == id);
        }
    }
}
