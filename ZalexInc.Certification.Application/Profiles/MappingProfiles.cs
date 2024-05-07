using AutoMapper;
using ZalexInc.Certification.Application.Commands;
using ZalexInc.Certification.Application.Dtos;
using ZalexInc.Certification.Domain.Entities;

namespace ZalexInc.Certification.Application.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Certificate, CertificateDto>()
                .ForMember(x => x.PageNumber, opt => opt.Ignore())
                .ForMember(x => x.PageSize, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<CreateCertificateCommand, Certificate>();
            CreateMap<UpdateCertificateCommand, Certificate>();

        }
    }
}
