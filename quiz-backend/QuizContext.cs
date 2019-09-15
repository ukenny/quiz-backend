using Microsoft.EntityFrameworkCore;
using quiz_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quiz_backend
{
    public class QuizContext : DbContext
    {
        public QuizContext(DbContextOptions<QuizContext> options) : base(options) { }

        public DbSet<Question> Questions { get; set; }

    }
}
