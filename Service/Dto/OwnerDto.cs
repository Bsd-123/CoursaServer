using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Service.Dto
{
    public class OwnerDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public string OwnerName { get; set; } = null!;

        public byte[]? Image { get; set; }

        public double Percentage { get; set; }

        public string? PaymentNumber { get; set; }

        public IFormFile? FileImage { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        [BindNever]
        public UserDto? User { get; internal set; }

        
    }
}
