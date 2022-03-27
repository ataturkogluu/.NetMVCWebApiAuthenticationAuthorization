using AutoMapper;
using AuthenticationAndAuthorization.Dto;
using DestekAybey.Models;

namespace AuthenticationAndAuthorization.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<LecErrorCode, LecErrorCodeDto>();
            CreateMap<LecErrorCodeDto, LecErrorCode>();

            CreateMap<LecErrorCode, LecErrorCodeUpdateDto>();
            CreateMap<LecErrorCodeUpdateDto, LecErrorCode>();

            CreateMap<LecFaq, LecFaqDto>();
            CreateMap<LecFaqDto, LecFaq>();

            CreateMap<LecFaq, LecFaqUpdateDto>();
            CreateMap<LecFaqUpdateDto, LecFaq>();
            
            CreateMap<LecLangHtmlTitle, LecLangHtmlTitleDto>();
            CreateMap<LecLangHtmlTitleDto,LecLangHtmlTitle>();

            CreateMap<LecLangHtmlTitle, LecLangHtmlTitleUpdateDto>();
            CreateMap<LecLangHtmlTitleUpdateDto,LecLangHtmlTitle>();
            
            CreateMap<LecLanguage, LecLanguageDto>();
            CreateMap<LecLanguageDto, LecLanguage>();

            CreateMap<LecLanguage, LecLanguageUpdateDto>();
            CreateMap<LecLanguageUpdateDto, LecLanguage>();
            
            CreateMap<LecProductSerie, LecProductSerieDto>();
            CreateMap<LecProductSerieDto, LecProductSerie>();

            CreateMap<LecProductSerie, LecProductSerieUpdateDto>();
            CreateMap<LecProductSerieUpdateDto, LecProductSerie>();
        }
    }
}