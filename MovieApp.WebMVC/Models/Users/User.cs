using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.WebMVC.Models.Users
{
    public class User
    {
        public string Id { get; set; }
        [DisplayName("Username")]
        public string Username { get; set; }
        [DisplayName("Password")]
        public string Password { get; set; }
        
        public bool IsAdmin { get; set; }

        public string Error { get; set; }
        public bool IsSuccess { get { return string.IsNullOrEmpty(Error); } }
    }
}
