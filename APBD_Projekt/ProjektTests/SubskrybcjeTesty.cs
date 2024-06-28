using APBD_Projekt.Context;
using APBD_Projekt.Models;
using APBD_Projekt.Models.DTO_s;
using APBD_Projekt.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjektTests
{
    public class SubskrybcjeTesty : IDisposable
    {
        private readonly CustomerDbContext _context;
        private readonly SubskrybcjaService _subskrybcjaService;

        public SubskrybcjeTesty()
        {
            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new CustomerDbContext(options);
            SeedDatabase();
            
            _subskrybcjaService = new SubskrybcjaService(_context);
        }

        private void SeedDatabase()
        {
            var oprogramowanie = new Oprogramowanie
            {
                OprogramowanieID = 1,
                Nazwa = "Oprogramowanie testowe",
                Cena = 100,
                Znizki = new[] { new Znizka { Wartosc = 10 } }
            };

            _context.Oprogramowania.Add(oprogramowanie);
            
            
            var klientFizyczny = new KlientFizyczny
            {
                Imie = "Jan",
                Nazwisko = "Kowalski",
                Adres = "Ciasteczkowa",
                Email = "JanKowalski@wp.pl",
                NrTelefonu = "796674772",
                PESEL = "03310106912",
                czyUsuniety = false
            };
            _context.KlienciFizyczni.Add(klientFizyczny);

            _context.SaveChanges();
        }

        [Fact]
        public async Task ZakupNowejSubskrybcji_ShouldCreateNewSubscription()
        {
            // Arrange
            var subskrybcjaDto = new SubskrybcjaDTO
            {
                OprogramowanieID = 1,
                CzasOdnowienia = DateTime.Now.AddMonths(6)
            };
            var clientId =  await _context.KlienciFizyczni.Where(a => a.Nazwisko.Equals("Kowalski")).Select(a => a.KlientID).FirstOrDefaultAsync();
            var clientType = "klientfizyczny";

            // Act
            await _subskrybcjaService.ZakupNowejSubskrybcji(subskrybcjaDto, clientId, clientType);

            // Assert
            var createdSubskrybcja = await _context.Subskrybcje.FirstOrDefaultAsync(s => s.ClientID == clientId && s.ClientType == clientType.ToLower());
            Assert.NotNull(createdSubskrybcja);
            Assert.Equal((decimal?)(90/2*0.9), createdSubskrybcja.Cena); // Cena po uwzględnieniu zniżek i konwersji na dzień
            Assert.True(createdSubskrybcja.CzyOplacona);
        }

        [Fact]
        public async Task StworzNowaSubskrybcje_ShouldCreateNewSubscriptionIfPreviousOnePaid()
        {
            
            var subskrybcjaDto = new SubskrybcjaDTO
            {
                OprogramowanieID = 1,
                CzasOdnowienia = DateTime.Now.AddMonths(6)
            };
            var clientId = 2;
            var clientType = "Firma";

            
            await _subskrybcjaService.StworzNowaSubskrybcje(subskrybcjaDto, clientId, clientType);

            // Assert
            var createdSubskrybcja = await _context.Subskrybcje.FirstOrDefaultAsync(s => s.ClientID == clientId && s.ClientType == clientType.ToLower());
            Assert.NotNull(createdSubskrybcja);
            Assert.Equal(20.74m, createdSubskrybcja.Cena);
            Assert.False(createdSubskrybcja.CzyOplacona);
        }

        [Fact]
        public async Task ZaplacZaSubskrybcje_ShouldPayForSubscription()
        {
            // Arrange
            var subskrybcja = new Subskrybcja
            {
                SubskrybcjaID = 1,
                ClientID = 3,
                ClientType = "firma",
                OprogramowanieID = 1,
                CzasOdnowienia = DateTime.Now.AddMonths(6),
                Cena = 20.74m,
                CzyOplacona = false
            };
            _context.Subskrybcje.Add(subskrybcja);
            await _context.SaveChangesAsync();

            var platnoscDto = new SubskrybcjaDTOPlatnosc
            {
                OprogramowanieID = 1,
                Kwota = 20.74m
            };
            var clientId = 3;
            var clientType = "firma";

            // Act
            await _subskrybcjaService.ZaplacZaSubskrybcje(platnoscDto, clientId, clientType);

            // Assert
            var updatedSubskrybcja = await _context.Subskrybcje.FindAsync(1);
            Assert.True(updatedSubskrybcja.CzyOplacona);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
