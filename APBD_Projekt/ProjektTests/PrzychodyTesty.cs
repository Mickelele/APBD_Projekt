using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD_Projekt;
using APBD_Projekt.Context;
using APBD_Projekt.Models;
using APBD_Projekt.Models.DTO_s;
using APBD_Projekt.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ProjektTests
{
    public class PrzychodServiceTests
    {
        private readonly CustomerDbContext _context;
        private readonly PrzychodService _przychodService;
        private readonly ExchangeRateService _exchangeRateService;

        public PrzychodServiceTests()
        {
            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new CustomerDbContext(options);

            // Mock ExchangeRateService
            _exchangeRateService = new ExchangeRateService();

            _przychodService = new PrzychodService(_context, _exchangeRateService);
        }

        [Fact]
        public async Task ObliczBiezacyPrzychod_ShouldCalculateCurrentRevenue()
        {
            // Arrange
            var kontrakty = new List<Kontrakt>
            {
                new Kontrakt { KontraktID = 1, Cena = 1000, CzyPodpisana = true, CzyAktywna = true },
                new Kontrakt { KontraktID = 2, Cena = 1500, CzyPodpisana = true, CzyAktywna = true }
            };
            _context.Kontrakty.AddRange(kontrakty);
            await _context.SaveChangesAsync();

            var subskrybcje = new List<Subskrybcja>
            {
                new Subskrybcja { SubskrybcjaID = 1, Cena = 500, CzyOplacona = true },
                new Subskrybcja { SubskrybcjaID = 2, Cena = 800, CzyOplacona = true }
            };
            _context.Subskrybcje.AddRange(subskrybcje);
            await _context.SaveChangesAsync();

            var przychodDto = new PrzychodDTO { Waluta = "PLN" };

            // Act
            var result = await _przychodService.ObliczBiezacyPrzychod(przychodDto);

            // Assert
            Assert.Equal("PLN", result.Waluta);
            Assert.Equal(3800, result.Przychod); // Sum of kontrakty and subskrybcje
        }

        [Fact]
        public async Task ObliczBiezacyPrzychod_ShouldConvertToOtherCurrency()
        {
            // Arrange
            var kontrakty = new List<Kontrakt>
            {
                new Kontrakt { KontraktID = 1, Cena = 1000, CzyPodpisana = true, CzyAktywna = true },
                new Kontrakt { KontraktID = 2, Cena = 1500, CzyPodpisana = true, CzyAktywna = true }
            };
            _context.Kontrakty.AddRange(kontrakty);
            await _context.SaveChangesAsync();

            var subskrybcje = new List<Subskrybcja>
            {
                new Subskrybcja { SubskrybcjaID = 1, Cena = 500, CzyOplacona = true },
                new Subskrybcja { SubskrybcjaID = 2, Cena = 800, CzyOplacona = true }
            };
            _context.Subskrybcje.AddRange(subskrybcje);
            await _context.SaveChangesAsync();

            var przychodDto = new PrzychodDTO { Waluta = "EUR" };
            var exchangeRate = 4.2m; // Example exchange rate

            // Act
            var result = await _przychodService.ObliczBiezacyPrzychod(przychodDto);

            // Assert
            Assert.Equal("EUR", result.Waluta);
            Assert.Equal(904.76m, result.Przychod);
        }

        [Fact]
        public async Task ObliczPrzewidywanyPrzychod_ShouldCalculateExpectedRevenue()
        {
            // Arrange
            var kontrakty = new List<Kontrakt>
            {
                new Kontrakt { KontraktID = 1, Cena = 1000, CzyAktywna = true },
                new Kontrakt { KontraktID = 2, Cena = 1500, CzyAktywna = true }
            };
            _context.Kontrakty.AddRange(kontrakty);
            await _context.SaveChangesAsync();

            var subskrybcje = new List<Subskrybcja>
            {
                new Subskrybcja { SubskrybcjaID = 1, Cena = 500 },
                new Subskrybcja { SubskrybcjaID = 2, Cena = 800 }
            };
            _context.Subskrybcje.AddRange(subskrybcje);
            await _context.SaveChangesAsync();

            var przychodDto = new PrzychodDTO { Waluta = "PLN" };

            // Act
            var result = await _przychodService.ObliczPrzewidywanyPrzychod(przychodDto);

            // Assert
            Assert.Equal("PLN", result.Waluta);
            Assert.Equal(3800, result.Przychod); 
        }

        [Fact]
        public async Task ObliczPrzewidywanyPrzychod_ShouldConvertToOtherCurrency()
        {
            // Arrange
            var kontrakty = new List<Kontrakt>
            {
                new Kontrakt { KontraktID = 1, Cena = 1000, CzyAktywna = true },
                new Kontrakt { KontraktID = 2, Cena = 1500, CzyAktywna = true }
            };
            _context.Kontrakty.AddRange(kontrakty);
            await _context.SaveChangesAsync();

            var subskrybcje = new List<Subskrybcja>
            {
                new Subskrybcja { SubskrybcjaID = 1, Cena = 500 },
                new Subskrybcja { SubskrybcjaID = 2, Cena = 800 }
            };
            _context.Subskrybcje.AddRange(subskrybcje);
            await _context.SaveChangesAsync();

            var przychodDto = new PrzychodDTO { Waluta = "USD" };
            var exchangeRate = 4.5m; // Example exchange rate

            // Act
            var result = await _przychodService.ObliczPrzewidywanyPrzychod(przychodDto);

            // Assert
            Assert.Equal("USD", result.Waluta);
            Assert.Equal(844.44m, result.Przychod); // Expected PLN converted to EUR
        }
    }
}
