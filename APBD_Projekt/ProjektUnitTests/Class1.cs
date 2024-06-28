using APBD_Projekt.Models;
using APBD_Projekt.Models.DTO_s;
using APBD_Projekt.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD_Projekt.Context;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace ProjektUnitTests
{
    public class CompanyServiceTests
    {
        [Fact]
        public async Task PokazFirmy_ReturnsAllFirmy()
        {
            // Arrange
            var testData = new List<Firma>
            {
                new Firma { FirmaID = 1, NazwaFirmy = "Firma A", Adres = "Adres A", Email = "a@firma.com", NrTelefonu = "123456789", KRS = "KRS123" },
                new Firma { FirmaID = 2, NazwaFirmy = "Firma B", Adres = "Adres B", Email = "b@firma.com", NrTelefonu = "987654321", KRS = "KRS456" }
            };

            var mockDbSet = new Mock<DbSet<Firma>>();
            mockDbSet.As<IQueryable<Firma>>().Setup(m => m.Provider).Returns(testData.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Firma>>().Setup(m => m.Expression).Returns(testData.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Firma>>().Setup(m => m.ElementType).Returns(testData.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Firma>>().Setup(m => m.GetEnumerator()).Returns(testData.GetEnumerator());

            var mockContext = new Mock<CustomerDbContext>();
            mockContext.Setup(c => c.Firmy).Returns(mockDbSet.Object);

            var service = new CompanyService(mockContext.Object);

            // Act
            var result = await service.PokazFirmy();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testData.Count, result.Count);
        }
        
    }
}