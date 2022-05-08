using BerrasBio.Data;
using BerrasBio.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BerrasBio.Pages
{
    public class HomeModel : PageModel
    {
        private readonly BerrasBioContext Context;
        public HomeModel(BerrasBioContext context)
        {
            Context = context;
        }

        public IList<Models.Movie> Movie { get; set; }
        public Movie Movies { get; set; }

        
        public string movieOfWeekTitle { get; set; }
        public string movieDescription { get; set; }
        public string PhotoPath { get; set; }

        public string GetPhotoPath()
        {
            return PhotoPath;
        }

        public async Task OnGetAsync(string photoPath)
        {
            var movies = from m in Context.Movie
                         select m;

            if (!string.IsNullOrEmpty(movieOfWeekTitle))
            {
                movies = movies.Where(s => s.MovieTitle.Contains(movieOfWeekTitle));
                
            }

            

            Movie = await movies.ToListAsync();
            
        }
    }
}
