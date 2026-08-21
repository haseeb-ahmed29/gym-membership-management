using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymMembershipManagement.Data;
using GymMembershipManagement.Models;

namespace GymMembershipManagement.Controllers;
public class MembersController(AppDbContext db) : Controller
{
    public async Task<IActionResult> Index(string? search, string? status)
    {
        var query = db.Members.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search)) query = query.Where(x => x.Name.Contains(search));
        if (!string.IsNullOrWhiteSpace(status)) query = query.Where(x => x.Status == status);
        ViewBag.Search = search; ViewBag.Status = status;
        return View(await query.OrderByDescending(x => x.CreatedAt).ToListAsync());
    }
    public IActionResult Create() => View(new Member());
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Member item)
    { if (!ModelState.IsValid) return View(item); db.Members.Add(item); await db.SaveChangesAsync(); TempData["Notice"] = "Record created successfully."; return RedirectToAction(nameof(Index)); }
    public async Task<IActionResult> Edit(int? id) => id is null ? NotFound() : (await db.Members.FindAsync(id) is Member item ? View(item) : NotFound());
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Member item)
    { if (id != item.Id) return NotFound(); if (!ModelState.IsValid) return View(item); db.Update(item); await db.SaveChangesAsync(); TempData["Notice"] = "Record updated successfully."; return RedirectToAction(nameof(Index)); }
    public async Task<IActionResult> Delete(int? id) => id is null ? NotFound() : (await db.Members.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id) is Member item ? View(item) : NotFound());
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) { var item = await db.Members.FindAsync(id); if (item is not null) { db.Members.Remove(item); await db.SaveChangesAsync(); } return RedirectToAction(nameof(Index)); }
}
