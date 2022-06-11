using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectDipMVC.Models;

namespace ProjectDipMVC.Controllers
{
    public class SectionsProjectsController : Controller
    {
        private readonly ProjectDipContext _context;

        public SectionsProjectsController(ProjectDipContext context)
        {
            _context = context;
        }

        // GET: SectionsProjects
        public async Task<IActionResult> Index()
        {
            var UserId = 1;
            var projectDipContext =
            from pd in _context.ProjectDescripts.Include(s => s.Project)
            join p in _context.SectionsProjects on 
                pd.ProjDscrptId equals p.ProjDscrptId into ps
            from p in ps.DefaultIfEmpty()
            where pd.UserId == UserId
            select new SectionsProjectIndex { 
                ProjDscrptId = pd.ProjDscrptId,
                Name = pd.Project.Name,
                Section_Name = pd.SectionName,
                Section_Number = pd.SectionNumber,
                SectionsId = null != p ? p.SectionsId: null,
                NameSections = null != p ? p.NameSections : null,
                NumberSections = null != p ? p.NumberSections : null,
                NameFileSections = null != p ? p.NameFileSections : null
            };

            //var projectDipContext = _context.SectionsProjects.Include(s => s.ProjDscrpt);//.Where(p => p.);
            return View(await projectDipContext.ToListAsync());
        }

        // GET: SectionsProjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SectionsProjects == null)
            {
                return NotFound();
            }

            var sectionsProject = await _context.SectionsProjects
                .Include(s => s.ProjDscrpt)
                .FirstOrDefaultAsync(m => m.SectionsId == id);
            if (sectionsProject == null)
            {
                return NotFound();
            }

            return View(sectionsProject);
        }

        // GET: SectionsProjects/Create
        public IActionResult Create(int ProjDscrptId)
        {
            ViewBag.ProjDscrptId = ProjDscrptId;
            ViewData["ProjDscrptId"] = new SelectList(_context.ProjectDescripts, "ProjDscrptId", "ProjDscrptId");
            return View();
        }

        // POST: SectionsProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SectionsId,NameSections,NumberSections,ProjDscrptId,NameFileSections,FileSections")] SectionsProject sectionsProject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sectionsProject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjDscrptId"] = new SelectList(_context.ProjectDescripts, "ProjDscrptId", "ProjDscrptId", sectionsProject.ProjDscrptId);
            return View(sectionsProject);
        }

        // GET: SectionsProjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SectionsProjects == null)
            {
                return NotFound();
            }

            var sectionsProject = await _context.SectionsProjects.FindAsync(id);
            if (sectionsProject == null)
            {
                return NotFound();
            }
            ViewData["ProjDscrptId"] = new SelectList(_context.ProjectDescripts, "ProjDscrptId", "ProjDscrptId", sectionsProject.ProjDscrptId);
            return View(sectionsProject);
        }

        // POST: SectionsProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SectionsId,NameSections,NumberSections,ProjDscrptId,NameFileSections,FileSections")] SectionsProject sectionsProject)
        {
            if (id != sectionsProject.SectionsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sectionsProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionsProjectExists(sectionsProject.SectionsId))
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
            ViewData["ProjDscrptId"] = new SelectList(_context.ProjectDescripts, "ProjDscrptId", "ProjDscrptId", sectionsProject.ProjDscrptId);
            return View(sectionsProject);
        }

        // GET: SectionsProjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SectionsProjects == null)
            {
                return NotFound();
            }

            var sectionsProject = await _context.SectionsProjects
                .Include(s => s.ProjDscrpt)
                .FirstOrDefaultAsync(m => m.SectionsId == id);
            if (sectionsProject == null)
            {
                return NotFound();
            }

            return View(sectionsProject);
        }

        // POST: SectionsProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SectionsProjects == null)
            {
                return Problem("Entity set 'ProjectDipContext.SectionsProjects'  is null.");
            }
            var sectionsProject = await _context.SectionsProjects.FindAsync(id);
            if (sectionsProject != null)
            {
                _context.SectionsProjects.Remove(sectionsProject);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SectionsProjectExists(int id)
        {
          return (_context.SectionsProjects?.Any(e => e.SectionsId == id)).GetValueOrDefault();
        }
    }
}
