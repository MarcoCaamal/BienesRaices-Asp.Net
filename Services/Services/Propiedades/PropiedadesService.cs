using AutoMapper;
using BaseCore.Entidades.ViewModels;
using Common.Mensajes;
using Domain.Entidades.Propiedades;
using Domain.Entidades.Propiedades.ViewModels;
using Domain.Util;
using Microsoft.Extensions.Logging;
using Repository.IRepositories.Propiedades;
using Services.IServices;
using Services.IServices.Propiedades;

namespace Services.Services.Propiedades
{
    public class PropiedadesService : IPropiedadesService
    {
        private readonly IPropiedadesRepository _propiedadesRepository;
        private readonly IAlmacenadorArchivosService _almacenadorArchivosService;
        private readonly IMapper _mapper;
        private readonly ILogger<PropiedadesService> _logger;

        private readonly string _contenedor = "propiedades";

        public PropiedadesService(IPropiedadesRepository propiedadesRepository,
            IAlmacenadorArchivosService almacenadorArchivosService,
            IMapper mapper,
            ILogger<PropiedadesService> logger)
        {
            _propiedadesRepository = propiedadesRepository;
            _almacenadorArchivosService = almacenadorArchivosService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<Propiedad>> ObtenerLista()
        {
            IEnumerable<Propiedad> lista = null;
            try
            {
                lista = await _propiedadesRepository.ObtenerLista();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
            }
            return lista;
        }

        public async Task<IEnumerable<AnunciosVM>> ObtenerListaParaAnuncios()
        {
            IEnumerable<Propiedad> lista = null;
            try
            {
                lista = await _propiedadesRepository.ObtenerLista();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
            }
            return _mapper.Map<IEnumerable<AnunciosVM>>(lista);
        }

        public async Task<IEnumerable<AnunciosVM>> ObtenerListaParaAnuncios(int? numeroRegistros)
        {
            IEnumerable<Propiedad> lista = null;
            try
            {
                lista = await _propiedadesRepository.ObtenerLista(numeroRegistros);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
            }
            return _mapper.Map<IEnumerable<AnunciosVM>>(lista);
        }

        public async Task<IEnumerable<IndexPropiedadesVM>> ObtenerListaParaIndex()
        {
            IEnumerable<Propiedad> lista = null;
            try
            {
                lista = await _propiedadesRepository.ObtenerLista(numeroRegistros: 3);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
            }
            return _mapper.Map<IEnumerable<IndexPropiedadesVM>>(lista);
        }

        public async Task<Propiedad> ObtenerPorId(int? id)
        {
            var propiedad = new Propiedad();
            try
            {
                propiedad = await _propiedadesRepository.ObtenerPorId(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
            }
            return propiedad;
        }

        public async Task<ResponseHelper> Crear(PropiedadCreacionVM model, string rootPath)
        {
            var response = new ResponseHelper();
            var propiedad = model.Propiedad;
            try
            {
                propiedad.Creado = DateTime.Now;
                propiedad.Imagen = await _almacenadorArchivosService.GuardarImagen(model.ImagenPropiedad, _contenedor, rootPath);

                if (string.IsNullOrEmpty(propiedad.Imagen))
                {
                    response.Success = false;
                    response.Message = PropiedadesMensajes.ErrorImagen;
                    return response;
                }

                if(await _propiedadesRepository.Crear(propiedad) > 0)
                {
                    response.Success = true;
                    response.Message = PropiedadesMensajes.CreacionExitosa;
                    return response;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                response.Success = false;
                response.Message = PropiedadesMensajes.CreacionError;
                return response;
            }
            return response;
        }

        public async Task<ResponseHelper> Editar(PropiedadCreacionVM model, string rootPath)
        {
            var response = new ResponseHelper();
            var propiedad = model.Propiedad;
            try
            {
                if(model.ImagenPropiedad != null)
                {
                    propiedad.Imagen = await _almacenadorArchivosService.EditarImagen(model.ImagenPropiedad, propiedad.Imagen, _contenedor, rootPath);
                }

                if (string.IsNullOrEmpty(propiedad.Imagen))
                {
                    response.Success = false;
                    response.Message = PropiedadesMensajes.ErrorImagen;
                    return response;
                }

                if (await _propiedadesRepository.Editar(propiedad) > 0)
                {
                    response.Success = true;
                    response.Message = PropiedadesMensajes.EdicionExitosa;
                    return response;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                response.Success = false;
                response.Message = PropiedadesMensajes.EdicionError;
                return response;
            }
            return response;
        }

        public async Task<ResponseHelper> Eliminar(Propiedad propiedad, string rootPath)
        {
            var response = new ResponseHelper();
            try
            {
                if (await _almacenadorArchivosService.BorrarImagen(propiedad.Imagen, _contenedor, rootPath) is false)
                {
                    response.Success = false;
                    response.Message = PropiedadesMensajes.ErrorEliminarImagen;
                    _logger.LogError(response.Message);
                    return response;
                }

                if(await _propiedadesRepository.Eliminar(propiedad.Id) > 0)
                {
                    response.Success = true;
                    response.Message = PropiedadesMensajes.EliminacionExitosa;
                    _logger.LogInformation(response.Message);
                    return response;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                response.Success = false;
                response.Message = PropiedadesMensajes.EliminacionError;
            }
            return response;
        }
    }
}
