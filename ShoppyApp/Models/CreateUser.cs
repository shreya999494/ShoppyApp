using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ShoppyApp.Models
{
    public class CreateUser
    {
        //internal IEnumerable<KeyValuePair<string, string>> Content;

        //public int U_Id { get; set; }

        [Required]
        [StringLength(10,MinimumLength =2,ErrorMessage ="Name length should be more than 2 and can't be more than 10")]
        public string FirstName { get; set;}

        [Required]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Name length should be more than 2 and can't be more than 10")]
        public string LastName { get; set;}

        [Required]
        [EmailAddress]
        public string Email { get; set;}

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set;}

        [NotMapped]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Password does not matched")]
        public string ConfirmPassword { get; set;}

    }
}