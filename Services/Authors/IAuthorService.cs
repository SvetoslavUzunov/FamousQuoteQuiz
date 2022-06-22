namespace FamousQuoteQuiz.Services.Authors;

public interface IAuthorService
{
    int Create(string name);

    bool Edit(int id, string name);

    void Delete(int id);
}
