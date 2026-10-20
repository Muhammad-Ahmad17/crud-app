using ClinicMvc.Data;
using ClinicMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicMvc.Controllers;

/// <summary>
/// CONTROLLER — handles HTTP, talks to Model (DB), picks a View.
///
/// URL pattern (see Program.cs): /{controller}/{action}/{id?}
///   GET  /Patients          → Index
///   GET  /Patients/Details/1 → Details
///   GET  /Patients/Create   → Create (form)
///   POST /Patients/Create   → Create (save)
///   GET  /Patients/Edit/1   → Edit (form)
///   POST /Patients/Edit/1   → Edit (save)
///   GET  /Patients/Delete/1 → Delete (confirm)
///   POST /Patients/Delete/1 → DeleteConfirmed
/// </summary>
public class PatientsController : Controller
{
    private readonly AppDbContext _db;

    // DI injects AppDbContext (registered in Program.cs)
    public PatientsController(AppDbContext db) => _db = db;

    // READ — list all (C[R]UD)
    public async Task<IActionResult> Index()
    {
        var patients = await _db.Patients
            .OrderBy(p => p.FullName)
            .ToListAsync();

        // Pass Model → View (Views/Patients/Index.cshtml)
        return View(patients);
    }

    // READ — one record
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();

        var patient = await _db.Patients.FirstOrDefaultAsync(p => p.Id == id);
        if (patient is null) return NotFound();

        return View(patient);
    }

    // CREATE — show empty form (GET)
    public IActionResult Create() => View();

    // CREATE — save form (POST)
    [HttpPost]
    [ValidateAntiForgeryToken] // stops CSRF; token comes from the form
    public async Task<IActionResult> Create(Patient patient)
    {
        // ModelState = validation from [Required], [EmailAddress], etc. on the Model
        if (!ModelState.IsValid)
            return View(patient); // re-show form with errors

        _db.Patients.Add(patient);
        await _db.SaveChangesAsync();

        TempData["Message"] = $"Created patient '{patient.FullName}'.";
        return RedirectToAction(nameof(Index)); // Post-Redirect-Get pattern
    }

    // UPDATE — show form with existing data (GET)
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();

        var patient = await _db.Patients.FindAsync(id);
        if (patient is null) return NotFound();

        return View(patient);
    }

    // UPDATE — save changes (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Patient patient)
    {
        if (id != patient.Id) return NotFound();

        if (!ModelState.IsValid)
            return View(patient);

        _db.Update(patient);
        await _db.SaveChangesAsync();

        TempData["Message"] = $"Updated patient '{patient.FullName}'.";
        return RedirectToAction(nameof(Index));
    }

    // DELETE — confirm page (GET)
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var patient = await _db.Patients.FirstOrDefaultAsync(p => p.Id == id);
        if (patient is null) return NotFound();

        return View(patient);
    }

    // DELETE — actually remove (POST)
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var patient = await _db.Patients.FindAsync(id);
        if (patient is not null)
        {
            _db.Patients.Remove(patient);
            await _db.SaveChangesAsync();
            TempData["Message"] = $"Deleted patient '{patient.FullName}'.";
        }

        return RedirectToAction(nameof(Index));
    }
}
