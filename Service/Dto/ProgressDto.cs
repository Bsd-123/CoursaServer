using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class ProgressDto
    {
        public int UserId { get; set; }

        public int LessonId { get; set; }

        public int Seconds { get; set; }

        public DateTime? LastView { get; set; }

        public LessonDto? Lesson { get; set; } = null!;

        public UserDto? User { get; set; } = null!;
    }
}
