namespace APBD_Projekt.Models.DTO_s;

public class KontraktDTO
{

    public int ClientID { get; set; }

    public string ClientType { get; set; }
    public DateTime DataWaznosciOd { get; set; }
    public DateTime DataWaznosciDo { get; set; }
    
    public int LataDodatkowegoWsparcia { get; set; }
    
    public int OprogramowanieID { get; set; }
}