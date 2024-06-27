using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD_Projekt.Models.DTO_s;

public class Kontrakt
{
    [Key]
    public int KontraktID { get; set; }
    
    [Required]
    public int ClientID { get; set; }

    [Required]
    public string ClientType { get; set; }
    
    [Required]
    public DateTime DataWaznosciOd { get; set; }
    
    [Required]
    public DateTime DataWaznosciDo { get; set; }
    
    [Required]
    public bool CzyPodpisana { get; set; }

    [Required]
    public bool CzyAktywna { get; set; }

    [Required]
    [Precision(10,2)]
    public double Cena { get; set; }

    [Required]
    public List<String> InformacjaOAktualizacjach { get; set; }

    [Required]
    public int LataWsparcia { get; set; }

    [Required]
    public int ZnizkaProcent { get; set; }

    [Required]
    public string OprogramowanieWersja { get; set; }
    
    public int? OprogramowanieID { get; set; }

    public double IleZaplacono { get; set; }
    
    [ForeignKey(nameof(OprogramowanieID))]
    public Oprogramowanie? Oprogramowanie { get; set; }

    public ICollection<Platnosc> Platnosci { get; set; } = new HashSet<Platnosc>();

}