using ConnectionResiliency.DbContexts;
using ConnectionResiliency.DbTables;
using ConnectionResiliency.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace ConnectionResiliency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static int control = 0; 
        private readonly ExampleDbContext exampleDbContext;
        private readonly IException<Users> exception;
        private readonly IConnectionResiliency _connectionResiliency;
        private readonly IModel _model;
        public UsersController(ExampleDbContext _exampleDbContext,IException<Users> _exception,IConnectionResiliency connectionResiliency,IModel model)
        {
            exampleDbContext = _exampleDbContext;
            exception = _exception;
            _connectionResiliency = connectionResiliency;
            _model = model;
        }
        [HttpGet]
        public async Task<IActionResult> User()
        {

          Users users = new Users()
          {
                UsersName = "ufuk"
          };
          string output= await exception.Exceptions(_connectionResiliency.AddUsers,users);



            if(output==Outputs.FailtureRetryLimited) //burada hata döndügünden dolayı kuyruga'ya users'ı atar
            {
                    _model.ExchangeDeclare("ErrorAddUserExchange", ExchangeType.Direct, true);
                    _model.QueueDeclare("ErrorAddUserQueue", true, false);
                    _model.QueueBind("ErrorAddUserQueue", "ErrorAddUserExchange", "ErrorAddUserRoutingKey");

                
                var BasicProperties= _model.CreateBasicProperties();

                var Serialize= JsonSerializer.Serialize(users);
 


                _model.BasicPublish("ErrorAddUserExchange", "ErrorAddUserRoutingKey",BasicProperties,Encoding.UTF8.GetBytes(Serialize));
                return Ok("Kuyruğa değer gönderildi");
            }
            return Ok(output);
            
        }



      
    
    }
}
