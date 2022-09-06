using AutoMapper;
using BaseCore.Entidades.ViewModels;
using Domain.Entidades.Propiedades;
using Domain.Entidades.Propiedades.ViewModels;

namespace BaseCore.Lib.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Propiedades
            CreateMap<Propiedad, IndexPropiedadesVM>();
            CreateMap<Propiedad, AnunciosVM>();
            CreateMap<Propiedad, PropiedadCreacionVM>()
                .ForMember(x => x.ImagenPropiedad, options => options.Ignore())
                .ForMember(x => x.Vendedores, options => options.Ignore())
                .ReverseMap();

        }
    }
}
