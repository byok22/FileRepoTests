using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FilesApi.Domain.Entities;
using FilesApi.Infrastructure.Persistance.DogBoardRepo;
using FilesApi.WebApi.Controllers.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;


namespace TestWebApi.Modules
{
    public class ExternalConTests
    {
       private  IDbConnect _dbConnectA;

       //   private ProductivityRepo _repository;

         

        private DogBoardRepo _dogBoardRepo;

        private readonly ProductivityServices _service ;
        public ExternalConTests()
        {
             // Configurar el servicio IDbConnectA en el contenedor de servicios
            var services = new ServiceCollection();
            services.AddScoped<IDbConnect, DbConnectA>();
            
            // Construir el proveedor de servicios
            var serviceProvider = services.BuildServiceProvider();

            // Obtener una instancia de IDbConnectA desde el proveedor de servicios
           // _dbConnectA =  new DbConnectA("external") ;

             _dogBoardRepo = new DogBoardRepo();

           //  _repository = new ProductivityRepo(_storeProceduresRepo);

            // this._service = new ProductivityServices(_repository);
        }
      
        
        
        [Fact]
        public async Task up_GetBlockedUnitsReportTest()
        {
            
            DateTime DateStart = new DateTime(2023, 8, 1);
            DateTime DateEnd = new DateTime(2023, 8, 11);     
            int CustomerID = 34;

           
            var items =  _dogBoardRepo.GetBlockedUnitsReportRowData(DateStart,DateEnd, CustomerID);                

            List<DogBoardControl> result = items;

            // Convertir el objeto a JSON utilizando Newtonsoft.Json
            string jsonData = JsonConvert.SerializeObject(result);                    
                                   
        }
    }
}