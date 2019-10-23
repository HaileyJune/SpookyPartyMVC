using System.ComponentModel.DataAnnotations;

namespace SpookyPartyMVC.Models
{
public class UserLogin
{
    [Required]
    [Display(Name = "Username")]
    public string LogUsername { get; set; }
    [Required]
    [Display(Name = "Password")]
    public string LogPassword { get; set; }

}
}