namespace FamousQuoteQuiz.Services.Quotes;

public interface IQuoteService
{
    int Create(string text, int authorId);

    bool Edit(int id, string text, int authorId);

    void Delete(int id);
}
