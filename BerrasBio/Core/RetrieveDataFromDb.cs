using BerrasBio.Data;
using BerrasBio.Models;

namespace BerrasBio.Core;

public class RetrieveDataFromDb
{
    public string MovieDescription(string title)
    {
        using (var context = new BerrasBioContext())
        {
            var movieDescription = (from m in context.Movie
                                    where m.MovieTitle == title
                                    select m.Description).SingleOrDefault().ToString();

            return movieDescription;
        }
    }

    public IList<Showing> AllFilmShowings(string filmName)
    {
        using (var context = new BerrasBioContext())
        {
            var allFilmShowings = (from s in context.Showings
                                    join m in context.Movie on s.MovieId equals m.MovieId
                                    where m.MovieTitle == filmName
                                    select s).ToList();

            return allFilmShowings;
        }

    }

    public IEnumerable<Movie> GetAllMovies()
    {
        using (var context = new BerrasBioContext())
        {
            var allMovies = (from m in context.Movie
                                   select m).ToList();

            return allMovies;
        }

    }
    
    public IEnumerable<Showing> GetAllShowing()
    {
        using (var context = new BerrasBioContext())
        {
            var allShowings = (from s in context.Showings
                                   select s).ToList();

            return allShowings;
        }

    }
    public List<DateTime> GetAllShowingTime(int? id)
    {
        using (var context = new BerrasBioContext())
        {
            var allShowingsTimes = (from s in context.Showings
                                    where s.MovieId==id
                                   select s.ShowingTime).ToList();

            return allShowingsTimes;
        }

    }
    
    public int? GetBookedSeats(string title, DateTime showingTime)
    {
        using (var context = new BerrasBioContext())
        {
            var getBookedSeats = (from s in context.Showings
                                  join m in context.Movie on s.MovieId equals m.MovieId
                                    where m.MovieTitle==title && s.ShowingTime==showingTime
                                   select s.BookedSeats).FirstOrDefault();

            return getBookedSeats;
        }

    }
    public int GetSalonSeats(int? id)
    {
        using (var context = new BerrasBioContext())
        {
            var getSalonsSeats = (from s in context.Showings
                                  join i in context.Salons on s.SalonId equals i.SalonId
                                    where s.SalonId==id
                                   select i.SeatsAvailable).FirstOrDefault();

            return getSalonsSeats;
        }

    }

    public int GetShowingId(int id, DateTime showingTime)
    {
        using (var context = new BerrasBioContext())
        {
            var showingID = (from s in context.Showings
                                  join m in context.Movie on s.MovieId equals m.MovieId
                                  where s.MovieId == id && s.ShowingTime == showingTime
                                  select s.ShowingId).FirstOrDefault();

            return showingID;
        }

    }
    

    
    
}
