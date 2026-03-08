using Microsoft.AspNetCore.Http;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class LessonDto
    {
        public int Id { get; set; }

        public int Idx { get; set; }

        public string Name { get; set; } = null!;

        public int TypeId { get; set; }

        public int CourseId { get; set; }

        public string Content { get; set; } = null!;
        public IFormFile? File { get; set; }

        public string MimeType { get; set; } = null!;

        public bool IsFree { get; set; }

        public bool? Status { get; set; }

        public CourseDto? Course { get; set; } = null!;

        public virtual ContentTypeDto? Type { get; set; } = null!;

        public int? DurationSec { get; set; }
    }
}
