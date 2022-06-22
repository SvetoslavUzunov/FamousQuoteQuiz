namespace FamousQuoteQuiz.Models.Quote;

using FamousQuoteQuiz.Models.Author;
using System.ComponentModel.DataAnnotations;
using static Data.DataConstants.Quote;

public class QuoteFormModel
{
    public int Id { get; set; }

    [StringLength(TextMaxLength, MinimumLength = TextMinLength)]
    public string Text { get; set; }

    [Display(Name = "Author")]
    public int AuthorId { get; set; }
    public string? AuthorName { get; set; }
    public IEnumerable<AuthorViewModel>? Authors { get; set; }
}
