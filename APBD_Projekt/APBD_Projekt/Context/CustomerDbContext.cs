using System.Data.Common;
using APBD_Projekt.Models;
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


}