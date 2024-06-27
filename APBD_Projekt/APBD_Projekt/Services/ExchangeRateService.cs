using System.Text.Json;
using Microsoft.SqlServer.Server;

namespace APBD_Projekt;

public class ExchangeRateService
{

    private static Dictionary<string, decimal> kursyWalut = new Dictionary<string, decimal>
    {
        { "USD", 4.5m },   
        { "EUR", 4.2m },   
        { "GBP", 5.2m },   
        { "CHF", 4.8m },   
        { "JPY", 0.042m }
    };

        

    public decimal GetExchangeRateAsync(string waluta)
    {
        var pair = kursyWalut.FirstOrDefault(a => a.Key.Equals(waluta.ToUpper()));
        if (pair.Key == null)
        {
            throw new InvalidOperationException($"Brak podanej waluty w bazie.");
        }
        return pair.Value;
    }

    
}