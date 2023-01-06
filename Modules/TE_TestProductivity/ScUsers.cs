using TodoApi;

namespace TestWebApi;

public class ScUsers
{
     [Fact]
    public void getAlll()
    {
        IGenericRepository<ScUser> _repository = new GenericRepository<ScUser>();
       
            var hola =  _repository.GetAll();
        
        
    }
}