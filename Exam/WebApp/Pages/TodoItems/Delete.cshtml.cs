using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.App.Ef;
using Domain;

namespace WebApp.Pages.TodoItems
{
    public class DeleteModel : PageModel
    {
        private readonly DAL.App.Ef.ApplicationDbContext _context;

        public DeleteModel(DAL.App.Ef.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty] public TodoItem TodoItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TodoItem = await _context.TodoItems
                .Include(t => t.Category).FirstOrDefaultAsync(m => m.TodoItemId == id);

            if (TodoItem == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TodoItem = await _context.TodoItems.FindAsync(id);

            if (TodoItem != null)
            {
                _context.TodoItems.Remove(TodoItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
