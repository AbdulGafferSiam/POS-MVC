using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PosMvcFinal.Models
{
    public class Admin
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter valid")]
        public string PassWord { get; set; }
        [Required(ErrorMessage = "Please enter valid password")]
        public string Name { get; set; }
    }
}