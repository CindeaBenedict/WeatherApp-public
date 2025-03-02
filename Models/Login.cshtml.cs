using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WeatherApp.Models; // 🔥 Ensure you have this

namespace WeatherApp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(
            SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager, 
            ILogger<LoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; } = new LoginViewModel(); // Ensure it's initialized

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
{
    if (!ModelState.IsValid)
    {
        _logger.LogWarning("Login attempt failed: Model state is invalid.");
        return Page();
    }

    var user = await _userManager.FindByEmailAsync(Input.Email);
    if (user == null)
    {
        ModelState.AddModelError(string.Empty, "Invalid email or password.");
        _logger.LogWarning("Login failed: User with email {Email} not found.", Input.Email);
        return Page();
    }

    var validPassword = await _userManager.CheckPasswordAsync(user, Input.Password);
    if (!validPassword)
    {
        ModelState.AddModelError(string.Empty, "Invalid email or password.");
        _logger.LogWarning("Login failed: Incorrect password for {Email}.", Input.Email);
        return Page();
    }

    var result = await _signInManager.PasswordSignInAsync(user, Input.Password, Input.RememberMe, lockoutOnFailure: false);
    if (result.Succeeded)
    {
        _logger.LogInformation("User {Email} logged in successfully.", Input.Email);
        return RedirectToPage("/Index"); // ✅ Redirect to homepage after login
    }
    
    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
    return Page();
}

    }

    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
