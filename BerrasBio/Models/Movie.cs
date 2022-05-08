using System.ComponentModel.DataAnnotations;

namespace BerrasBio.Models;

public class Movie
{
    [Key]
    [Display(Name = "Movie ID")]
    public int MovieId { get; set; }

    [Required]
    [Display(Name = "Movie Title")]
    public string MovieTitle { get; set; }

    [Required]
    [Display(Name = "Run Time")]
    public int RunTime { get; set; }

    public string PhotoPath { get; set; }

    
    public string Description { get; set; }
    public ICollection<Showing> Showings { get; set; }
}
