using System;
using System.ComponentModel.DataAnnotations;

namespace Boris.BeekProject.Model
{
    public class User
    {
        [Required]
        public Guid Id { get; set; }
    }
}
