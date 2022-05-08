#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BerrasBio.Data;
using BerrasBio.Models;
using System.ComponentModel.DataAnnotations;


namespace BerrasBio.Pages.Booking
{
    public class BookModel : PageModel
    {
        private readonly BerrasBio.Data.BerrasBioContext _context;

        public BookModel(BerrasBio.Data.BerrasBioContext context)
        {
            _context = context;
        }

        public const string MessageKey = nameof(MessageKey);


        [BindProperty]
        public Showing Showing { get; set; }
       
        public Models.Movie Movie { get; set; }

        public string MovieTitle { get; set; }

        [BindProperty]
        public int WantedTickets { get; set; }
        public int MaxSeats { get; set; }
        public int AvailableSeats { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Showing = await _context.Showings
                .Include(s => s.Movie)
                .Include(s => s.Salon).FirstOrDefaultAsync(m => m.ShowingId == id);

            Movie = await (from m in _context.Movie
                           join s in _context.Showings on m.MovieId equals s.MovieId
                           select m).FirstOrDefaultAsync();

            MovieTitle = (from m in _context.Movie
                                join s in _context.Showings on m.MovieId equals s.MovieId
                                 where s.ShowingId == id
                                select m.MovieTitle).SingleOrDefault();


            MaxSeats = await (from sh in _context.Showings
                              join s in _context.Salons on sh.SalonId equals s.SalonId
                              select s.SeatsAvailable).FirstOrDefaultAsync();

            AvailableSeats = (int)(MaxSeats - Showing.BookedSeats);

            if (Showing == null)
            {
                return NotFound();
            }
           ViewData["MovieId"] = new SelectList(_context.Movie, "MovieId", "MovieTitle");
           ViewData["SalonId"] = new SelectList(_context.Salons, "SalonId", "SalonId");
            return Page();
        }

      

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int id)
        {
         
            if(id == null)
            {
                return NotFound();
            }

            Showing.ShowingId = id;

            Showing.MovieId = ( from s in _context.Showings
                                where s.ShowingId == id
                                select s.MovieId).FirstOrDefault();

            Showing.SalonId = (from s in _context.Showings
                               where s.ShowingId == id
                               select s.SalonId).FirstOrDefault();

            Showing.ShowingTime= (from s in _context.Showings
                                  where s.ShowingId == id
                                  select s.ShowingTime).FirstOrDefault();

           var check = Showing.BookedSeats + WantedTickets;

            Showing.PriceRate = (from s in _context.Showings
                                 where s.ShowingId == id
                                 select s.PriceRate).FirstOrDefault();

            MovieTitle = (from m in _context.Movie
                          join s in _context.Showings on m.MovieId equals s.MovieId
                          select m.MovieTitle).SingleOrDefault();

            if (check > 50)
            {
                TempData[MessageKey] = $"There are not enough available seats  for amount of tickets selected!";
                return RedirectToAction(Request.Path);
            }
            else
            {
                Random rdn = new Random();
                var refNr = rdn.Next(1, 999999);

                Showing.BookedSeats = Showing.BookedSeats + WantedTickets;
                _context.Update(Showing);
                await _context.SaveChangesAsync();
                if(WantedTickets == 1)
                {
                    TempData[MessageKey] = $"{MovieTitle.ToString()}: {WantedTickets} ticket is pre-booked! Your reference number is: #{refNr}. See you soon!";
                }
                else
                {
                    TempData[MessageKey] = $"{MovieTitle.ToString()}: {WantedTickets} tickets are pre-booked! Your reference number is: #{refNr}. See you soon!";
                }
                
                return RedirectToPage("/Home");
            }

        }

      
    }
}
