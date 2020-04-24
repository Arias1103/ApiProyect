using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiProyect.Entities
{
    public class Branch
    {
        public int Id { get; set; }
        [Required]
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
        public List<Product> Products { get; set; }

    }
}
