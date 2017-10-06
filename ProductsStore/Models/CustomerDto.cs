﻿using System.ComponentModel.DataAnnotations;

namespace ProductsStore.Models
{
    public class CustomerDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [Range(1, 50, ErrorMessage = "Invalid State id")]
        public int? StateId { get; set; }

        [Required]
        public int? Zip { get; set; }

        [Required]
        public string Gender { get; set; }
    }
}