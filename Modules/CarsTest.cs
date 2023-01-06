using TodoApi.Models;

namespace TestWebApi;

public class CarsTest
{
  //  string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=TodoApi;Trusted_Connection=True;MultipleActiveResultSets=true";
     
    [Fact]
    public void Test1()
    {
        CarsContext _carsContext = new CarsContext();
        var cars = _carsContext.Cars.ToList();
        
    }
}
