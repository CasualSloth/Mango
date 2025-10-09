using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Mango.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50)] public string Name {  get; set; }
    }
}
