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
    public class IndexModel : PageModel
    {
        private readonly DAL.App.Ef.ApplicationDbContext _context;

        public IndexModel(DAL.App.Ef.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty] public IList<TodoItem>? TodoItem { get; set; }

        [BindProperty] public string? Order { get; set; }


        public async Task OnGetAsync()
        {
            TodoItem = await _context.TodoItems
                .Include(t => t.Category).ToListAsync();
        }

        [BindProperty] public List<TodoItem>? OrderedTodoItem { get; set; }
        public async Task OnPostAsync()
        {
            if (Order == "creation")
            {
                TodoItem = await _context.TodoItems
                    .Include(t => t.Category).ToListAsync();
                TodoItem = TodoItem.OrderBy(item => item.DateCreated).ToList();
            }
            else if (Order == $"priority")
            {
                TodoItem = await _context.TodoItems
                    .Include(t => t.Category).ToListAsync();
                TodoItem = TodoItem.OrderBy(item => item.EPriority).ToList();
            }
            else //kuii on due datei järgi vaja orderida
            {
                TodoItem = await _context.TodoItems
                    .Include(t => t.Category).ToListAsync();
                TodoItem = TodoItem.OrderBy(item => item.DateDue).ToList();
            }
        }
    }
}