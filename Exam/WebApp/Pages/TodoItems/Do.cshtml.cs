using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages.TodoItems
{
    public class Do : PageModel
    {
        private readonly DAL.App.Ef.ApplicationDbContext _context;

        public Do(DAL.App.Ef.ApplicationDbContext context)
        {
            _context = context;
        }

        public TodoItem TodoItem { get; set; } = default!;

        public async Task<RedirectToPageResult> OnGetAsync(int id)
        {
            TodoItem = await _context.TodoItems.FirstOrDefaultAsync(t => t.TodoItemId == id);
            if (TodoItem.DateDone != null)
            {
                TodoItem.DateDone = null;
            }
            else
            {
                TodoItem.DateDone = DateTime.Now;
            }

            _context.Attach(TodoItem).State = EntityState.Modified;


            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool TodoItemExists(int id)
        {
            return _context.TodoItems.Any(e => e.TodoItemId == id);
        }
    }
}