namespace FamousQuoteQuiz.Services.Quotes;

using FamousQuoteQuiz.Data;
using FamousQuoteQuiz.Data.Models;
using FamousQuoteQuiz.Models.Quote;

public class QuoteService : IQuoteService
{
    private readonly QuoteQuizDbContext data;

    public QuoteService(QuoteQuizDbContext data)
    {
        this.data = data;
    }

    public int Create(string text, int authorId)
    {
        var quoteData = new Quote
        {
            Text = text,
            AuthorId = authorId
        };

        data.Quotes.Add(quoteData);
        data.SaveChanges();

        return quoteData.Id;
    }

    public bool Edit(int id, string text, int authorId)
    {
        var quoteData = data.Quotes.Find(id);
        if (quoteData == null)
        {
            return false;
        }

        quoteData.Text = text;
        quoteData.AuthorId = authorId;

        data.SaveChanges();

        return true;
    }

    public void Delete(int id)
    {
        var quote = data.Quotes.Find(id);
        if (quote != null)
        {
            data.Quotes.Remove(quote);
            data.SaveChanges();
        }
    }
}
