namespace FamousQuoteQuiz.Data.Models;

using System.ComponentModel.DataAnnotations;
using static Data.DataConstants.Author;

public class Author
{
    public int Id { get; init; }

    [MaxLength(NameMaxLength)]
    public string Name { get; set; }

    public virtual IEnumerable<Quote>? Quotes { get; set; } = new List<Quote>();
}
