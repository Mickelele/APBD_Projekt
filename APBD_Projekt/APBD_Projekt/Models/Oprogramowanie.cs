using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APBD_Projekt.Models.DTO_s;
using APBD_Projekt.Services;
using Microsoft.EntityFrameworkCore;
namespace APBD_Projekt.Models;

public class Oprogramowanie
{
    [Key]
    public int OprogramowanieID { get; set; }
    

    [MaxLength(50)]
    public string? Nazwa { get; set; }
    

    [MaxLength(200)]
    public string? Opis { get; set; }
    

    [MaxLength(100)]
    public string? Wersja { get; set; }
    

    [MaxLength(30)]
    public string? Kategoria { get; set; }
    

    [Precision(10, 2)]
    public double? Cena { get; set; }
    
    public ICollection<Kontrakt>? Kontrakty { get; set; } = new HashSet<Kontrakt>();
    
    
    public ICollection<Subskrybcja>? Subskrybcje { get; set; } = new HashSet<Subskrybcja>();
    
    public ICollection<Znizka>? Znizki { get; set; } = new HashSet<Znizka>();
}