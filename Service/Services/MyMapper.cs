using AutoMapper;
using Repository.Entities;
using Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class MyMapper : Profile
    {
        string path = Directory.GetCurrentDirectory() + "\\images\\";
        public MyMapper()
        {
            CreateMap<ContentType, ContentTypeDto>().ForMember("DisplayIcon", x => x.MapFrom(y => fromStringToByte(y.DisplayIcon)));
            CreateMap<ContentTypeDto, ContentType>().ForMember("DisplayIcon", x => x.MapFrom(y => y.FileImage.FileName));
            CreateMap<Owner, OwnerDto>().ForMember("Image", x => x.MapFrom(y => fromStringToByte(y.Image)));
            CreateMap<OwnerDto, Owner>().ForMember("Image", x => x.MapFrom(y => y.FileImage.FileName));
        }
        public byte[] fromStringToByte(string mypath)
        {
            if (string.IsNullOrEmpty(mypath)) return null;
            return File.ReadAllBytes(path + mypath);
        }
    }
}
