using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;
using FilesApi.Domain.Entities;
using FilesApi.Infrastructure.Persistance;
using FilesApi.Infrastructure.Persistance.DropDownsRepo;

namespace TestWebApi
{
    public class SelectsTest
    {

        private readonly IDbConnect _dbConnectA;

        private DropDownsRepo _dropDownRepo;

        public SelectsTest()
        {

             // Configurar el servicio IDbConnectA en el contenedor de servicios
            var services = new ServiceCollection();
            services.AddScoped<IDbConnect, DbConnectA>();
            
            // Construir el proveedor de servicios
            var serviceProvider = services.BuildServiceProvider();

            // Obtener una instancia de IDbConnectA desde el proveedor de servicios
            _dbConnectA = serviceProvider.GetRequiredService<IDbConnect>();      


               _dropDownRepo = new DropDownsRepo(_dbConnectA);
      
        }
        [Fact]
        public async Task BuildingSelectsTest()
        {
            Assert.True(true);

            DropDown[] buildingSelect = new DropDown[]{
               new DropDown  { id ="1",
                    text="Prueba"                     
                },
                new DropDown {
                    id ="2",
                    text="Prueba2"

                }
            };
            string jsonData = JsonConvert.SerializeObject(buildingSelect);  
            Console.Write(jsonData);

        }

       

    }
}