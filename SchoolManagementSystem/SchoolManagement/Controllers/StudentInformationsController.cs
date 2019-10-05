using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using SchoolManagement.Data;
using SchoolManagement.Models;
using SchoolManagement.ViewModels;

namespace SchoolManagement.Controllers
{
    public class StudentInformationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileProvider fileProvider;
        private readonly IHostingEnvironment hostingEnvironment;

        public StudentInformationsController(ApplicationDbContext context, IMapper mapper,IFileProvider fileprovider,IHostingEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            fileProvider = fileprovider;
            hostingEnvironment = env;

        }

        //GET: StudentInformations
        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder; ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null) { pageNumber = 1; } else { searchString = currentFilter; }

            ViewData["CurrentFilter"] = searchString;

            var students = from s in _context.StudentInformation select s; if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.StudentName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.StudentName);
                    break;

                default:
                    students = students.OrderBy(s => s.StudentName);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<StudentInformation>.CreateAsync(students.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["ClassID"] = new SelectList(_context.ClassInformation, "ClassID", "ClassName");
            return View();
        }
        // Ok
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,StudentName,FathersName,MothersName,DOB,Religion,GurdiansCellPhone,StudentAddress,ImagePath,ClassID")]StudentInformationVM studentInformationVM, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentInformationVM);
                await _context.SaveChangesAsync();

                // Code to upload image if not null
                if (file != null || file.Length != 0)
                {
                    // Create a File Info 
                    FileInfo fi = new FileInfo(file.FileName);

                    // This code creates a unique file name to prevent duplications 
                    // stored at the file location
                    var newFilename = studentInformationVM.StudentID + "_" + String.Format("{0:d}",
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
                    studentInformationVM.ImagePath = pathToSave;
                    _context.Update(studentInformationVM);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassID"] = new SelectList(_context.ClassInformation, "ClassID", "ClassName", studentInformationVM.ClassID);
            return View(studentInformationVM);
        }

        // Ok
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentInformationVM = new StudentInformationVM();
            {
                StudentInformation studentInformation = await _context.StudentInformation.SingleOrDefaultAsync(c =>
                c.StudentID == id);
                if (studentInformation == null)
                {
                    return NotFound();
                }
                studentInformationVM.StudentID = studentInformation.StudentID;
                studentInformationVM.StudentName = studentInformation.StudentName;
                studentInformationVM.FathersName = studentInformation.FathersName;
                studentInformationVM.MothersName = studentInformation.MothersName;
                studentInformationVM.DOB = studentInformation.DOB;
                studentInformationVM.Religion = studentInformation.Religion;
                studentInformationVM.GurdiansCellPhone = studentInformation.GurdiansCellPhone;
                studentInformationVM.StudentAddress = studentInformation.StudentAddress;
                studentInformationVM.ImagePath = studentInformation.ImagePath;
                studentInformationVM.ClassID = studentInformation.ClassID;


            }
            ViewData["ClassID"] = new SelectList(_context.ClassInformation, "ClassID", "ClassName", studentInformationVM.ClassID);
            return View(studentInformationVM);
        }
        // Ok
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentInformationVM studentInformationVM)
        {
            if (ModelState.IsValid)
            {
                var studentInformation = await
                _context.StudentInformation.FindAsync(studentInformationVM.StudentID);
                if (studentInformation == null)
                {
                    return NotFound();
                }
                _mapper.Map<StudentInformationVM, StudentInformation>(studentInformationVM, studentInformation);
                _context.Entry(studentInformation).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(studentInformationVM);
        }
        // Ok
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentInformationVM = new StudentInformationVM();
            {
                StudentInformation studentInformation = await _context.StudentInformation.SingleOrDefaultAsync(c =>
                c.StudentID == id);
                if (studentInformation == null)
                {
                    return NotFound();
                }
                studentInformationVM.StudentID = studentInformation.StudentID;
                studentInformationVM.StudentName = studentInformation.StudentName;
                studentInformationVM.FathersName = studentInformation.FathersName;
                studentInformationVM.MothersName = studentInformation.MothersName;
                studentInformationVM.DOB = studentInformation.DOB;
                studentInformationVM.Religion = studentInformation.Religion;
                studentInformationVM.GurdiansCellPhone = studentInformation.GurdiansCellPhone;
                studentInformationVM.StudentAddress = studentInformation.StudentAddress;
                studentInformationVM.ImagePath = studentInformation.ImagePath;
                studentInformationVM.ClassID = studentInformation.ClassID;
            }
            return View(studentInformationVM);
        }
        // Ok
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, StudentInformationVM
        studentInformationVM)
        {
            if (ModelState.IsValid)
            {
                var studentInformation = await _context.StudentInformation.FindAsync(studentInformationVM.StudentID);
                if (studentInformation == null)
                {
                    return NotFound();
                }
                var trn = _mapper.Map<StudentInformationVM, StudentInformation>(studentInformationVM, studentInformation);
                _context.StudentInformation.Remove(trn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassID"] = new SelectList(_context.ClassInformation, "ClassID", "ClassName", studentInformationVM.ClassID);
            return View(studentInformationVM);
        }
        // Ok
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentInformationVM = new StudentInformationVM();
            {
                StudentInformation studentInformation = await _context.StudentInformation.SingleOrDefaultAsync(c =>
                c.StudentID == id);
                if (studentInformation == null)
                {
                    return NoContent();
                }
                studentInformationVM.StudentID = studentInformation.StudentID;
                studentInformationVM.StudentName = studentInformation.StudentName;
                studentInformationVM.FathersName = studentInformation.FathersName;
                studentInformationVM.MothersName = studentInformation.MothersName;
                studentInformationVM.DOB = studentInformation.DOB;
                studentInformationVM.Religion = studentInformation.Religion;
                studentInformationVM.GurdiansCellPhone = studentInformation.GurdiansCellPhone;
                studentInformationVM.StudentAddress = studentInformation.StudentAddress;
                studentInformationVM.ImagePath = studentInformation.ImagePath;

                studentInformationVM.ClassID = studentInformation.ClassID;
            }
            ViewData["ClassID"] = new SelectList(_context.ClassInformation, "ClassID", "ClassName", studentInformationVM.ClassID);
            return View(studentInformationVM);
        }
    }
}



























