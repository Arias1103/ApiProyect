using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApiProyect.Entities;

namespace ApiProyect.DTOs
{
    public class BranchDTO
    {
        public int Id { get; set; }
        [Required]
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
    }
}
