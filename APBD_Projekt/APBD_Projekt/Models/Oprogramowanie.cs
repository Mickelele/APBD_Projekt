using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD_Projekt.Models;

public class Oprogramowanie
{
    [Key]
    public int OprogramowanieID { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Nazwa { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Opis { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Wersja { get; set; }
    
    [Required]
    [MaxLength(30)]
    public string Kategoria { get; set; }
    
    [Required]
    [Precision(10, 2)]
    public double Cena { get; set; }
    public int? KlientFizycznyID { get; set; }
    public int? FirmaID { get; set; }
    
    [ForeignKey(nameof(KlientFizycznyID))]
    public KlientFizyczny? KlientFizyczny { get; set; }
    
    [ForeignKey(nameof(FirmaID))]
    public Firma? Firma { get; set; }

    public ICollection<Znizka> Znizki { get; set; } = new HashSet<Znizka>();
}