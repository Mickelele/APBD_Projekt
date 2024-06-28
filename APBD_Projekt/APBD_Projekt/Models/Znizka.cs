using System.ComponentModel.DataAnnotations;

namespace APBD_Projekt.Models;

public class Znizka
{
    [Key]
    public int ZnikaID { get; set; }
    

    [MaxLength(100)]
    public string? Oferta { get; set; }
    

    public int Wartosc { get; set; }
    

    public DateTime? ObowiazujeOd { get; set; }
    

    public DateTime? ObowiazujeDo { get; set; }
    
    public ICollection<Oprogramowanie>? Oprogramowania { get; set; } = new HashSet<Oprogramowanie>();
    
}