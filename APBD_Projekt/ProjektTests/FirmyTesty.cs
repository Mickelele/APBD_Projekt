using APBD_Projekt.Models;
using APBD_Projekt.Models.DTO_s;
using APBD_Projekt.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using APBD_Projekt.Context;
using Xunit;

namespace ProjektTests
{
    public class FirmaServiceTests
    {
        private readonly FirmaService _firmaService;
        private readonly CustomerDbContext _context;
        
        public FirmaServiceTests()
        {
            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;
 
            _context = new CustomerDbContext(options);
            _firmaService = new FirmaService(_context);
        }
        
        [Fact]
        public async Task WstawFirme_DodajeNowaFirme()
        {
            var newCompany = new FirmaDTO()
            {
                NazwaFirmy = "TestFirma",
                Adres = "TestAdres",
                Email = "test@test.com",
                NrTelefonu = "123456789",
                KRS = "1234567890"
            };
            
            await _firmaService.WstawFirme(newCompany);
            
            var addedCompany = await _context.Firmy.FirstOrDefaultAsync(f => f.KRS.Equals("1234567890"));
            Assert.NotNull(addedCompany);
            Assert.Equal("TestFirma", addedCompany.NazwaFirmy);
            Assert.Equal("TestAdres", addedCompany.Adres);
            Assert.Equal("test@test.com", addedCompany.Email);
            Assert.Equal("123456789", addedCompany.NrTelefonu);
            Assert.Equal("1234567890", addedCompany.KRS);
        }
        
        
        [Fact]
        public async Task AktualizujDaneFirmy_AktualizujeDaneFirmy()
        {
            var newCompany = new Firma()
            {
                NazwaFirmy = "TestFirma",
                Adres = "TestAdres",
                Email = "test@test.com",
                NrTelefonu = "123456789",
                KRS = "1234567890"
            };

            _context.Firmy.Add(newCompany);
            await _context.SaveChangesAsync();

            var updateDto = new FirmaDTOUpdate()
            {
                NazwaFirmy = "UpdatedFirma",
                Adres = "UpdatedAdres",
                Email = "updated@test.com",
                NrTelefonu = "987654321"
            };

            var companyId = newCompany.FirmaID;
            await _firmaService.AktualizujDaneFirmy(updateDto, companyId);

            var updatedCompany = await _context.Firmy.FirstOrDefaultAsync(f => f.FirmaID == companyId);
            Assert.NotNull(updatedCompany);
            Assert.Equal("UpdatedFirma", updatedCompany.NazwaFirmy);
            Assert.Equal("UpdatedAdres", updatedCompany.Adres);
            Assert.Equal("updated@test.com", updatedCompany.Email);
            Assert.Equal("987654321", updatedCompany.NrTelefonu);
        }
        
        
        
        
        [Fact]
        public async Task SprawdzCzyFirmaIstnieje_ZwracaTrueJesliFirmaIstnieje()
        {
            var newCompany = new Firma()
            {
                NazwaFirmy = "TestFirma",
                Adres = "TestAdres",
                Email = "test@test.com",
                NrTelefonu = "123456789",
                KRS = "1234567890"
            };

            _context.Firmy.Add(newCompany);
            await _context.SaveChangesAsync();

            var exists = await _firmaService.SprawdzCzyFirmaIstnieje(newCompany.FirmaID);
            Assert.True(exists);
        }
        
        
        [Fact]
        public async Task SprawdzCzyFirmaIstnieje_ZwracaFalseJesliFirmaNieIstnieje()
        {
            var nonExistentCompanyId = 999;

            var exists = await _firmaService.SprawdzCzyFirmaIstnieje(nonExistentCompanyId);
            Assert.False(exists);
        }
        
        [Fact]
        public async Task SprawdzCzyFirmaIstnieje_DlaKRS_ZwracaTrueJesliFirmaIstnieje()
        {
            var newCompany = new Firma()
            {
                NazwaFirmy = "TestFirma",
                Adres = "TestAdres",
                Email = "test@test.com",
                NrTelefonu = "123456789",
                KRS = "1234567890"
            };

            _context.Firmy.Add(newCompany);
            await _context.SaveChangesAsync();

            var firmaDto = new FirmaDTO()
            {
                NazwaFirmy = "TestFirma",
                Adres = "TestAdres",
                Email = "test@test.com",
                NrTelefonu = "123456789",
                KRS = "1234567890"
            };

            var exists = await _firmaService.SprawdzCzyFirmaIstnieje(firmaDto);
            Assert.True(exists);
        }
        
        
        [Fact]
        public async Task SprawdzCzyFirmaIstnieje_DlaKRS_ZwracaFalseJesliFirmaNieIstnieje()
        {
            var firmaDto = new FirmaDTO()
            {
                NazwaFirmy = "NonExistentFirma",
                Adres = "NonExistentAdres",
                Email = "nonexistent@test.com",
                NrTelefonu = "000000000",
                KRS = "0000000000"
            };

            var exists = await _firmaService.SprawdzCzyFirmaIstnieje(firmaDto);
            Assert.False(exists);
        }
        
        
    }
}