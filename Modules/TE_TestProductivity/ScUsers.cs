using TodoApi;
using TodoApi.Infrastructure.Persistance;
 using TodoApi.Infrastructure;
using TodoApi.Controllers.Common;
using TodoApi.Aplication;
using System;
using System.Collections.Generic;
namespace TestWebApi;

public class ScUsersTest
{

   
      private static TE_TestProductivityContext _context= new TE_TestProductivityContext();

        private static IGenericRepository<TodoApi.Infrastructure.Persistance.ScUser> _repository = new GenericRepository<TodoApi.Infrastructure.Persistance.ScUser>(_context);

         private readonly IGenericService <TodoApi.Infrastructure.Persistance.ScUser> _service = new GenericService<TodoApi.Infrastructure.Persistance.ScUser>(_repository);
    
    [Fact]
    public void getAlll()
    {
        
        List<ScUser> resultList = _service.GetAll().Result.ToList();
           
    }
    [Theory]
    [InlineData(1)]
    [InlineData(4)]
    public void getById(int id)
    {
      
     ScUser result = _service.Get(id).Result;
    }
    [Fact]
    public void create()
    {
        ScUser user = new ScUser();
        user.FkmesUserId = 1;
        user.EmployeeNumer = 1;
        user.Ntuser = "test";
        user.FirstName = "test";
        user.LastName = "test";
        user.SecondLastName = "test";
        user.Email = "test";
        user.FkroleId = 1;
        user.Available = true;        
        user.CreatedAt = DateTime.Now;
        user.UpdatedAt = DateTime.Now;    
        user.Available = true;
        ScUser result = _service.Create(user).Result;
    }
}