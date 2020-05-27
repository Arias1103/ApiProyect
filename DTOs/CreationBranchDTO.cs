using System;
using System.ComponentModel.DataAnnotations;

namespace ApiProyect.DTOs
{
    public class CreationBranchDTO
    {
        [Required]
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
    }
}
