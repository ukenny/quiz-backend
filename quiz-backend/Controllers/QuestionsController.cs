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
        private readonly QuizContext _quizContext;

        public QuestionsController(QuizContext quizContext)
        {
            _quizContext = quizContext;
        }
        [HttpGet]
        public IEnumerable<Question> Get()
        {
            return _quizContext.Questions;
        }
        [HttpPost]
        public string Post([FromBody]Question question)
        {
            _quizContext.Questions.Add(question);
            _quizContext.SaveChanges();
            return JsonConvert.SerializeObject(question);
        }
    }
}