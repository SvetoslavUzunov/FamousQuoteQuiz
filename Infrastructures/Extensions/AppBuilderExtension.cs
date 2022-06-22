namespace FamousQuoteQuiz.Infrastructures.Extensions;

using Microsoft.EntityFrameworkCore;
using FamousQuoteQuiz.Data;
using FamousQuoteQuiz.Data.Models;

public static class AppBuilderExtension
{
    public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
    {
        using var scopedServices = app.ApplicationServices.CreateScope();
        var data = scopedServices.ServiceProvider.GetService<QuoteQuizDbContext>();

        data.Database.Migrate();
        SeedAuthors(data);
        SeedQuotes(data);

        return app;
    }

    private static void SeedAuthors(QuoteQuizDbContext data)
    {
        if (data.Authors.Any())
        {
            return;
        }

        data.Authors.AddRange(new[]
        {
            new Author { Name = "Albert Einstein" },
            new Author { Name = "Bill Gates" },
            new Author { Name = "Steve Jobs" }
        });

        data.SaveChanges();
    }

    private static void SeedQuotes(QuoteQuizDbContext data)
    {
        if (data.Quotes.Any())
        {
            return;
        }

        data.Quotes.AddRange(new[]
        {
            new Quote { Text = "Life is like riding a bicycle. To keep your balance, you must keep moving", AuthorId = 1},
            new Quote { Text = "A computer in every desk, and in every home", AuthorId = 2},
            new Quote { Text = "Your time is limited, so don't waste it living someone else's life", AuthorId = 3}
        });

        data.SaveChanges();
    }
}
