using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpGet("{quizId}")]
        public IActionResult Get([FromRoute]int quizId)
        {
            var isValidId = _quizContext.Quiz.SingleOrDefaultAsync(quiz => quiz.ID == quizId) != null;
            if (!isValidId)
            {
                return NotFound();
            }
            return Ok(_quizContext.Questions.Where(questions => questions.QuizID == quizId).ToListAsync().Result);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Question question)
        {
            _quizContext.Questions.Add(question);
            await _quizContext.SaveChangesAsync();
            return Ok(question);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Question question)
        {
            if (id != question.ID)
            {
                return BadRequest();
            }
            _quizContext.Entry(question).State = EntityState.Modified;

            await _quizContext.SaveChangesAsync();

            return Ok(question);
        }
    }
}