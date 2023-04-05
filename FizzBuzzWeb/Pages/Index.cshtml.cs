using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MyApp.Pages
{
public class IndexModel : PageModel
{
private readonly IHttpContextAccessor _httpContextAccessor;
private ISession _session => _httpContextAccessor.HttpContext.Session;
    public IndexModel(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    [BindProperty]
    [Range(1899, 2022, ErrorMessage = "Rok musi być między 1899 a 2022.")]
    public int Year { get; set; }

    [BindProperty]
    [StringLength(100, ErrorMessage = "Imię nie może być dłuższe niż 100 znaków.")]
    public string Name { get; set; }

    public string Result { get; private set; }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        bool isLeap = DateTime.IsLeapYear(Year);
        Result = $"{Name} urodził{((Name.EndsWith("a") || Name.EndsWith("ę")) ? "a" : "ł")} się w {Year} roku. {(isLeap ? "To był rok przestępny." : "To nie był rok przestępny.")}";

        var results = _session.GetObjectFromJson<List<Result>>("Results") ?? new List<Result>();
        results.Add(new Result { Name = Name, Year = Year, IsLeap = isLeap });
        _session.SetObjectAsJson("Results", results);
            return RedirectToPage("./Saved");
}

public void OnGet()
{
}

public class SavedModel : PageModel
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISession _session;
    public List<Result> Results { get; set; }

    public SavedModel(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _session = _httpContextAccessor.HttpContext.Session;
    }

    public void OnGet()
    {
        Results = _session.GetObjectFromJson<List<Result>>("Results") ?? new List<Result>();
    }
}

public class Result
{
    public string Name { get; set; }
    public int Year { get; set; }
    public bool IsLeap { get; set; }
}

