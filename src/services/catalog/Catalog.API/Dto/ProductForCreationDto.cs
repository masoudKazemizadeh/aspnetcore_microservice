using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Dto
{
    public class ProductForCreationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public string Category { get; set; }
        [Required]

        public string Summary { get; set; }
        public string Description { get; set; }
        [Required]

        public decimal Price { get; set; }

    }
}
