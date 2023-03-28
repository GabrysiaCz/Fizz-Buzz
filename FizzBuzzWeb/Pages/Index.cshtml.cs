using Microsoft.AspNetCore.Mvc;
using FizzBuzzWeb.Forms;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FizzBuzzWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        [BindProperty]
        public FizzBuzzForm FizzBuzz { set; get; }
        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                Name = "Gabrysia";

            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToPage("./Privacy");
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int number;
            try
            {
                number = Int32.Parse(TextBox1.Text);

                if (number % 3 == 0 && number % 5 == 0)
                {
                    MessageBox.Show("FizzBuzz");
                }
                else if (number % 3 == 0)
                {
                    MessageBox.Show("Fizz");
                }
                else if (number % 5 == 0)
                {
                    MessageBox.Show("Buzz");
                }
                else
                {
                    MessageBox.Show("Liczba: " + number + " nie spełnia kryteriów FizzBuzz");
                }
            }
            catch
            {
                MessageBox.Show("Wprowadzono nieprawidłowe dane");
            }
        }
    }

}