using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FilesApi.Domain.Entities;
using FilesApi.Infrastructure.Persistance.StoreProcedureRepo;
using FilesApi.Application.ProductivityRepository;
using FilesApi.WebApi.Controllers.Services;
using FilesApi.Infrastructure.Persistance.MesTisRepo;
using Microsoft.Extensions.DependencyInjection;

namespace TestWebApi.Modules.WebServiceTest
{
    public class WebServiceTest
    {
        private StoreProceduresRepo _storeProceduresRepo;
        private  IDbConnect _dbConnectA;
        private MesTisRepo _mesTisRepo;
          public WebServiceTest()
          {
              var services = new ServiceCollection();

               services.AddScoped<IDbConnect, DbConnectA>();

              // Construir el proveedor de servicios
            var serviceProvider = services.BuildServiceProvider();



               _dbConnectA = serviceProvider.GetRequiredService<IDbConnect>();

             _storeProceduresRepo = new StoreProceduresRepo(_dbConnectA);

             _mesTisRepo = new MesTisRepo(_storeProceduresRepo);
            
          }
           

        [Fact]
        public void GetTestHistoryTest()
        {
           int customerID = 68;
           string serialNumber="JMX2249A22W";

           var testHistory =  _mesTisRepo.GetSerialHistoryDT(customerID,serialNumber);

           List<TestResultDetails> testDetails = new List<TestResultDetails>();

          
            foreach (System.Data.DataRow row in testHistory.Rows)
            {
                TestResultDetails fileRepositoryModelResult = new TestResultDetails();
                fileRepositoryModelResult.SerialNumber = serialNumber;
                fileRepositoryModelResult.StartTime = (string?)row[0];
                fileRepositoryModelResult.StopTime = (string?)row[1];
                fileRepositoryModelResult.TestStatus = (string?)row[2];
                fileRepositoryModelResult.MachineName = (string?)row[3];
                fileRepositoryModelResult.Operator = (string?)row[4];
                fileRepositoryModelResult.StepOrTestName = (string?)row[5];
                fileRepositoryModelResult.FailureLabel = (string?)row[6];
                fileRepositoryModelResult.FailureMessage = (string?)row[7];

                testDetails.Add(fileRepositoryModelResult);
               
            }
            
          
         

           
        }

         [Fact]
        public void GetOkTest()
        {
           int customerID = 43;
           string serialNumber="FFC233704805";

           string  AssemblyNumber="";

           string TesterName = "MY13500225FDL";
           string ProcessStep= "AOI_T";

           var testHistory =  _mesTisRepo.GetOkToTest(customerID,serialNumber, AssemblyNumber,TesterName, ProcessStep);

           
            
          
         

           
        }
    }
}