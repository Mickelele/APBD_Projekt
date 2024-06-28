using APBD_Projekt.Models;
using APBD_Projekt.Models.DTO_s;
using APBD_Projekt.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using APBD_Projekt.Context;
using Xunit;

namespace ProjektTests
{
    
    
    public class CompanyServiceTests
    {
        private Mock<CustomerDbContext> _mock;
        private CompanyService _companyService;
        
        public CompanyServiceTests()
        {
            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;
            
            
            _mock = new Mock<CustomerDbContext>(options);
            _companyService = new CompanyService(_mock.Object);
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

            if (_companyService == null)
            {
                throw new InvalidOperationException($"Tragedia");
            }
            
            await _companyService.WstawFirme(newCompany);
            
            
        }
        
        
    }
}