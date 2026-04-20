using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.CategoryService.DTOs
{
    public class UpdateCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Sort { get; set; }
    }
}
