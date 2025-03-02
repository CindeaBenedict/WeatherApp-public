using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Pages.Account
{
    public class AccountModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountModel> _logger;

        public AccountModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        // Properties to hold user information
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PreferredLanguage { get; set; } = "Not Set";
        public List<FavoriteLocation> FavoriteLocations { get; set; } = new List<FavoriteLocation>();

        // GET method for loading user data
        public async Task OnGetAsync()
        {
            _logger.LogInformation("OnGetAsync invoked.");
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                UserName = user.UserName ?? "Unknown User";
                Email = user.Email ?? "No Email";
                PreferredLanguage = user.Preferences?.PreferredLanguage ?? "Not Set";
                FavoriteLocations = user.FavoriteLocations != null
                    ? new List<FavoriteLocation>(user.FavoriteLocations)
                    : new List<FavoriteLocation>();
                
                _logger.LogInformation($"User data loaded for: {UserName}");
            }
            else
            {
                _logger.LogWarning("No user is logged in.");
            }
        }

        // POST method for handling logout
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            try
            {
                _logger.LogInformation("Logout process started.");
                await _signInManager.SignOutAsync();
                
                HttpContext.Session?.Clear();
                Response.Cookies.Delete(".AspNetCore.Identity.Application");

                _logger.LogInformation("User signed out successfully.");
                return RedirectToPage("/Account/Login");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during logout: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while logging out. Please try again.");
                return Page();
            }
        }

        // POST method for updating user preferences
        public async Task<IActionResult> OnPostUpdatePreferencesAsync(string preferredLanguage)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning("Cannot update preferences: No user is logged in.");
                return RedirectToPage("/Account/Login");
            }

            user.Preferences ??= new UserPreference();
            user.Preferences.PreferredLanguage = preferredLanguage;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation($"Preferences updated for user: {user.UserName}");
                return RedirectToPage("/Account/Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
                _logger.LogError($"Error updating preferences: {error.Description}");
            }

            return Page();
        }
    }
}
