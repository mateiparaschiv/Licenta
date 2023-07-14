using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System.ComponentModel.DataAnnotations;

namespace LicentaApp.Models
{
    [CollectionName("users")]
    public class UserModel : MongoIdentityUser<Guid>
    {
        [Required]
        //public string Username { get; set; }
        public override string UserName { get => base.UserName; set => base.UserName = value; }


        [Required]
        //[EmailAddress]
        [Display(Name = "Email")]
        //public string Email { get; set; }
        public override string Email { get => base.Email; set => base.Email = value; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

}
