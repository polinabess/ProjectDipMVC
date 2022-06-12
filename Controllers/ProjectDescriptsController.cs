using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectDipMVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace ProjectDipMVC.Controllers
{
    [Authorize(Roles = "Администратор, Главный редактор")]
    public class ProjectDescriptsController : Controller
    {
        private readonly ProjectDipContext _context;

        public ProjectDescriptsController(ProjectDipContext context)
        {
            _context = context;
        }

        // GET: ProjectDescripts
        public async Task<IActionResult> Index(int ProjectId)
        {
            var projectDipContext = _context.ProjectDescripts.Include(p => p.Project).
                Include(p => p.User).Where(p=> p.ProjectId == ProjectId);
            ViewBag.ProjectId = ProjectId;
            return View(await projectDipContext.ToListAsync());
        }

        // GET: ProjectDescripts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProjectDescripts == null)
            {
                return NotFound();
            }

            var projectDescript = await _context.ProjectDescripts
                .Include(p => p.Project)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProjDscrptId == id);
            if (projectDescript == null)
            {
                return NotFound();
            }

            return View(projectDescript);
        }

        // GET: ProjectDescripts/Create
        public async Task<IActionResult> Create(int ProjectId)        {
            //var lstProjects = _context.Projects.Where(p => p.ProjectId == ProjectId).ToListAsync().Result;
            //ViewData["ProjectId"] = new SelectList(lstProjects, "ProjectId", "Name");
            ViewBag.ProjectId = ProjectId;
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Login");
            return View();
        }

        // POST: ProjectDescripts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("ProjDscrptId,SectionName,SectionNumber,UserId,ProjectId")]*/ 
        ProjectDescript projectDescript)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(projectDescript);
                await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { ProjectId = projectDescript.ProjectId });
            //}
            //ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", projectDescript.ProjectId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Login", projectDescript.UserId);
            //return View(projectDescript);
        }

        // GET: ProjectDescripts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProjectDescripts == null)
            {
                return NotFound();
            }

            var projectDescript = await _context.ProjectDescripts.FindAsync(id);
            if (projectDescript == null)
            {
                return NotFound();
            }
            //var lstProjects = _context.Projects.Where(p => p.ProjectId == projectDescript.ProjectId).
            //    ToListAsync().Result;
            //ViewData["ProjectId"] = new SelectList(lstProjects, "ProjectId", "Name", projectDescript.ProjectId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Login", projectDescript.UserId);
            return View(projectDescript);
        }

        // POST: ProjectDescripts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjDscrptId,SectionName,SectionNumber,UserId,ProjectId")] ProjectDescript projectDescript)
        {
            if (id != projectDescript.ProjDscrptId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
            var ProjectId = projectDescript.ProjectId;
                try
                {
                    _context.Update(projectDescript);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectDescriptExists(projectDescript.ProjDscrptId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { ProjectId = ProjectId });
            //}
            //ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "ProjectId", projectDescript.ProjectId);
            //ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", projectDescript.UserId);
            //return View(projectDescript);
        }

        // GET: ProjectDescripts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProjectDescripts == null)
            {
                return NotFound();
            }

            var projectDescript = await _context.ProjectDescripts
                .Include(p => p.Project)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProjDscrptId == id);
            if (projectDescript == null)
            {
                return NotFound();
            }

            return View(projectDescript);
        }

        // POST: ProjectDescripts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProjectDescripts == null)
            {
                return Problem("Entity set 'ProjectDipContext.ProjectDescripts'  is null.");
            }
            var projectDescript = await _context.ProjectDescripts.FindAsync(id);
            var ProjectId = projectDescript.ProjectId;
            if (projectDescript != null)
            {
                _context.ProjectDescripts.Remove(projectDescript);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { ProjectId = ProjectId });
        }

        private bool ProjectDescriptExists(int id)
        {
          return (_context.ProjectDescripts?.Any(e => e.ProjDscrptId == id)).GetValueOrDefault();
        }
    }
}
