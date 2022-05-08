using System.ComponentModel.DataAnnotations;

namespace BerrasBio.Models;

public class Salon
{
    [Key]
    [Display(Name = "Salon ID")]
    public int SalonId { get; set; }

    [Required]
    [Display(Name = "Seats Available")]
    public int SeatsAvailable { get; set; }

    public ICollection<Showing> Showings { get; set; }
}
