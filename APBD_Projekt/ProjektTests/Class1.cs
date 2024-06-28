using APBD_Projekt.Models;
using APBD_Projekt.Models.DTO_s;
using APBD_Projekt.Services;
using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using APBD_Projekt.Context;

namespace ProjektTests
{
    public class CompanyServiceTests
    {
        private Mock<CustomerDbContext> _mock;
        private CompanyService _companyService;
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            // Initialize AutoFixture with AutoMoq customization
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            // Configure in-memory database for DbContext
            var options = new DbContextOptionsBuilder<CustomerDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique database name
                .Options;

            // Mock CustomerDbContext using the options
            _mock = new Mock<CustomerDbContext>(options);

            // Configure the CompanyService with the mocked CustomerDbContext
            _companyService = new CompanyService(_mock.Object);
        }

        [Test]
        public async Task WstawFirme_DodajeNowaFirme()
        {
            // Arrange
            var newCompany = _fixture.Create<FirmaDTO>();

            // Act
            await _companyService.WstawFirme(newCompany);

            // Assert
            _mock.Verify(x => x.Firmy.Add(It.IsAny<Firma>()), Times.Once);
            _mock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }
}