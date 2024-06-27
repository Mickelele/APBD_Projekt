using APBD_Projekt.Context;
using APBD_Projekt.Models;
using APBD_Projekt.Models.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace APBD_Projekt.Services;

public class PlatnoscService
{
    private readonly CustomerDbContext _context;

    public PlatnoscService(CustomerDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<ICollection<Platnosc>> PokazPlatnosci()
    {
        return await _context.Platnosci.ToListAsync();
    }
    
    
    public async Task DodajPlatnosc(PlatnoscDTO platnoscDto)
    {
        var kontrakt = await _context.Kontrakty.FirstAsync(k => k.KontraktID == platnoscDto.KontraktID);

        var nowaPlatnosc = new Platnosc()
        {
            KlientID = platnoscDto.KlientID,
            IleZaplacono = platnoscDto.IleZaplacono,
            PozostaloDoZaplaty = kontrakt.Cena - (kontrakt.IleZaplacono + platnoscDto.IleZaplacono),
            KontraktID = platnoscDto.KontraktID
        };

        kontrakt.IleZaplacono += platnoscDto.IleZaplacono;

        if (kontrakt.Cena == kontrakt.IleZaplacono)
        {
            kontrakt.CzyPodpisana = true;
        }

        await _context.Platnosci.AddAsync(nowaPlatnosc);
        await _context.SaveChangesAsync();

    }

    public async Task<bool> czyKwotaJestZaWysoka(PlatnoscDTO platnoscDto)
    {
        var kontrakt = await _context.Kontrakty.FirstAsync(k => k.KontraktID == platnoscDto.KontraktID);

        var pozostaleNaleznosci = kontrakt.Cena - kontrakt.IleZaplacono;

        return platnoscDto.IleZaplacono > pozostaleNaleznosci;
    }

    public async Task<bool> czyTerminMinal(PlatnoscDTO platnoscDto)
    {
        var kontrakt = await _context.Kontrakty.FirstAsync(k => k.KontraktID == platnoscDto.KontraktID);
        return DateTime.Now > kontrakt.DataWaznosciDo; 
    }

    public async Task PrzygotujNowaUmowe(PlatnoscDTO platnoscDto)
    {
        var kontrakt = await _context.Kontrakty.FirstAsync(k => k.KontraktID == platnoscDto.KontraktID);
        var nowaUmowa = new Kontrakt()
        {
            ClientID = kontrakt.ClientID,
            ClientType = kontrakt.ClientType,
            DataWaznosciOd = DateTime.Now,
            DataWaznosciDo = DateTime.Now.AddDays(15),
            CzyPodpisana = false,
            CzyAktywna = true,
            Cena = kontrakt.Cena,
            InformacjaOAktualizacjach = kontrakt.InformacjaOAktualizacjach,
            LataWsparcia = kontrakt.LataWsparcia,
            ZnizkaProcent = kontrakt.ZnizkaProcent,
            OprogramowanieWersja = kontrakt.OprogramowanieWersja,
            OprogramowanieID = kontrakt.OprogramowanieID,
            IleZaplacono = 0
        };

        var platnosci = await _context.Platnosci.Where(a => a.KontraktID == platnoscDto.KontraktID).ToListAsync();
        _context.RemoveRange(platnosci);
        
        kontrakt.IleZaplacono = 0;
        kontrakt.CzyAktywna = false;
        await _context.AddAsync(nowaUmowa);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CzyKontraktDanegoKlientaIstnieje(PlatnoscDTO platnoscDto)
    {
        return await _context.Kontrakty.AnyAsync(k => k.KontraktID == platnoscDto.KontraktID && k.ClientID == platnoscDto.KlientID && k.CzyAktywna == true);
    }

}