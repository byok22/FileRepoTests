using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;
using FilesApi.Domain.Entities;
using FilesApi.Infrastructure.Persistance.StoreProcedureRepo;
using FilesApi.Application.ProductivityRepository;
using FilesApi.WebApi.Controllers.Services;

namespace TestWebApi
{
   
    public class DatabaseTests
    {
         private  IDbConnect _dbConnectA;

          private ProductivityRepo _repository;

         private StoreProceduresRepo _storeProceduresRepo;

         private readonly ProductivityServices _service ;
        public DatabaseTests()
        {
             // Configurar el servicio IDbConnectA en el contenedor de servicios
            var services = new ServiceCollection();
            services.AddScoped<IDbConnect, DbConnectA>();
            
            // Construir el proveedor de servicios
            var serviceProvider = services.BuildServiceProvider();

            // Obtener una instancia de IDbConnectA desde el proveedor de servicios
            _dbConnectA = serviceProvider.GetRequiredService<IDbConnect>();

             _storeProceduresRepo = new StoreProceduresRepo(_dbConnectA);

             _repository = new ProductivityRepo(_storeProceduresRepo);

             this._service = new ProductivityServices(_repository);
        }
        private const string ConnectionTE_MESApi = "Data Source=MXGDLM0OTSQLV2A;Initial Catalog=TE_MESApi;Persist Security Info=True;User ID=EngSWUser;Password=EnGswUs3r19!;TrustServerCertificate=true;";
        
        
        [Fact]
        public async Task up_GetDebugAndRepairByHourAsync()
        {
           DateTime pDatetime = new DateTime(2023, 6, 1);
            int pShift = 2; // Cambiar el valor del shift según sea necesario
            int pBuildingID = 0;
            int pCustomerID = 52;
            int pAreaID = 0;
            int pPCBAFamilyID = 0;
            int PositionType = 0;
            
            var items = await _service.GetDebugRepairEmployeesHour(new DateTime(2023,6,1),1,0,81,0,0,0);                

            List<DebugRepairEmployees> result = items;

            // Convertir el objeto a JSON utilizando Newtonsoft.Json
            string jsonData = JsonConvert.SerializeObject(result);                    
                                   
        }

        [Fact]
        public async Task up_GetDebugAndRepairByDayAsync()
        {
            DateTime pDatetime = new DateTime(2023, 5, 17);
            //int pShift = 2; // Cambiar el valor del shift según sea necesario
            int pBuildingID = 0;
            int pCustomerID = 29;
            int pAreaID = 0;
            int pPCBAFamilyID = 0;
            int PositionType = 0;
            
          //  var items =  _storeProceduresRepo.Up_GetDebugAndRepairByDay(pDatetime,pBuildingID,pCustomerID,pAreaID,pPCBAFamilyID,PositionType);                

            var items = await  _service.GetDebugRepairEmployeesDay(pDatetime,pBuildingID,pCustomerID,pAreaID,pPCBAFamilyID,PositionType);                

            List<DebugRepairEmployees> result = items;

            // Convertir el objeto a JSON utilizando Newtonsoft.Json
            string jsonData = JsonConvert.SerializeObject(result);                    
                                   
        }

        [Fact]
        public async Task up_GetDebugAndRepairByCustomer()
        {
            DateTime pDatetime = new DateTime(2023, 5, 17);
            //int pShift = 2; // Cambiar el valor del shift según sea necesario
            int pBuildingID = 0;
            int debugRepairType=1;
            
            var items =  _storeProceduresRepo.up_GetDebugAndRepairByCustomer(pDatetime,pBuildingID,debugRepairType);                

           // var items = await  _service.GetDebugRepairEmployeesDay(pDatetime,pBuildingID,pCustomerID,pAreaID,pPCBAFamilyID,PositionType);                

            List<DebugRepairCustomers> result = items;

            // Convertir el objeto a JSON utilizando Newtonsoft.Json
            string jsonData = JsonConvert.SerializeObject(result);                    
                                   
        }
                      
        [Fact]
        public void MesApiConectionTest()
        {           
            using (var connection = _dbConnectA.GetConnection())
            {
                // Intenta abrir la conexión
               

                // Verifica si la conexión está abierta
                Assert.Equal(ConnectionState.Open, connection.State);

                // Puedes agregar más pruebas relacionadas con la conexión a la base de datos
                // Por ejemplo, puedes ejecutar consultas SQL y verificar los resultados esperados.
            }
        }
    }
}
