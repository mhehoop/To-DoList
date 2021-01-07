using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DAL.App.Ef;


namespace WebApp.Pages
{
    public class Statistics : PageModel
    {
        private readonly ApplicationDbContext _context;

        public Statistics(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty] public int Done { get; set; } = default!;

        [BindProperty] public int Not { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Done = 0;
            Not = 0;
            var todoItems = await _context.TodoItems.ToListAsync();
            foreach (var item in todoItems)
            {
                if (item.DateDone == null)
                {
                    Not = Not + 1;
                }
                else
                {
                    Done = Done + 1;
                }
            }
        }
    }
}