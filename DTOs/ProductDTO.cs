using System;
using System.ComponentModel.DataAnnotations;
using ApiProyect.Entities;
using Microsoft.AspNetCore.Http;

namespace ApiProyect.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}

