using BerrasBio.Models;

namespace BerrasBio.Data;

public class DataSeeding
{
    public static void SeedData(BerrasBioContext context)
    {

        if (context.Showings.Any())
        {
            return;
        }

        var movies = new Movie[]
        {
                new Movie{MovieTitle="The Wizard of Oz", RunTime=101, Description="Young Dorothy Gale and her dog are swept away by a tornado from their Kansas farm to the magical Land of Oz, and embark on a quest with three new friends to see the Wizard, who can return her to her home and fulfill the others' wishes.",PhotoPath="Wizard-of-oz-poster.jpg"},
                new Movie{MovieTitle="Gone With The Wind ", RunTime=221, Description="The manipulative daughter of a Georgia plantation owner conducts a turbulent romance with a roguish profiteer during the American Civil War and Reconstruction periods.",PhotoPath="Gone-with-the-wind-poster.jpg"},
                new Movie{MovieTitle="Casablanca", RunTime=102,Description="A cynical expatriate American cafe owner struggles to decide whether or not to help his former lover and her fugitive husband escape the Nazis in French Morocco.",PhotoPath="Casablanka-poster.jpg"},
                new Movie{MovieTitle="Breakfast At Tiffany's", RunTime=114, Description="A young New York socialite becomes interested in a young man who has moved into her apartment building, but her past threatens to get in the way.", PhotoPath="Breakfast-at-tiffanies-poster.jpeg"},
                new Movie{MovieTitle="Psycho", RunTime=109,Description="A Phoenix secretary embezzles $40,000 from her employer's client, goes on the run, and checks into a remote motel run by a young man under the domination of his mother." ,PhotoPath="Pyscho-poster.jpg"}

        };

        context.Movie.AddRange(movies);
        context.SaveChanges();

        var salons = new Salon[]
        {
                new Salon{SeatsAvailable=50},


        };

        context.Salons.AddRange(salons);
        context.SaveChanges();


        for (int i = 0; i <= 6; i++)
        {
            var dateNow = DateTime.Now.AddDays(i);
            Random random = new Random();
            int maxValue = 50;

            var showings = new Showing[]
             {
                new Showing { MovieId = 1, SalonId = 1, ShowingTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 10, 0, 00), BookedSeats = random.Next(0, maxValue),PriceRate =100.00},
                new Showing { MovieId = 2, SalonId = 1, ShowingTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 13, 0, 00), BookedSeats = random.Next(0, maxValue) ,PriceRate =100.00},
                new Showing { MovieId = 3, SalonId = 1, ShowingTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 16, 30, 00), BookedSeats = random.Next(0, maxValue) ,PriceRate =170.00},
                new Showing { MovieId = 4, SalonId = 1, ShowingTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 19, 0, 00), BookedSeats = random.Next(0, maxValue),PriceRate =170.00},
                new Showing { MovieId = 5, SalonId = 1, ShowingTime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, 22, 0, 00), BookedSeats =random.Next(0, maxValue) ,PriceRate =170.00}
              };
            context.Showings.AddRange(showings);
        }

        context.SaveChanges();
    }
}
