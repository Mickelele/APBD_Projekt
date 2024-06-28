using System.Data.Common;
using APBD_Projekt.Models;
using APBD_Projekt.Models.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace APBD_Projekt.Context;

public class CustomerDbContext : DbContext
{
    protected CustomerDbContext()
    {
        
    }

    public CustomerDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<KlientFizyczny> KlienciFizyczni { get; set; }
    public DbSet<Firma> Firmy { get; set; }
    public DbSet<Oprogramowanie> Oprogramowania { get; set; }
    public DbSet<Znizka> Znizki { get; set; }
    public DbSet<Kontrakt> Kontrakty { get; set; }
    public DbSet<Platnosc> Platnosci { get; set; }
    public DbSet<AppUser> Uzytkownicy { get; set; }
    public DbSet<Subskrybcja> Subskrybcje { get; set; }


}