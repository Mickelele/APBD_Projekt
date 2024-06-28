using APBD_Projekt.Context;
using APBD_Projekt.Models;
using APBD_Projekt.Models.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace APBD_Projekt.Services;

public class KlientFizycznyService
{

    private readonly CustomerDbContext _context;

    public KlientFizycznyService(CustomerDbContext context)
    {
        _context = context;
    }
    
    
    
    public async Task<ICollection<KlientFizyczny>> PokazKlientowFizycznych()
    {
        return await _context.KlienciFizyczni.Where(a => a.czyUsuniety == false).ToListAsync();
    }
    
    public async Task WstawKlientaFizycznego(KlientFizycznyDTO klientFizycznyDto)
    {
        
        
        
        var client = new KlientFizyczny()
        {
            Imie = klientFizycznyDto.Imie,
            Nazwisko = klientFizycznyDto.Nazwisko,
            Adres = klientFizycznyDto.Adres,
            Email = klientFizycznyDto.Email,
            NrTelefonu = klientFizycznyDto.NrTelefonu,
            PESEL = klientFizycznyDto.PESEL,
            czyUsuniety = false
        };

        await _context.KlienciFizyczni.AddAsync(client);
        await _context.SaveChangesAsync();

    }
    
    public async Task UsunKlientaFizycznego(int id)
    {

        var client = await _context.KlienciFizyczni.FirstAsync(a => a.KlientID == id);
        client.czyUsuniety = true;
        await _context.SaveChangesAsync();
    }
    
    
    public async Task AktualizujKlientaFizycznego(KlientFizycznyDTOUpdate klientFizycznyDto, int id)
    {
        var client = await _context.KlienciFizyczni.FirstAsync(a => a.KlientID == id);

        client.Imie = klientFizycznyDto.Imie;
        client.Nazwisko = klientFizycznyDto.Nazwisko;
        client.Adres = klientFizycznyDto.Adres;
        client.Email = klientFizycznyDto.Email;
        client.NrTelefonu = klientFizycznyDto.NrTelefonu;

        await _context.SaveChangesAsync();
    }


    public async Task<int> SprawdzIstnienieIZaktualizuj(KlientFizycznyDTO klientFizycznyDto)
    {
        var client = await _context.KlienciFizyczni.FirstOrDefaultAsync(a => a.PESEL == klientFizycznyDto.PESEL);

        if (client != null)
        {
            if (client.czyUsuniety)
            {
                client.czyUsuniety = false;
                client.Imie = klientFizycznyDto.Imie;
                client.Nazwisko = klientFizycznyDto.Nazwisko;
                client.Email = klientFizycznyDto.Email;
                client.NrTelefonu = klientFizycznyDto.NrTelefonu;
                client.PESEL = klientFizycznyDto.PESEL;
                
                await _context.SaveChangesAsync();
                return 2;
            }

            return 1;
        }

        return 0;
    }

    public async Task<bool> CzyKlientUsuniety(string PESEL)
    {
        var c = await _context.KlienciFizyczni.FirstAsync(a => a.PESEL.Equals(PESEL));
        return c.czyUsuniety;
    }
    
    public async Task<bool> CzyKlientIstnieje(KlientFizycznyDTO klientFizycznyDto)
    {
        return await _context.KlienciFizyczni.AnyAsync(a => a.PESEL == klientFizycznyDto.PESEL && a.czyUsuniety == false);
    }
    
    public async Task<bool> CzyKlientIstnieje(int id)
    {
        return await _context.KlienciFizyczni.AnyAsync(a => a.KlientID == id & a.czyUsuniety == false);
    }

}