using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using FilesApi.Domain.Entities.Configs;
using Microsoft.Data.SqlClient;
using FilesApi.Infrastructure.Persistance.ConfigRepo;
using Microsoft.Extensions.DependencyInjection;
using FilesApi.Domain.Entities.Catalogs;
using FilesApi.Infrastructure.Entities;
using FilesApi.Domain.Entities;

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

       [Fact]
        public async Task CRUD_DepartmentTestAsync()
        {
            Department department = new Department();    
            
            department.Available= true;
            department.Description="New Department";

            


             DepartmentResult addResult = await _infraRepo.Up_AddDepartment(department);

            Assert.NotNull(addResult);
            Assert.True(!string.IsNullOrEmpty(addResult.Message));
            Assert.True(addResult.PKDepartment > 0);

            var addedPKDepartment=addResult.PKDepartment;

            // Obtener el departamento recién agregado
            Department addedDepartment = await _infraRepo.Up_GetDepartmentByPK(addedPKDepartment);

            // Asegúrate de que se haya encontrado el departamento
            Assert.NotNull(addedDepartment);

            // Actualizar el departamento
            string updatedDescription = "Departamento Actualizado";
            bool updatedAvailable = false;

            string updateResult = await _infraRepo.Up_UpdateDepartment(addedPKDepartment, updatedDescription, updatedAvailable);

            // Asegúrate de que la actualización sea exitosa
            Assert.Equal("Department updated successfully", updateResult);

            // Obtener el departamento actualizado
            Department updatedDepartment = await _infraRepo.Up_GetDepartmentByPK(addedPKDepartment);

            // Asegúrate de que el departamento se haya actualizado correctamente
            Assert.NotNull(updatedDepartment);
            Assert.Equal(updatedDescription, updatedDepartment.Description);
            Assert.False(updatedDepartment.Available); // Modificado para verificar que no está disponible

            // Eliminar el departamento
            string deleteResult = await _infraRepo.Up_DeleteDepartment(addedPKDepartment);

            // Asegúrate de que la eliminación sea exitosa
            Assert.Equal("Department deleted successfully", deleteResult);

            // Intentar obtener el departamento eliminado
            Department deletedDepartment = await _infraRepo.Up_GetDepartmentByPK(addedPKDepartment);

            // Asegúrate de que el departamento se haya eliminado (debe ser null)
            Assert.Null(deletedDepartment);
        }

        
        [Fact]
        public async Task AddTestRouteStepByDepartmentTestAsync()
        {
            int fkDepartment = 1; // Reemplaza con el valor deseado
            int fkTestRouteStep = 2; // Reemplaza con el valor deseado

            ResultMsgID result = await _infraRepo.Up_AddTestRouteStepByDepartment(fkDepartment, fkTestRouteStep);

            Assert.NotNull(result);
            Assert.Equal("TestRouteStepByDepartment added successfully", result.Message);
            Assert.True(result.Id > 0);
        }

        [Fact]
        public async Task UpdateTestRouteStepByDepartmentTestAsync()
        {
            int pkTestRouteStepByDepartment = 1; // Reemplaza con el valor del registro que deseas actualizar
            int fkDepartment = 3; // Reemplaza con el nuevo valor
            int fkTestRouteStep = 4; // Reemplaza con el nuevo valor

            ResultMsgID result = await _infraRepo.Up_UpdateTestRouteStepByDepartment(pkTestRouteStepByDepartment, fkDepartment, fkTestRouteStep);

            Assert.Equal("TestRouteStepByDepartment updated successfully", result.Message);
            Assert.Equal(pkTestRouteStepByDepartment, result.Id);
        }

        [Fact]
        public async Task DeleteTestRouteStepByDepartmentTestAsync()
        {
            int pkTestRouteStepByDepartment = 1; // Reemplaza con el valor del registro que deseas eliminar

            ResultMsgID result = await _infraRepo.Up_DeleteTestRouteStepByDepartment(pkTestRouteStepByDepartment);

            Assert.Equal("TestRouteStepByDepartment deleted successfully", result.Message);
            Assert.Equal(pkTestRouteStepByDepartment, result.Id);
        }



        
    }
}