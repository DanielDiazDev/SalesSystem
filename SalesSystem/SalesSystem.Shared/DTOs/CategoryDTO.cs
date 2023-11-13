using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Shared.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string? Description { get; set; }

        public override bool Equals(object o)
        {
            var other = o as CategoryDTO;
            return other?.CategoryId == CategoryId;
        }
        public override int GetHashCode() => CategoryId.GetHashCode();
        public override string ToString()
        {
            return Description;
        }
    }
}
