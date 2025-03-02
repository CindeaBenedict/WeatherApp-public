using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Models
{
    public class LoginViewModel
{
    [Required]
    [Display(Name = "Username or Email")]
    public string Username { get; set; } = string.Empty; // Add this

    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty; // Keep if needed

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }
}

}
