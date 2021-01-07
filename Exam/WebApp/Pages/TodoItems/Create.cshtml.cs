using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain;

namespace WebApp.Pages.TodoItems
{
    public class CreateModel : PageModel
    {
        private readonly DAL.App.Ef.ApplicationDbContext _context;

        public CreateModel(DAL.App.Ef.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return Page();
        }

        [BindProperty] public TodoItem TodoItem { get; set; } = default!;
        
        [BindProperty, DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DateDue { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            TodoItem.DateCreated = DateTime.Now;
            TodoItem.DateDone = null;
            TodoItem.DateDue = DateDue;
            
            //see validation ei tööta
            // if (!ModelState.IsValid)
            // {
            //     return Page();
            // }

            _context.TodoItems.Add(TodoItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
