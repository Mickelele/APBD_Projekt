using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD_Projekt.Models.DTO_s;

public class Kontrakt
{
    [Key]
    public int KontraktID { get; set; }
    
    
    public int? ClientID { get; set; }
    
    public string? ClientType { get; set; }
    
    public DateTime? DataWaznosciOd { get; set; }
    
    public DateTime? DataWaznosciDo { get; set; }
    
    public bool? CzyPodpisana { get; set; }
    
    public bool? CzyAktywna { get; set; }
    
    [Precision(10,2)]
    public double? Cena { get; set; }
    
    public List<String>? InformacjaOAktualizacjach { get; set; }
    
    public int? LataWsparcia { get; set; }

    
    public int? ZnizkaProcent { get; set; }

    
    public string? OprogramowanieWersja { get; set; }
    
    public int? OprogramowanieID { get; set; }

    public double? IleZaplacono { get; set; }
    
    [ForeignKey(nameof(OprogramowanieID))]
    public Oprogramowanie? Oprogramowanie { get; set; }

    public ICollection<Platnosc> Platnosci { get; set; } = new HashSet<Platnosc>();

}