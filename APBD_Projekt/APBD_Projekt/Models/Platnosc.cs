using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APBD_Projekt.Models.DTO_s;

namespace APBD_Projekt.Models;

public class Platnosc
{
    [Key]
    public int PlatnoscID { get; set; }
    
    [Required]
    public int KlientID { get; set; }
    [Required]
    public double IleZaplacono { get; set; }
    [Required]
    public double PozostaloDoZaplaty { get; set; }
    [Required]
    public int KontraktID { get; set; }
    
    [ForeignKey(nameof(KontraktID))]
    public Kontrakt Kontrakt { get; set; }
}