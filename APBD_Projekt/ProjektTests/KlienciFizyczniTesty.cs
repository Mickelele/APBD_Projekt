using APBD_Projekt.Context;
using APBD_Projekt.Models;
using APBD_Projekt.Models.DTO_s;
using APBD_Projekt.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ProjektTests
{
    public class KlienciFizyczniTesty
    {
        private readonly KlientFizycznyService _klientFizycznyService;
        private readonly CustomerDbContext _context;

        public KlienciFizyczniTesty()
        {
            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            _context = new CustomerDbContext(options);
            _klientFizycznyService = new KlientFizycznyService(_context);
        }

        [Fact]
        public async Task WstawKlientaFizycznego_DodajeNowegoKlienta()
        {
            var newClient = new KlientFizycznyDTO()
            {
                Imie = "Jan",
                Nazwisko = "Kowalski",
                Adres = "Testowa 1",
                Email = "jan@test.com",
                NrTelefonu = "123456789",
                PESEL = "12345678901"
            };

            await _klientFizycznyService.WstawKlientaFizycznego(newClient);

            var addedClient = await _context.KlienciFizyczni.FirstOrDefaultAsync(c => c.PESEL == "12345678901");
            Assert.NotNull(addedClient);
            Assert.Equal("Jan", addedClient.Imie);
            Assert.Equal("Kowalski", addedClient.Nazwisko);
            Assert.Equal("Testowa 1", addedClient.Adres);
            Assert.Equal("jan@test.com", addedClient.Email);
            Assert.Equal("123456789", addedClient.NrTelefonu);
            Assert.Equal("12345678901", addedClient.PESEL);
        }

        

        [Fact]
        public async Task UsunKlientaFizycznego_UstawiaCzyUsunietyNaTrue()
        {
            var newClient = new KlientFizyczny()
            {
                Imie = "Jan",
                Nazwisko = "Kowalski",
                Adres = "Testowa 1",
                Email = "jan@test.com",
                NrTelefonu = "123456789",
                PESEL = "12345678901",
                czyUsuniety = false
            };

            _context.KlienciFizyczni.Add(newClient);
            await _context.SaveChangesAsync();

            await _klientFizycznyService.UsunKlientaFizycznego(newClient.KlientID);

            var updatedClient = await _context.KlienciFizyczni.FirstOrDefaultAsync(c => c.KlientID == newClient.KlientID);
            Assert.True(updatedClient.czyUsuniety);
        }

        [Fact]
        public async Task AktualizujKlientaFizycznego_AktualizujeDaneKlienta()
        {
            var newClient = new KlientFizyczny()
            {
                Imie = "Jan",
                Nazwisko = "Kowalski",
                Adres = "Testowa 1",
                Email = "jan@test.com",
                NrTelefonu = "123456789",
                PESEL = "12345678901",
                czyUsuniety = false
            };

            _context.KlienciFizyczni.Add(newClient);
            await _context.SaveChangesAsync();

            var updateDto = new KlientFizycznyDTOUpdate()
            {
                Imie = "Piotr",
                Nazwisko = "Nowak",
                Adres = "Nowa 2",
                Email = "piotr@test.com",
                NrTelefonu = "987654321"
            };

            //await _klientFizycznyService.AktualizujKlientaFizycznego(updateDto);

            var updatedClient = await _context.KlienciFizyczni.FirstOrDefaultAsync(c => c.KlientID == newClient.KlientID);
            Assert.Equal("Piotr", updatedClient.Imie);
            Assert.Equal("Nowak", updatedClient.Nazwisko);
            Assert.Equal("Nowa 2", updatedClient.Adres);
            Assert.Equal("piotr@test.com", updatedClient.Email);
            Assert.Equal("987654321", updatedClient.NrTelefonu);
        }

        [Fact]
        public async Task SprawdzIstnienieIZaktualizuj_AktualizujeKlientaJesliIstnieje()
        {
            var existingClient = new KlientFizyczny()
            {
                Imie = "Jan",
                Nazwisko = "Kowalski",
                Adres = "Testowa 1",
                Email = "jan@test.com",
                NrTelefonu = "123456789",
                PESEL = "12345678901",
                czyUsuniety = true
            };

            _context.KlienciFizyczni.Add(existingClient);
            await _context.SaveChangesAsync();

            var clientDto = new KlientFizycznyDTO()
            {
                Imie = "Jan",
                Nazwisko = "Kowalski",
                Adres = "Testowa 1",
                Email = "jan@test.com",
                NrTelefonu = "123456789",
                PESEL = "12345678901"
            };

            var result = await _klientFizycznyService.SprawdzIstnienieIZaktualizuj(clientDto);

            Assert.Equal(2, result);

            var updatedClient = await _context.KlienciFizyczni.FirstOrDefaultAsync(c => c.PESEL == "12345678901");
            Assert.False(updatedClient.czyUsuniety);
        }
        
        
    }
}
