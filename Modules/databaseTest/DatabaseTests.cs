using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;
using FilesApi.Domain.Entities;
using FilesApi.Infrastructure.Persistansce.StoreProcedureRepo;

namespace TestWebApi
{
   
    public class DatabaseTests
    {
         private readonly IDbConnect _dbConnectA;

         private StoreProceduresRepo _storeProceduresRepo;
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
        }
        private const string ConnectionTE_MESApi = "Data Source=MXGDLM0OTSQLV2A;Initial Catalog=TE_MESApi;Persist Security Info=True;User ID=EngSWUser;Password=EnGswUs3r19!;TrustServerCertificate=true;";
        
        
        [Fact]
        public void up_GetDebugAndRepairByHour()
        {
           DateTime pDatetime = new DateTime(2023, 6, 1);
            int pShift = 2; // Cambiar el valor del shift según sea necesario
            int pBuildingID = 0;
            int pCustomerID = 52;
            int pAreaID = 0;
            int pPCBAFamilyID = 0;
            int PositionType = 0;
            
            var items = _storeProceduresRepo.up_GetDebugAndRepairByHour(new DateTime(2023,6,1),1,0,81,0,0,0);                

            List<DebugRepairEmployeesHour> result = items;

            // Convertir el objeto a JSON utilizando Newtonsoft.Json
            string jsonData = JsonConvert.SerializeObject(result);

            // Deserializar el JSON a un objeto Result
            DebugRepairEmployeesHour deserializedResult = JsonConvert.DeserializeObject<DebugRepairEmployeesHour>(jsonData);

            // Acceder a los valores de los elementos de datos
            
            Console.WriteLine("Number: " + deserializedResult.Number);
            Console.WriteLine("Name: " + deserializedResult.Name);
            Console.WriteLine("Position: " + deserializedResult.Position);
            Console.WriteLine("ZoneName: " + deserializedResult.ZoneName);
            Console.WriteLine("Shift: " + deserializedResult.Shift);
            Console.WriteLine("HourlyGoal: " + deserializedResult.HourlyGoal);
            Console.WriteLine("HourlyCounts:");
            foreach (KeyValuePair<int, int> hourlyCount in deserializedResult.HourlyCounts)
            {
                Console.WriteLine(hourlyCount.Key + ": " + hourlyCount.Value);
            }
            Console.WriteLine("GrandTotal: " + deserializedResult.GrandTotal);
            Console.WriteLine("Productivity: " + deserializedResult.Productivity);
            Console.WriteLine("Effy: " + deserializedResult.Effy);
            Console.WriteLine();
            

            Console.WriteLine(jsonData);
                                   
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
