using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APBD_Projekt.Models.DTO_s;

namespace APBD_Projekt.Models;

public class Platnosc
{
    [Key]
    public int? PlatnoscID { get; set; }
    
    public int? KlientID { get; set; }

    public double? IleZaplacono { get; set; }

    public double? PozostaloDoZaplaty { get; set; }

    public int? KontraktID { get; set; }
    
    
    [ForeignKey(nameof(KontraktID))]
    public Kontrakt? Kontrakt { get; set; }
    
}