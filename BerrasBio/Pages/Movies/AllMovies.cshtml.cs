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
using BerrasBio.Core;

namespace BerrasBio.Pages.Movies
{
    public class AllMoviesModel : PageModel
    {
        private readonly BerrasBio.Data.BerrasBioContext _context;

        public AllMoviesModel(BerrasBio.Data.BerrasBioContext context)
        {
            _context = context;
        }

        public Models.Movie Movie { get; set; }

        public IEnumerable<Models.Movie> Movies { get; set; }
      
        public void OnGet()
        {
            RetrieveDataFromDb db = new();
            Movies = db.GetAllMovies();
        }

    }
}
