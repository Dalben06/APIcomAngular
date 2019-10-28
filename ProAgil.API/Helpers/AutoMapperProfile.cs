using AutoMapper;
using ProAgil.API.Dto;
using ProAgil.Domain;
using ProAgil.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProAgil.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Evento, EventoDTO>()
                .ForMember(dest => dest.Palestrantes, opt =>
                {
                    opt.MapFrom(src => src.PalestrantesEvento.Select(x => x.Palestrante));
                }).ReverseMap();
            CreateMap<Palestrante, PalestranteDTO>()
                .ForMember(dest => dest.Eventos, opt => {
                    opt.MapFrom(src => src.PalestrantesEventos.Select(e => e.Evento)); }).ReverseMap();
                
            CreateMap<RedeSocial ,RedeSocialDTO>().ReverseMap();
            CreateMap<Lote, LoteDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();

        }
    }
}
