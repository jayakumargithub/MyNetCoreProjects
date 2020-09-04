using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NSwaggerTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        public HelloMessage Get(string name)
        {
            return new HelloMessage { Message = $"Hello {name}"};
        } 

    }

    public class HelloMessage
    {
        public string Message { get; set; }
    }
}
