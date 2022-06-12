using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectDipMVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace ProjectDipMVC.Controllers
{

    [Authorize(Roles = "Администратор, Гланый редактор")]
    public class ProjectsController : Controller
    {
        private readonly ProjectDipContext _context;

        public ProjectsController(ProjectDipContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var projectDipContext = _context.Projects.Include(p => p.User);
            try
            {
                var t = projectDipContext.ToList();
            }catch(Exception ex)
            {
                var t = ex;
            }
                return View(await projectDipContext.ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Login");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectUpload projectUpload)
        {
            //if (ModelState.IsValid)Create
            //{
            var project = createProject(projectUpload);
            _context.Add(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Login", projectUpload.UserId);
            //return View(projectUpload);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            var projectUpload = new ProjectUpload
            {
                ProjectId = project.ProjectId,
                Name = project.Name,
                UserId = project.UserId
            };
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Login", projectUpload.UserId);
            
            return View(projectUpload);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectUpload projectUpload)
        {
            if (id != projectUpload.ProjectId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    var project = createProject(projectUpload, id);
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(projectUpload.ProjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Login", projectUpload.UserId);
            //return View(projectUpload);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projects == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Projects == null)
            {
                return Problem("Entity set 'ProjectDipContext.Projects'  is null.");
            }
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
          return (_context.Projects?.Any(e => e.ProjectId == id)).GetValueOrDefault();
        }

        private static Project createProject(ProjectUpload projectUpload, int? ProjectId = null)
        {
            var fileName = Path.GetFileName(projectUpload.TitulFile.FileName);
            var project = new Project
            {
                Name = projectUpload.Name,
                DateCreate = DateTime.Now,
                UserId = projectUpload.UserId,
                TitulName = fileName
            };
            project.ProjectId = ProjectId != null ? ProjectId.Value : project.ProjectId;
            using (var target = new MemoryStream())
            {
                projectUpload.TitulFile.CopyTo(target);
                project.TitulFile = target.ToArray();
            }

            return project;
        }
    }
}
