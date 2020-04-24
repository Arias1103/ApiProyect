﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ApiProyect.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [Required]
        public int BranchId { get; set; }
        public Branch Branches { get; set; }
    }
}
