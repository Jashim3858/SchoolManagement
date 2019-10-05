using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Controllers
{
    public class ClassInformationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClassInformationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClassInformations
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClassInformation.ToListAsync());
        }

        // GET: ClassInformations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classInformation = await _context.ClassInformation
                .FirstOrDefaultAsync(m => m.ClassID == id);
            if (classInformation == null)
            {
                return NotFound();
            }

            return View(classInformation);
        }

        // GET: ClassInformations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClassInformations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassID,ClassName,NoOfStudent")] ClassInformation classInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classInformation);
        }

        // GET: ClassInformations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classInformation = await _context.ClassInformation.FindAsync(id);
            if (classInformation == null)
            {
                return NotFound();
            }
            return View(classInformation);
        }

        // POST: ClassInformations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassID,ClassName,NoOfStudent")] ClassInformation classInformation)
        {
            if (id != classInformation.ClassID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassInformationExists(classInformation.ClassID))
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
            return View(classInformation);
        }

        // GET: ClassInformations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classInformation = await _context.ClassInformation
                .FirstOrDefaultAsync(m => m.ClassID == id);
            if (classInformation == null)
            {
                return NotFound();
            }

            return View(classInformation);
        }

        // POST: ClassInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classInformation = await _context.ClassInformation.FindAsync(id);
            _context.ClassInformation.Remove(classInformation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassInformationExists(int id)
        {
            return _context.ClassInformation.Any(e => e.ClassID == id);
        }
    }
}
