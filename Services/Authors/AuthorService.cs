namespace FamousQuoteQuiz.Services.Authors;

using FamousQuoteQuiz.Data;
using FamousQuoteQuiz.Data.Models;

public class AuthorService : IAuthorService
{
    private readonly QuoteQuizDbContext data;

    public AuthorService(QuoteQuizDbContext data)
    {
        this.data = data;
    }

    public int Create(string name)
    {
        var authorData = new Author
        {
            Name = name
        };

        data.Authors.Add(authorData);
        data.SaveChanges();

        return authorData.Id;
    }

    public bool Edit(int id, string name)
    {
        var authorData = data.Authors.Find(id);
        if (authorData == null)
        {
            return false;
        }

        authorData.Name = name;

        data.SaveChanges();

        return true; 
    }

    public void Delete(int id)
    {
        var author = data.Authors.Find(id);

        if (author != null)
        {
            data.Authors.Remove(author);
            data.SaveChanges();
        }
    }
}
