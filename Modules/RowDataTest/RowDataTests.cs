using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;
using FilesApi.Domain.Entities;
using FilesApi.Infrastructure.Persistance.StoreProcedureRepo;
using FilesApi.Infrastructure.Persistance.RowDataRepo;
using FilesApi.Application.ProductivityRepository;
using FilesApi.WebApi.Controllers.Services;

namespace TestWebApi
{
    public class RowDataTests
    {
         private  IDbConnect _dbConnectA;

       //   private ProductivityRepo _repository;

         

         private RowDataRepo _rowDataRepo;

         private readonly ProductivityServices _service ;
        public RowDataTests()
        {
             // Configurar el servicio IDbConnectA en el contenedor de servicios
            var services = new ServiceCollection();
            services.AddScoped<IDbConnect, DbConnectA>();
            
            // Construir el proveedor de servicios
            var serviceProvider = services.BuildServiceProvider();

            // Obtener una instancia de IDbConnectA desde el proveedor de servicios
            _dbConnectA = serviceProvider.GetRequiredService<IDbConnect>();

             _rowDataRepo = new RowDataRepo(_dbConnectA);

           //  _repository = new ProductivityRepo(_storeProceduresRepo);

            // this._service = new ProductivityServices(_repository);
        }
        private const string ConnectionTE_MESApi = "Data Source=MXGDLM0OTSQLV2A;Initial Catalog=TE_MESApi;Persist Security Info=True;User ID=EngSWUser;Password=EnGswUs3r19!;TrustServerCertificate=true;";
        
        
        [Fact]
        public async Task up_GetDebug_RepairRowDataTest()
        {
            
            DateTime DateStart = new DateTime(2023, 7, 17);
            DateTime DateEnd = new DateTime(2023, 7, 17);
            int BuildingID = 0;
            int CustomerID = 29;

           
            var items =  _rowDataRepo.Up_GetDebug_RepairRowData(DateStart,DateEnd, BuildingID, CustomerID);                

            List<DebugRepairRowData> result = items;

            // Convertir el objeto a JSON utilizando Newtonsoft.Json
            string jsonData = JsonConvert.SerializeObject(result);                    
                                   
        }
    }
}