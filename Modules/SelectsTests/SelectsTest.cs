using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;
using FilesApi.Domain.Entities;
using FilesApi.Infrastructure.Persistance.DropDownsRepo;
using FilesApi.Application.ProductivityRepository;
using FilesApi.WebApi.Controllers.Services;
using FilesApi.Application.DropDownsRepository;

namespace TestWebApi
{

    public class SelectsTest
    {
     


        private readonly DropDownsRepo _infraRepo;

       

       private readonly IDbConnect _dbConnectA;

       private readonly DropDownsRepository _appRepository;

       private readonly DropDownsService _service;


        public SelectsTest()
        {
              // Configurar el servicio IDbConnectA en el contenedor de servicios
            var services = new ServiceCollection();
            services.AddScoped<IDbConnect, DbConnectA>();

              // Construir el proveedor de servicios
            var serviceProvider = services.BuildServiceProvider();

            // Obtener una instancia de IDbConnectA desde el proveedor de servicios
            _dbConnectA = serviceProvider.GetRequiredService<IDbConnect>();

            _infraRepo = new DropDownsRepo(_dbConnectA);


            _appRepository = new DropDownsRepository(_infraRepo);

            _service = new DropDownsService(_appRepository);
           
        }

        [Fact]
        public async Task GetBuildingsTest()
        {
            // Act
            List<DropDown> buildingSelect = await _service.GetBuildings();

            // Assert
            Assert.NotNull(buildingSelect);
            Assert.NotEmpty(buildingSelect);
            //Assert.Equal(2, buildingSelect.Count);

            foreach (var item in buildingSelect)
            {
                Assert.False(string.IsNullOrEmpty(item.id));
                Assert.False(string.IsNullOrEmpty(item.text));
            }
        }

        [Fact]
        public async Task GetManufacturingAreaTest()
        {
            
            DateTime fecha = new DateTime(2023,7,5);
            int Shift = 1;
            int customerID = 5;
            string testRouteStep = "ALL";
            // Act
            //List<DropDown> selects = await _infraRepo.up_GetTestManufacturingArea_TestDashd_V2(fecha,Shift,customerID,testRouteStep);
            List<DropDown> selects = await _service.GetTestManufacturingArea(fecha,Shift,customerID,testRouteStep);

            // Assert
            Assert.NotNull(selects);
            Assert.NotEmpty(selects);
            //Assert.Equal(2, selects.Count);

            foreach (var item in selects)
            {
                Assert.False(string.IsNullOrEmpty(item.id));
                Assert.False(string.IsNullOrEmpty(item.text));
            }
        }

        [Fact]
        public async Task GetCustomersTest()
        {
            int idBuilding = 0;
            // Act
            List<DropDown> selects = await _service.GetCustomersByBuilding(idBuilding);

            // Assert
            Assert.NotNull(selects);
            Assert.NotEmpty(selects);
            //Assert.Equal(2, selects.Count);

            foreach (var item in selects)
            {
                Assert.False(string.IsNullOrEmpty(item.id));
                Assert.False(string.IsNullOrEmpty(item.text));
            }
        }
         [Fact]
        public async Task GetAreasTest()
        {
           
            // Act
            List<DropDown> selects = await _service.GetAreas();

            // Assert
            Assert.NotNull(selects);
            Assert.NotEmpty(selects);
            //Assert.Equal(2, selects.Count);

            foreach (var item in selects)
            {
                Assert.False(string.IsNullOrEmpty(item.id));
                Assert.False(string.IsNullOrEmpty(item.text));
            }
        }

        [Fact]
        public async Task GetPcbaFamiliesByCustomerTest()
        {
            int idCustomer=5;//Aclara
           
            // Act
            //List<DropDown> selects = await _infraRepo.Up_getPCBAFamiliesByCustomer(idCustomer);
            List<DropDown> selects = await _service.GetPCBAFamiliesByCustomer(idCustomer);

            // Assert
            Assert.NotNull(selects);
            Assert.NotEmpty(selects);
            //Assert.Equal(2, selects.Count);

            foreach (var item in selects)
            {
                Assert.False(string.IsNullOrEmpty(item.id));
                Assert.False(string.IsNullOrEmpty(item.text));
            }
        }

        [Fact]
        public async Task GetPositionsTest()
        {
           
            // Act
              //List<DropDown> selects = await _infraRepo.Up_GetPositionTypes();
            List<DropDown> selects = await _service.GetPositionTypes();

            // Assert
            Assert.NotNull(selects);
            Assert.NotEmpty(selects);
            //Assert.Equal(2, selects.Count);

            foreach (var item in selects)
            {
                Assert.False(string.IsNullOrEmpty(item.id));
                Assert.False(string.IsNullOrEmpty(item.text));
            }
        }

        [Fact]    
        public async Task GetShiftsTest()
        {
           
            // Act
              List<DropDown> selects = await _infraRepo.Up_GetShiftsAvailable();
            //List<DropDown> selects = await _service.GetPositionTypes();

            // Assert
            Assert.NotNull(selects);
            Assert.NotEmpty(selects);
            //Assert.Equal(2, selects.Count);

            foreach (var item in selects)
            {
                Assert.False(string.IsNullOrEmpty(item.id));
                Assert.False(string.IsNullOrEmpty(item.text));
            }
        }

        
    }
}
