using System.ComponentModel.DataAnnotations;

namespace APBD_Projekt.Models;

public class KlientFizyczny
{
    [Key]
    public int KlientID { get; set; }
    
    [MaxLength(30)]
    [Required]
    public string Imie { get; set; }
    
    [MaxLength(50)]
    [Required]
    public string Nazwisko { get; set; }
    
    [MaxLength(200)]
    [Required]
    public string Adres { get; set; }
    
    [MaxLength(100)]
    [Required]
    public string Email { get; set; }
    
    [MaxLength(20)]
    [Required]
    public string NrTelefonu { get; set; }
    
    [MaxLength(20)]
    [Required]
    public string PESEL { get; set; }
    
    public bool czyUsuniety { get; set; }
    
    public ICollection<Oprogramowanie> Oprogramowania = new HashSet<Oprogramowanie>();
}