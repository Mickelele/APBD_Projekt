using System.ComponentModel.DataAnnotations;

namespace APBD_Projekt.Models;

public class Firma
{   [Key]
    public int FirmaID { get; set; }
    
    [MaxLength(200)]
    [Required]
    public string NazwaFirmy { get; set; }
    
    [MaxLength(200)]
    [Required]
    public string Adres { get; set; }
    
    [MaxLength(100)]
    [Required]
    public string Email { get; set; }
    
    [MaxLength(20)]
    [Required]
    public string NrTelefonu { get; set; }
    
    [MaxLength(50)]
    [Required]
    public string KRS { get; set; }

    public ICollection<Oprogramowanie> Oprogramowania = new HashSet<Oprogramowanie>();
}