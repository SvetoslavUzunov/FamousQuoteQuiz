namespace FamousQuoteQuiz.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FamousQuoteQuiz.Data;
using FamousQuoteQuiz.Models.Author;
using FamousQuoteQuiz.Models.Quote;
using FamousQuoteQuiz.Services.Authors;
using static GlobalConstants;

public class AuthorController : Controller
{
    private readonly QuoteQuizDbContext data;
    private readonly IAuthorService authors;

    public AuthorController(QuoteQuizDbContext data, IAuthorService authors)
    {
        this.data = data;
        this.authors = authors;
    }

    [Authorize]
    public IActionResult Add()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public IActionResult Add(AuthorFormModel author)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        authors.Create(author.Name);

        TempData[GlobalMessageKey] = "Author was added successfully!";

        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    public IActionResult All()
    {
        var authors = data.Authors
            .Select(a => new AuthorViewModel
            {
                Id = a.Id,
                Name = a.Name,
            })
            .ToList();

        authors.ForEach(a => a.Quotes = GetQuotes());

        return View(authors);
    }

    [Authorize]
    public IActionResult QuotesByAuthor(int id)
    {
        var quotes = data.Quotes
            .Where(q => q.AuthorId == id)
            .Select(q => new QuoteViewModel
            {
                Id = q.Id,
                Text = q.Text,
                Author = q.Author
            })
            .ToList();

        return View(quotes);
    }

    [Authorize]
    public IActionResult Edit(int id)
    {
        var author = data.Authors
            .Select(a => new AuthorFormModel
            {
                Id = a.Id,
                Name = a.Name
            })
            .FirstOrDefault(a => a.Id == id);

        return View(author);
    }

    [Authorize]
    [HttpPost]
    public IActionResult Edit(int id, AuthorViewModel author)
    {
        if (!ModelState.IsValid)
        {
            return View(author);
        }

        authors.Edit(id, author.Name);

        TempData[GlobalMessageKey] = "Author was successfully edited!";

        return RedirectToAction(nameof(All));
    }

    [Authorize]
    public IActionResult Delete(int id)
    {
        authors.Delete(id);

        TempData[GlobalMessageKey] = "Author was successfully deleted!";

        return RedirectToAction(nameof(All));
    }

    private IEnumerable<QuoteViewModel> GetQuotes()
       => data.Quotes.Select(q => new QuoteViewModel
       {
           Id = q.Id,
           Text = q.Text,
           Author = q.Author
       })
       .ToList();
}
