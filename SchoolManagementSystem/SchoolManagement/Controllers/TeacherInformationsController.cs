using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using SchoolManagement.Data;
using SchoolManagement.Models;

namespace SchoolManagement.Controllers
{
    public class TeacherInformationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileProvider fileProvider;
        private readonly IHostingEnvironment hostingEnvironment;

        public TeacherInformationsController(ApplicationDbContext context, IFileProvider fileprovider, IHostingEnvironment env)
        {
            _context = context;
            fileProvider = fileprovider;
            hostingEnvironment = env;
        }

        // GET: TeacherInformations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TeacherInformation_1.Include(t => t.Class);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TeacherInformations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherInformation = await _context.TeacherInformation_1
                .Include(t => t.Class)
                .FirstOrDefaultAsync(m => m.TeacherID == id);
            if (teacherInformation == null)
            {
                return NotFound();
            }

            return View(teacherInformation);
        }

        // GET: TeacherInformations/Create
        public IActionResult Create()
        {
            ViewData["ClassID"] = new SelectList(_context.ClassInformation, "ClassID", "ClassName");
            return View();
        }

        // POST: TeacherInformations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherID,TeacherName,DOB,TeachersCellPhone,Religion,TeacherAddress,ImagePath,ClassID")] TeacherInformation teacherInformation, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherInformation);
                await _context.SaveChangesAsync();

                // Code to upload image if not null
                if (file != null || file.Length != 0)
                {
                    // Create a File Info 
                    FileInfo fi = new FileInfo(file.FileName);

                    // This code creates a unique file name to prevent duplications 
                    // stored at the file location
                    var newFilename = teacherInformation.TeacherID + "_" + String.Format("{0:d}",
                                      (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                    var webPath = hostingEnvironment.WebRootPath;
                    var path = Path.Combine("", webPath + @"\ImageFiles\" + newFilename);

                    // IMPORTANT: The pathToSave variable will be save on the column in the database
                    var pathToSave = @"/ImageFiles/" + newFilename;

                    // This stream the physical file to the allocate wwwroot/ImageFiles folder
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // This save the path to the record
                    teacherInformation.ImagePath = pathToSave;
                    _context.Update(teacherInformation);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassID"] = new SelectList(_context.ClassInformation, "ClassID", "ClassName", teacherInformation.ClassID);
            return View(teacherInformation);
        }

        // GET: TeacherInformations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherInformation = await _context.TeacherInformation_1.FindAsync(id);
            if (teacherInformation == null)
            {
                return NotFound();
            }
            ViewData["ClassID"] = new SelectList(_context.ClassInformation, "ClassID", "ClassName", teacherInformation.ClassID);
            return View(teacherInformation);
        }

        // POST: TeacherInformations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherID,TeacherName,DOB,TeachersCellPhone,Religion,TeacherAddress,ImagePath,ClassID")] TeacherInformation teacherInformation)
        {
            if (id != teacherInformation.TeacherID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherInformationExists(teacherInformation.TeacherID))
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
            ViewData["ClassID"] = new SelectList(_context.ClassInformation, "ClassID", "ClassName", teacherInformation.ClassID);
            return View(teacherInformation);
        }

        // GET: TeacherInformations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherInformation = await _context.TeacherInformation_1
                .Include(t => t.Class)
                .FirstOrDefaultAsync(m => m.TeacherID == id);
            if (teacherInformation == null)
            {
                return NotFound();
            }

            return View(teacherInformation);
        }

        // POST: TeacherInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacherInformation = await _context.TeacherInformation_1.FindAsync(id);
            _context.TeacherInformation_1.Remove(teacherInformation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherInformationExists(int id)
        {
            return _context.TeacherInformation_1.Any(e => e.TeacherID == id);
        }
    }
}
