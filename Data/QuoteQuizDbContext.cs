namespace FamousQuoteQuiz.Data;

using FamousQuoteQuiz.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class QuoteQuizDbContext : IdentityDbContext
{
    public QuoteQuizDbContext(DbContextOptions<QuoteQuizDbContext> options)
        : base(options)
    {
    }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Quote> Quotes { get; set; }
}