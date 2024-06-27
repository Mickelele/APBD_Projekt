namespace APBD_Projekt.Models.DTO_s;

public class PrzychodDTO
{
    public string Waluta { get; set; } = "PLN";
    public int? OprogramowanieID { get; set; }
}

public class PrzychodDTOReturn
{

    public string Waluta { get; set; } = "PLN";
    public decimal Przychod { get; set; }
    
    
    public PrzychodDTOReturn(string waluta, decimal przychod)
    {
        Waluta = waluta;
        Przychod = przychod;
    }
}
