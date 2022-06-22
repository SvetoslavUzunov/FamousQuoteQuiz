namespace FamousQuoteQuiz.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FamousQuoteQuiz.Data;
using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.Models.Quote;

public class HomeController : Controller
{
    private readonly QuoteQuizDbContext data;

    public HomeController(QuoteQuizDbContext data)
        => this.data = data;

    public IActionResult Index()
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}