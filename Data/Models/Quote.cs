namespace FamousQuoteQuiz.Data.Models;

using System.ComponentModel.DataAnnotations;
using static Data.DataConstants.Quote;

public class Quote
{
    public int Id { get; init; }

    [MaxLength(TextMaxLength)]
    public string Text { get; set; }

    public int AuthorId { get; set; }

    public virtual Author Author { get; set; }
}
