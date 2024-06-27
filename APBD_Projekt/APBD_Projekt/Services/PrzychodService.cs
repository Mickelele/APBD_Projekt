using APBD_Projekt.Context;
using APBD_Projekt.Models.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace APBD_Projekt.Services;

public class PrzychodService
{
    private readonly CustomerDbContext _context;
    private readonly ExchangeRateService _exchangeRateService;

    public PrzychodService(CustomerDbContext context, ExchangeRateService exchangeRateService)
    {
        _context = context;
        _exchangeRateService = exchangeRateService;
    }
    
    public async Task<PrzychodDTOReturn> ObliczBiezacyPrzychod(PrzychodDTO przychodDto)
    {
        List<Kontrakt> kontrakty = [];
        if (przychodDto.OprogramowanieID.HasValue)
        {
            kontrakty = await _context.Kontrakty.Where(k =>
                    k.OprogramowanieID == przychodDto.OprogramowanieID && k.CzyPodpisana == true &&
                    k.CzyAktywna == true)
                .ToListAsync();
        }
        else
        {
            kontrakty = await _context.Kontrakty.Where(k => k.CzyPodpisana == true &&
                                                            k.CzyAktywna == true)
                .ToListAsync();
        }

        var przychod = (decimal)kontrakty.Sum(k => k.Cena);
        
        if (przychodDto.Waluta != "PLN")
        {
            var kurs = _exchangeRateService.GetExchangeRateAsync(przychodDto.Waluta);
            przychod /= kurs;
        }
        
        return new PrzychodDTOReturn(przychodDto.Waluta, przychod);
    }
    
    
    public async Task<PrzychodDTOReturn> ObliczPrzewidywanyPrzychod(PrzychodDTO przychodDto)
    {
        List<Kontrakt> kontrakty = [];
        
        if (przychodDto.OprogramowanieID.HasValue)
        {
            kontrakty = await _context.Kontrakty
                .Where(k =>
                    k.OprogramowanieID == przychodDto.OprogramowanieID &&
                    k.CzyAktywna == true)
                .ToListAsync();
        }
        else
        {
            kontrakty = await _context.Kontrakty
                .Where(k => k.CzyAktywna == true)
                .ToListAsync();
        }
        
        
        var przychod = (decimal)kontrakty.Sum(k => k.Cena);
        
        if (przychodDto.Waluta != "PLN")
        {
            var kurs = _exchangeRateService.GetExchangeRateAsync(przychodDto.Waluta);
            przychod /= kurs;
        }
        
        return new PrzychodDTOReturn(przychodDto.Waluta, przychod);
        
    }
    
    
}