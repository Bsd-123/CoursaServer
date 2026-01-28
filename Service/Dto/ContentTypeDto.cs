using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class ContentTypeDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public byte[]? DisplayIcon { get; set; }

        public IFormFile? FileImage { get; set; }
    }
}
