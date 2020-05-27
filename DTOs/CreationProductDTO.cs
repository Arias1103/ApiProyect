using System;
using Microsoft.AspNetCore.Http;

namespace ApiProyect.DTOs
{
    public class CreationProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public IFormFile Image { get; set; }
        public int BranchId { get; set; }
    }
}
