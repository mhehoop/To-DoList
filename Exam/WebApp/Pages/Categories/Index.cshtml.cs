using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.App.Ef;
using Domain;

namespace WebApp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly DAL.App.Ef.ApplicationDbContext _context;

        public IndexModel(DAL.App.Ef.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await _context.Categories.ToListAsync();
        }
    }
}
