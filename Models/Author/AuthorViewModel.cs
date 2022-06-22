namespace FamousQuoteQuiz.Models.Author;

using FamousQuoteQuiz.Models.Quote;

public class AuthorViewModel
{
    public int Id { get; set; }

    public string Name { get; set; }

    public IEnumerable<QuoteViewModel>? Quotes { get; set; } = new List<QuoteViewModel>();
}
