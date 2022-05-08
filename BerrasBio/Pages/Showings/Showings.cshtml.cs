#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BerrasBio.Data;
using BerrasBio.Models;

namespace BerrasBio.Pages.Showings
{
    public class ShowingsModel : PageModel
    {
        private readonly BerrasBio.Data.BerrasBioContext _context;

        public ShowingsModel(BerrasBio.Data.BerrasBioContext context)
        {
            _context = context;
        }

        public IList<Showing> Showing { get;set; }

        public async Task OnGetAsync()
        {
            Showing = await _context.Showings
                .Include(s => s.Movie)
                .Include(s => s.Salon).ToListAsync();
        }
    }
}
