using System.ComponentModel.DataAnnotations;
namespace wedding_planner.Models{
    public class RegisterViews{
        [Required]
        [MinLength(2)]
        public string FirstName{get;set;}
        [Required]
        [MinLength(2)]
        public string LastName{get;set;}
        [Required]
        [EmailAddress]
        public string Email{get;set;}

        [Required]
        [DataType(DataType.Password)]
        public string Password{get;set;}

        [DataType(DataType.Password)] 
        [Compare ("Password",ErrorMessage="Password and PAssword confirmation do not match")]
        public string PasswordConfirmation{get;set;}
    }
    
}