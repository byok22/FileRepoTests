using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FilesApi.Domain.Entities.Configs;
using Microsoft.Data.SqlClient;
using FilesApi.Infrastructure.Persistance.ConfigRepo;
using Microsoft.Extensions.DependencyInjection;

namespace TestWebApi.Modules.ConfigsTest
{

    public class ConfigTests
    {
        private readonly ConfigRepo _infraRepo;       
       private readonly IDbConnect _dbConnectA;

       public ConfigTests()
       {

          var services = new ServiceCollection();
            services.AddScoped<IDbConnect, DbConnectA>();

              // Construir el proveedor de servicios
            var serviceProvider = services.BuildServiceProvider();

            // Obtener una instancia de IDbConnectA desde el proveedor de servicios
            _dbConnectA = serviceProvider.GetRequiredService<IDbConnect>();

            _infraRepo = new  ConfigRepo(_dbConnectA);
           
        
       }
        [Fact]
        public async Task GetUsersTestAsync()
        {
            
           
            List<User> users = await _infraRepo.Up_Get_SC_Users();

            // Assert
            Assert.NotNull(users);
            Assert.NotEmpty(users);
            //Assert.Equal(2, selects.Count);

            foreach (var item in users)
            {
                Assert.False(string.IsNullOrEmpty(item.Email));
                Assert.False(string.IsNullOrEmpty(item.FullName));
            }
        }

        [Fact]
        public async Task InsertUsersTestAsync()
        {
            
           
            UserDb user = new UserDb();

            user.EmployeeNumber = 3524661;
            user.NTUser = "3524661";
            user.FirstName = "Borrame";
            user.LastName = "BorrameApellido";
            user.SecondLastName = "Ninguno";
            user.Email = "borrame@gmail.com";
            user.Available = true;
            user.FKRole = 1;
            user.Role = "Master Admin" ;  

            int result = await _infraRepo.Up_Insert_SC_Users(user);      
            
          
        }

         [Fact]
        public async Task UpdateUsersTestAsync()
        {                       
            UserDb user = new UserDb();
            user.EmployeeNumber = 352466;
            user.NTUser = "352466";
            user.FirstName = "NoBorrame";
            user.LastName = "NoBorrameApellido";
            user.SecondLastName = "Ninguno2";
            user.Email = "borrame2@gmail.com";
            user.Available = false;
            user.FKRole = 1;
            user.Role = "Master Admin" ;  
            bool result = await _infraRepo.Up_Update_SC_Users(user);     
        }

         [Fact]
        public async Task getUserTest()
        {                       
            
            var NTUser = "3524661";         
          
            var result = await _infraRepo.Get_SC_Users_By_NTUser(NTUser);     
        }


        [Fact]
        public async Task GetCustomerTestAsync()
        {
                       
            List<Customer> customers = await _infraRepo.Up_Get_Customers();

            // Assert
            Assert.NotNull(customers);
            Assert.NotEmpty(customers);
            //Assert.Equal(2, selects.Count);

          
        }

        [Fact]
        public async Task GetSubCustomerTestAsync()
        {
            
           
            List<SubCustomer> subcustomers = await _infraRepo.Up_Get_SubCustomers();

            // Assert
            Assert.NotNull(subcustomers);
            Assert.NotEmpty(subcustomers);
            //Assert.Equal(2, selects.Count);

           
        }


        
    }
}