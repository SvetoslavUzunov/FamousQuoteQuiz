namespace FamousQuoteQuiz.Models.Author;

using System.ComponentModel.DataAnnotations;
using static Data.DataConstants.Author;

public class AuthorFormModel
{
    public int Id { get; init; }

    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string Name { get; set; }
}
