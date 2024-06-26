using System.ComponentModel.DataAnnotations;

namespace APBD_Projekt.Models.DTO_s;

public class FirmaDTO
{
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
}


public class FirmaDTOUpdate
{
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
    
}