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

namespace BerrasBio.Pages.Movies
{
    public class MovieIndexModel : PageModel
    {
        private readonly BerrasBio.Data.BerrasBioContext _context;

        public MovieIndexModel(BerrasBio.Data.BerrasBioContext context)
        {
            _context = context;
        }

        public Models.Movie Movie { get; set; }
        
        public IList<Showing> AllShows { get; set; }
        public Showing Showings { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movie.FirstOrDefaultAsync(m => m.MovieId == id);
            Showings= await _context.Showings.FirstOrDefaultAsync(m => m.MovieId == id);

            var showings = (from s in _context.Showings
                            where s.MovieId == id
                            select s).ToListAsync();

            AllShows = await showings;

            if (Movie == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
