namespace FamousQuoteQuiz.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FamousQuoteQuiz.Data;
using FamousQuoteQuiz.Models.Author;
using FamousQuoteQuiz.Models.Quote;
using FamousQuoteQuiz.Services.Quotes;
using static GlobalConstants;

public class QuoteController : Controller
{
    private readonly QuoteQuizDbContext data;
    private readonly IQuoteService quotes;

    public QuoteController(QuoteQuizDbContext data, IQuoteService quotes)
    {
        this.data = data;
        this.quotes = quotes;
    }

    [Authorize]
    public IActionResult Add()
    {
        var quoteData = new QuoteFormModel
        {
            Authors = GetAuthors()
        };

        return View(quoteData);
    }

    [Authorize]
    [HttpPost]
    public IActionResult Add(QuoteFormModel quote)
    {
        if (!ModelState.IsValid)
        {
            quote.Authors = GetAuthors();

            return View(quote);
        }

        quotes.Create(quote.Text, quote.AuthorId);

        TempData[GlobalMessageKey] = "Quote was added successfully!";

        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    public IActionResult YesAnswer(int quoteId, int authorId)
    {
        var quote = data.Quotes
            .FirstOrDefault(q => q.Id == quoteId);

        var correctAuthorName = data.Quotes
            .Where(q => q.Id == quoteId)
            .Select(q => q.Author.Name)
            .FirstOrDefault();

        if (quote.AuthorId == authorId)
        {
            TempData[GlobalMessageKey] = $"Correct! The right answer is: {correctAuthorName}";
        }
        else
        {
            TempData[GlobalMessageKey] = $"Sorry, you are wrong! The right answer is: {correctAuthorName}";
        }

        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    public IActionResult NoAnswer(int quoteId, int authorId)
    {
        var quote = data.Quotes
            .FirstOrDefault(q => q.Id == quoteId);

        var correctAuthorName = data.Quotes
            .Where(q => q.Id == quoteId)
            .Select(q => q.Author.Name)
            .FirstOrDefault();

        if (quote.AuthorId != authorId)
        {
            TempData[GlobalMessageKey] = $"Correct! The right answer is: {correctAuthorName}";
        }
        else
        {
            TempData[GlobalMessageKey] = $"Sorry, you are wrong! The right answer is: {correctAuthorName}";
        }

        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    public IActionResult MultipleChoice(int quoteId, int authorId)
    {
        var quotes = data.Quotes
            .Select(q => new QuoteViewModel
            {
                Id = q.Id,
                Text = q.Text,
                Author = q.Author
            })
            .ToList();

        var quote = data.Quotes
            .FirstOrDefault(q => q.Id == quoteId);

        if (quote != null)
        {
            var correctAuthorName = data.Quotes
                .Where(q => q.Id == quoteId)
                .Select(q => q.Author.Name)
                .FirstOrDefault();

            if (quote.AuthorId == authorId)
            {
                TempData[GlobalMessageKey] = $"Correct! The right answer is: {correctAuthorName}";
            }
            else
            {
                TempData[GlobalMessageKey] = $"Sorry, you are wrong! The right answer is: {correctAuthorName}";
            }
        }

        return View(quotes);
    }

    [Authorize]
    public IActionResult All()
    {
        var quotes = data.Quotes
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
        var quote = data.Quotes
            .Select(q => new QuoteFormModel
            {
                Id = q.Id,
                Text = q.Text,
                AuthorId = q.AuthorId,
                AuthorName = q.Author.Name
            })
            .FirstOrDefault(q => q.Id == id);

        quote.Authors = GetAuthors();

        return View(quote);
    }

    [Authorize]
    [HttpPost]
    public IActionResult Edit(int id, QuoteFormModel quote)
    {
        if (!data.Authors.Any(a => a.Id == quote.AuthorId))
        {
            ModelState.AddModelError(nameof(quote.Id), "Author does not exist!");
        }

        if (!ModelState.IsValid)
        {
            quote.Authors = GetAuthors();

            return View(quote);
        }

        quotes.Edit(id, quote.Text, quote.AuthorId);

        TempData[GlobalMessageKey] = "Quote was successfully edited!";

        return RedirectToAction(nameof(All));
    }

    [Authorize]
    public IActionResult Delete(int id)
    {
        quotes.Delete(id);

        TempData[GlobalMessageKey] = "Quote was successfully deleted!";

        return RedirectToAction(nameof(All));
    }

    private IEnumerable<AuthorViewModel> GetAuthors()
        => data.Authors.Select(a => new AuthorViewModel
        {
            Id = a.Id,
            Name = a.Name
        })
        .ToList();
}
