using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace General_EvetsParty_MVC.Models
{
    public class EventCustomerEditModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [CompareAttribute("Confirm", ErrorMessage = "Passwords don't match.")]
        public string Password { get; set; }
        [CompareAttribute("Password", ErrorMessage = "Passwords don't match.")]
        public string Confirm { get; set; }   
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[A-ZА-ЯЄЇІ][a-zа-яєїі]*([\- ]+[A-ZА-ЯЄЇІ][a-zа-яєїі]+)*$",
         ErrorMessage = "Characters are not allowed.")]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[A-ZА-ЯЄЇІ][a-zа-яєїі]*([\- ]+[A-ZА-ЯЄЇІ][a-zа-яєїі]+)*$",
         ErrorMessage = "Characters are not allowed.")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(13)]
        [MinLength(13)]
        [RegularExpression(@"^\+\d{12}$",
         ErrorMessage = "Incorrect mobile. Format : +ccoooxxxxxxx, cc-country code, ooo - operator, xxxxxxx - number")]
        public string Mobile { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
         ErrorMessage = "Incorrect eMail. Format : 'xxx@xx.xx'")]
        public string Email { get; set; }
        public string Birthday { get; set; }
        [RegularExpression(@"^[\w\-_. ]+$",
         ErrorMessage = "Characters are not allowed.")]
        public string Country { get; set; }
        public string Address { get; set; }       
        public bool ShowContacts { get; set; }
    }
}