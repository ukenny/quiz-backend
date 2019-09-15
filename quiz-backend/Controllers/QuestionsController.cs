using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using quiz_backend.Models;

namespace quiz_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Question> Get()
        {
            return new Question[] {
                new Question { Text = "Hello" },
                new Question { Text = "There" },
                new Question { Text = "Everyone" }
            };
        }
        [HttpPost]
        public string Post([FromBody]Question question)
        {
            return JsonConvert.SerializeObject(question);
        }
    }
}