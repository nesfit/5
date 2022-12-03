using System.ComponentModel.DataAnnotations;

namespace CookBook.Web.BL.Models;

public class LoginModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}
