using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.App.Ef;
using Domain;


namespace WebApp.Pages
{
    public class Search : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Search(ApplicationDbContext context)
        {
            _context = context;
        }


        [BindProperty] public IList<TodoItem>? TodoItems { get; set; }
        [BindProperty] public List<Category>? Categories { get; set; }
        
        [BindProperty] public string? SearchHeadline { get; set; } 
        [BindProperty] public string? SearchDescription { get; set; } 
        [BindProperty] public EPriority? SearchPriority { get; set; } 
        [BindProperty] public int? SearchCategoryId { get; set; } 

        public async Task OnGetAsync()
        {
            Categories = await _context.Categories.ToListAsync();
        }

        public async Task<PageResult> OnPostAsync()
        {
            Console.WriteLine(SearchHeadline);
            Console.WriteLine(SearchDescription);
            Console.WriteLine(SearchPriority);
            Console.WriteLine(SearchCategoryId);
            
            
            Categories = await _context.Categories.ToListAsync();
            var query = _context.TodoItems.AsQueryable();
            
            SearchHeadline = SearchHeadline?.Trim();
            if (!string.IsNullOrWhiteSpace(SearchHeadline))
            {
                query = query.Where(m => m.Heading.Contains(SearchHeadline));
            }
            
            SearchDescription = SearchDescription?.Trim();
            if (!string.IsNullOrWhiteSpace(SearchDescription))
            {
                query = query.Where(m => m.Description.Contains(SearchDescription));
            }

            if (SearchPriority != null)
            {
                query = query.Where(m => m.EPriority == SearchPriority);
            }
            
            if (SearchCategoryId != -1)
            {
                query = query.Where(m => m.CategoryId == SearchCategoryId);
            }

            TodoItems = await query.ToListAsync();

            return Page();


        }
    }
}