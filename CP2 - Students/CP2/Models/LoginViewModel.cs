using System.ComponentModel.DataAnnotations;

namespace CP2.Models;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }

    [Required]
    [Range(1, 9)]
    [Display(Name = "Secret number")]
    public int SelectedNumber { get; set; } = 1;
}

