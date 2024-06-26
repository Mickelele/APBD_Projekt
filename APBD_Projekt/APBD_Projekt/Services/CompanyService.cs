using APBD_Projekt.Context;
using APBD_Projekt.Models;
using APBD_Projekt.Models.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace APBD_Projekt.Services;

public class CompanyService
{
    private readonly CustomerDbContext _context;

    public CompanyService(CustomerDbContext context)
    {
        _context = context;
    }
    
    
    
    public async Task<ICollection<Firma>> PokazFirmy()
    {
        return await _context.Firmy.ToListAsync();
    }
    
    
    public async Task WstawKlientaFizycznego(FirmaDTO firmaDto)
    {
        var firma = new Firma()
        {
            NazwaFirmy = firmaDto.NazwaFirmy,
            Adres = firmaDto.Adres,
            Email = firmaDto.Email,
            NrTelefonu = firmaDto.NrTelefonu,
            KRS = firmaDto.KRS,
        };

        await _context.Firmy.AddAsync(firma);
        await _context.SaveChangesAsync();

    }
    
    
    public async Task AktualizujDaneFirmy(FirmaDTOUpdate firmaDto, int id)
    {
        var firma = await _context.Firmy.FirstAsync(a => a.FirmaID == id);
        firma.NazwaFirmy = firmaDto.NazwaFirmy;
        firma.Adres = firmaDto.Adres;
        firma.Email = firmaDto.Email;
        firma.NrTelefonu = firmaDto.NrTelefonu;

        await _context.SaveChangesAsync();
    }
    
    
    
    
    
    public async Task<bool> SprawdzCzyFirmaIstnieje(FirmaDTO firmaDto)
    {
        return await _context.Firmy.AnyAsync(a => a.KRS == firmaDto.KRS);
    }
    
    public async Task<bool> SprawdzCzyFirmaIstnieje(int id)
    {
        return await _context.Firmy.AnyAsync(a => a.FirmaID == id);
    }
    
    
    
    
}

