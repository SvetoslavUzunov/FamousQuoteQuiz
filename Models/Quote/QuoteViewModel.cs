namespace FamousQuoteQuiz.Models.Quote;

using Data.Models;

public class QuoteViewModel
{
    public int Id { get; set; }

    public string Text { get; set; }

    public Author? Author { get; set; }
}
