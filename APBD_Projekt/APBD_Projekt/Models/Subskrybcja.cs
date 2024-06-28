
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace APBD_Projekt.Models;

public class Subskrybcja
{
    [Key]
    public int? SubskrybcjaID { get; set; }
    

    public int? ClientID { get; set; }
    

    public string? ClientType { get; set; }
    

    public int? OprogramowanieID { get; set; }
    

    public string? Nazwa { get; set; }
    

    public DateTime CzasOdnowienia { get; set; }
    

    [Precision(10,2)]
    public decimal? Cena { get; set; }


    public bool CzyOplacona { get; set; }
    
    [ForeignKey(nameof(OprogramowanieID))]
    public Oprogramowanie? Oprogramowanie { get; set; }
    
}

