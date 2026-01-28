using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class CourseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int Price { get; set; }

        public byte[]? Image { get; set; }
        public int SkillId { get; set; }
        public int OwnerId { get; set; }

        public bool? Status { get; set; }

        public IFormFile? FileImage { get; set; }
    }
}
