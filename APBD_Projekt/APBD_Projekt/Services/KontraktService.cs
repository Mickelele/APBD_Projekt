using System.Security.Authentication.ExtendedProtection;
using APBD_Projekt.Context;
using APBD_Projekt.Models.DTO_s;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace APBD_Projekt.Services;

public class KontraktService
{

    private readonly CustomerDbContext _context;

    public KontraktService(CustomerDbContext context)
    {
        _context = context;
    }
    
    
    
    public async Task DodajKontrakt(KontraktDTO kontraktDto)
    {
        var cena = await _context.Oprogramowania
            .Where(o => o.OprogramowanieID == kontraktDto.OprogramowanieID)
            .Select(o => o.Cena)
            .FirstAsync();

        cena += (1000 * kontraktDto.LataDodatkowegoWsparcia);

        var Nazwa = await _context.Oprogramowania
            .Where(o => o.OprogramowanieID == kontraktDto.OprogramowanieID)
            .Select(o => o.Nazwa)
            .FirstAsync();
        
        var aktualizacje = await _context.Oprogramowania
            .Where(a => a.Nazwa == Nazwa)
            .Select(a => a.Wersja)
            .ToListAsync();

        var wersja = await _context.Oprogramowania
            .Where(o => o.OprogramowanieID == kontraktDto.OprogramowanieID)
            .Select(o => o.Wersja)
            .FirstAsync();

        var kontrakt = new Kontrakt()
        {
            ClientID = kontraktDto.ClientID,
            ClientType = kontraktDto.ClientType.ToLower(),
            DataWaznosciOd = kontraktDto.DataWaznosciOd,
            DataWaznosciDo = kontraktDto.DataWaznosciDo,
            CzyPodpisana = false,
            CzyAktywna = true,
            ZnizkaProcent = await UzyskajZnizke(kontraktDto),
            Cena = cena * ((100 - (double)await UzyskajZnizke(kontraktDto))/100),
            InformacjaOAktualizacjach = aktualizacje,
            LataWsparcia = 1 + kontraktDto.LataDodatkowegoWsparcia,
            OprogramowanieWersja = wersja,
            OprogramowanieID = kontraktDto.OprogramowanieID,
            IleZaplacono = 0
        };
        await _context.AddAsync(kontrakt);
        await _context.SaveChangesAsync();
    }

    
    public async Task UsunKontrakt(int id)
    {
        var platnosci = await _context.Platnosci.Where(p => p.KontraktID == id).ToListAsync();
        if (platnosci.Count != 0)
        {
            _context.RemoveRange(platnosci);
        }
        
        var kontrakt = await _context.Kontrakty.FirstAsync(k => k.KontraktID == id);
        _context.Kontrakty.Remove(kontrakt);
        await _context.SaveChangesAsync();
    }
    
    
    public async Task<bool> CzyKlientIstnieje(KontraktDTO kontraktDto)
    {
        if (kontraktDto.ClientType.ToLower().Equals("klientfizyczny"))
        {
           return await _context.KlienciFizyczni.AnyAsync(c => c.KlientID == kontraktDto.ClientID && c.czyUsuniety == false);
        }

        if (kontraktDto.ClientType.ToLower().Equals("firma"))
        {
            return await _context.Firmy.AnyAsync(f => f.FirmaID == kontraktDto.ClientID);
        }
        return false;
    }
    
    public async Task<bool> CzyKlientNieMaJuzAktywnegoKontrkatu(KontraktDTO kontraktDto)
    {
        
        return await _context.Kontrakty.AnyAsync(c => c.ClientID == kontraktDto.ClientID && c.ClientType.ToLower().Equals(kontraktDto.ClientType.ToLower()) && c.OprogramowanieID == kontraktDto.OprogramowanieID && c.CzyPodpisana == false);
    }
    

    public bool CzyDataMiesciSieWZakresie(KontraktDTO kontraktDto)
    {
        var roznica = kontraktDto.DataWaznosciDo - kontraktDto.DataWaznosciOd;
        return roznica.TotalDays >= 3 && roznica.TotalDays <= 30 && kontraktDto.LataDodatkowegoWsparcia >= 0 && kontraktDto.LataDodatkowegoWsparcia <= 3;
    }
    
    public bool CzyOplaconaWTerminie(KontraktDTO kontraktDto)
    {
        return DateTime.Now < kontraktDto.DataWaznosciDo;
    }
    
    public async Task<bool> CzyOprogramowanieIstnieje(int id)
    {
        return await _context.Oprogramowania.AnyAsync(a => a.OprogramowanieID == id);
    }


    public async Task<int> UzyskajZnizke(KontraktDTO kontraktDto)
    {
        int znizka = 0;
        var znizki = await _context.Oprogramowania
            .Where(o => o.OprogramowanieID == kontraktDto.OprogramowanieID)
            .Include(o => o.Znizki)
            .SelectMany(o => o.Znizki)
            .ToListAsync();

        if (znizki.Any())
        {
            znizka += znizki.Max(z => z.Wartosc);
        }

        var czyUzytkownikKupowalWczesniej = await _context.Kontrakty.FirstOrDefaultAsync(a => a.ClientID == kontraktDto.ClientID);

        if (czyUzytkownikKupowalWczesniej != null)
        {
            znizka += 5;
        }

        return znizka;
    }


    public async Task<bool> CzyKontraktIstnieje(int id)
    {
        return await _context.Kontrakty.AnyAsync(k => k.KontraktID == id);
    }
    
    

}