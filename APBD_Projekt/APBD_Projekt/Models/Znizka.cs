using System.ComponentModel.DataAnnotations;

namespace APBD_Projekt.Models;

public class Znizka
{
    [Key]
    public int ZnikaID { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Oferta { get; set; }
    
    [Required]
    public int Wartosc { get; set; }
    
    [Required]
    public DateTime ObowiazujeOd { get; set; }
    
    [Required]
    public DateTime ObowiazujeDo { get; set; }
    
    public ICollection<Oprogramowanie> Oprogramowania { get; set; } = new HashSet<Oprogramowanie>();
    
}