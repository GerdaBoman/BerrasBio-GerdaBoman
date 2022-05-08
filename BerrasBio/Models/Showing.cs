using System.ComponentModel.DataAnnotations;

namespace BerrasBio.Models;

public class Showing
{
    [Key]
    public int ShowingId { get; set; }
    [Display(Name = "Movie ID")]
    public int MovieId { get; set; }
    [Display(Name = "Salon ID")]
    public int SalonId { get; set; }

    [Required]
    [DataType(DataType.DateTime)]
    [Display(Name = "Showing Time")]
    public DateTime ShowingTime { get; set; }

    [Range(0,50,ErrorMessage ="Not enough seating available for selected movie!")]
    [DisplayFormat(NullDisplayText = "0")]
    [Display(Name = "Booked Seats")]
    public int? BookedSeats { get; set; }

    [Required, Display(Name = "Price"), DataType(DataType.Currency)]
    public double PriceRate { get; set; }

    public Movie Movie { get; set; }

    public Salon Salon { get; set; }
}
