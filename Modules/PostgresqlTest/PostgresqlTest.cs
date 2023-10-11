using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FilesApi.Infrastructure.Persistance.JemstsGdlRepo;
using Microsoft.Extensions.DependencyInjection;

namespace TestWebApi.Modules.PostgresqlTest
{
    public class PostgresqlTest
    {

        private  IDbConnect _dbConnectA;

       //   private ProductivityRepo _repository;         
        private JemstsGdlRepo _InfraRepo;

        public PostgresqlTest()
        {
             // Configurar el servicio IDbConnectA en el contenedor de servicios
            var services = new ServiceCollection();
            services.AddScoped<IDbConnect, DbConnectA>();
            
            // Construir el proveedor de servicios
          //  var serviceProvider = services.BuildServiceProvider();

            // Obtener una instancia de IDbConnectA desde el proveedor de servicios
            _dbConnectA =  new DbConnectA("external") ;

             _InfraRepo = new JemstsGdlRepo();

           //  _repository = new ProductivityRepo(_storeProceduresRepo);

            // this._service = new ProductivityServices(_repository);
        }

        [Fact]
        public async Task getUserTest()
        {                       
            
           
          
            var result =  _InfraRepo.healDB();     
        }

         [Fact]
        public async Task GetParametricsInfoTest()
        {                       
            
           
          
            var result =  _InfraRepo.GetParametricsInfo(123);     
        }

        
    }
}