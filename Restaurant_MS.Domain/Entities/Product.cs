using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_MS.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
