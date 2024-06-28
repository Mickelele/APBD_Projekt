using APBD_Projekt.Context;
using APBD_Projekt.Migrations;
using APBD_Projekt.Models;
using APBD_Projekt.Models.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace APBD_Projekt.Services;

public class SubskrybcjaService
{

    private readonly CustomerDbContext _context;

    public SubskrybcjaService(CustomerDbContext context)
    {
        _context = context;
    }
    
    
    public async Task ZakupNowejSubskrybcji(SubskrybcjaDTO subskrybcjaDto, int id, string type)
    {
        var cena = await _context.Oprogramowania
            .Where(o => o.OprogramowanieID == subskrybcjaDto.OprogramowanieID)
            .Select(o => o.Cena)
            .FirstAsync();
             
        var iloscDni = await PobierzIloscDni(subskrybcjaDto);

        
        cena *= (iloscDni.TotalDays / 365);
        
            
        var nowaSubskrybcja = new Subskrybcja()
        {
            ClientID = id,
            ClientType = type,
            OprogramowanieID = subskrybcjaDto.OprogramowanieID,
            Nazwa = await _context.Oprogramowania.Where(o => o.OprogramowanieID == subskrybcjaDto.OprogramowanieID).Select(a => a.Nazwa).FirstAsync(),
            CzasOdnowienia = subskrybcjaDto.CzasOdnowienia,
            Cena = (decimal)cena * ((100 - (decimal)await UzyskajZnizke(subskrybcjaDto, id, type))/100),
            CzyOplacona = true
        };
        


        await _context.Subskrybcje.AddAsync(nowaSubskrybcja);
        await _context.SaveChangesAsync();
        

    }

public async Task StworzNowaSubskrybcje(SubskrybcjaDTO subskrybcjaDto, int id, string type)
{
    var ostatniaSubskrybcja = await _context.Subskrybcje
        .Where(s => s.ClientID == id && s.ClientType.Equals(type.ToLower()))
        .OrderByDescending(s => s.CzasOdnowienia)
        .FirstOrDefaultAsync();

    if (ostatniaSubskrybcja != null && ostatniaSubskrybcja.CzyOplacona)
    {
        var cena = await _context.Oprogramowania
            .Where(o => o.OprogramowanieID == subskrybcjaDto.OprogramowanieID)
            .Select(o => o.Cena)
            .FirstAsync();

        decimal znizka = 1;
        if (await CzyZnizkaKliencka(id, type))
        {
            znizka -= (decimal)0.05;
        }

        var nowaSubskrybcja = new Subskrybcja()
        {
            ClientID = id,
            ClientType = type,
            OprogramowanieID = subskrybcjaDto.OprogramowanieID,
            Nazwa = await _context.Oprogramowania
                .Where(o => o.OprogramowanieID == subskrybcjaDto.OprogramowanieID)
                .Select(a => a.Nazwa)
                .FirstAsync(),
            CzasOdnowienia = ostatniaSubskrybcja.CzasOdnowienia.AddMonths(3), 
            Cena = (decimal)cena * ((decimal)90 / 365) * znizka,
            CzyOplacona = false
        };

        await _context.Subskrybcje.AddAsync(nowaSubskrybcja);
        await _context.SaveChangesAsync();
    }
}

public async Task ZaplacZaSubskrybcje(SubskrybcjaDTOPlatnosc subskrybcjaDtoPlatnosc, int id, string type)
{
    var subskrybcja =
        await _context.Subskrybcje.FirstOrDefaultAsync(s => s.ClientID == id && s.ClientType.Equals(type.ToLower()) && s.CzyOplacona == false);

    if (subskrybcja == null)
    {
        throw new InvalidDataException("Nie znaleziono subskrypcji do opłacenia.");
    }

    if (subskrybcja.CzasOdnowienia < DateTime.Now)
    {
        _context.Subskrybcje.Remove(subskrybcja);
        await _context.SaveChangesAsync();
        throw new InvalidDataException("Przekroczono czas platnosci. Subskrybcja zostala anulowana.");
    }

    if (subskrybcja.Cena == subskrybcjaDtoPlatnosc.Kwota)
    {
        subskrybcja.CzyOplacona = true;
        await _context.SaveChangesAsync(); 

        SubskrybcjaDTO s = new SubskrybcjaDTO
        {
            OprogramowanieID = subskrybcjaDtoPlatnosc.OprogramowanieID,
            CzasOdnowienia = subskrybcja.CzasOdnowienia
        };

        await StworzNowaSubskrybcje(s, id, type);
    }
}



    public async Task SprawdzCzasOdnowienia(SubskrybcjaDTO subskrybcjaDto)
    {
        var roznica = subskrybcjaDto.CzasOdnowienia - DateTime.Now;
        if (!(roznica.TotalDays >= 30 && roznica.TotalDays <= 730))
        {
            throw new InvalidDataException($"Czas odnowienia wybiega poza nasze razy czasowe 30 dni -> 2 lata");
        }
    }
    
    public async Task<TimeSpan> PobierzIloscDni(SubskrybcjaDTO subskrybcjaDto)
    {
        return subskrybcjaDto.CzasOdnowienia - DateTime.Now;
    }
    
    public async Task<bool> CzyZnizkaKliencka(int id, string type)
    {
        return await _context.Subskrybcje.AnyAsync(s => s.ClientID == id && s.ClientType.Equals(type.ToLower()));
    }
    
    public async Task<bool> CzySubskrybcjaIstnieje(SubskrybcjaDTOPlatnosc subskrybcjaDtoPlatnosc, int id, string type)
    {
        return await _context.Subskrybcje.AnyAsync(s => s.ClientID == id && s.ClientType.Equals(type.ToLower()) && s.OprogramowanieID == subskrybcjaDtoPlatnosc.OprogramowanieID && s.CzyOplacona == false);
    }
    

    
    public async Task<bool> CzyOprogramowanieIstnieje(SubskrybcjaDTO subskrybcjaDto)
    {
        return await _context.Oprogramowania
            .AnyAsync(o => o.OprogramowanieID == subskrybcjaDto.OprogramowanieID);
    }
    
    
    public async Task<bool> CzyCenaSieZgadza(SubskrybcjaDTOPlatnosc subskrybcjaDtoPlatnosc, int id, string type)
    {
        return subskrybcjaDtoPlatnosc.Kwota != await _context.Subskrybcje.Where(s => s.ClientID == id && s.ClientType.Equals(type.ToLower()) && s.CzyOplacona == false).Select(s => s.Cena).FirstAsync();
    }
    
    
    
    public async Task<bool> CzyKlientIstnieje(int id, string type)
    {
        if (type.ToLower().Equals("klientfizyczny"))
        {
            return await _context.KlienciFizyczni.AnyAsync(k => k.KlientID == id);
        }
        if(type.ToLower().Equals("firma"))
        {
            return await _context.Firmy.AnyAsync(f => f.FirmaID == id);
        }

        return false;
    }
    
    
    public async Task<bool> CzyKlientMaJuzAktywneOprogramowanie(SubskrybcjaDTO subskrybcjaDto, int id, string type)
    {
        return await _context.Subskrybcje
            .AnyAsync(s => s.ClientID == id && s.ClientType == type.ToLower() && s.OprogramowanieID == subskrybcjaDto.OprogramowanieID);
    }
    
    
    public async Task<int> UzyskajZnizke(SubskrybcjaDTO subskrybcjaDto, int id, string type)
    {
        int znizka = 0;
        var znizki = await _context.Oprogramowania
            .Where(o => o.OprogramowanieID == subskrybcjaDto.OprogramowanieID)
            .Include(o => o.Znizki)
            .SelectMany(o => o.Znizki)
            .ToListAsync();

        if (znizki.Any())
        {
            znizka += znizki.Max(z => z.Wartosc);
        }

        var czyUzytkownikKupowalWczesniej = await _context.Subskrybcje.FirstOrDefaultAsync(a => a.ClientID == id && a.ClientType == type.ToLower());

        if (czyUzytkownikKupowalWczesniej != null)
        {
            znizka += 5;
        }

        return znizka;
    }

}