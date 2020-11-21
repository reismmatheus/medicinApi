using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicinApi.Models
{
    public class Register
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
