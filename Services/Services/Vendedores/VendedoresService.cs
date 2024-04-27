using AutoMapper;
using Common.Mensajes;
using Domain.Entidades.Vendedores;
using Domain.Util;
using Microsoft.Extensions.Logging;
using Repository.IRepositories.Vendedores;
using Services.IServices.Vendedores;

namespace Services.Services.Vendedores
{
    public class VendedoresService: IVendedoresService
    {
        private readonly IVendedoresRepository _vendedoresRepository;
        private readonly ILogger<VendedoresService> _logger;
        private readonly IMapper _mapper;

        public VendedoresService(IVendedoresRepository vendedoresRepository,
            ILogger<VendedoresService> logger,
            IMapper mapper)
        {
            _vendedoresRepository = vendedoresRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Vendedor>?> ObtenerLista()
        {
            IEnumerable<Vendedor>? lista = null;
            try
            {
                lista = await _vendedoresRepository.ObtenerLista();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
            }
            return lista;
        }

        public async Task<Vendedor?> ObtenerPorId(int? id)
        {
            var vendedor = new Vendedor();
            try
            {
                vendedor = await _vendedoresRepository.ObtenerPorId(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
            }
            return vendedor;
        }

        public async Task<ResponseHelper> Crear(Vendedor model)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                if(model is null)
                {
                    response.Success = false;
                    response.Message = VendedoresMensajes.CreacionError;
                    _logger.LogError(response.Message);
                    return response;
                }

                if(await _vendedoresRepository.Crear(model) > 0)
                {
                    response.Success = true;
                    response.Message = VendedoresMensajes.CreacionExitosa;
                    _logger.LogInformation(response.Message);
                    return response;
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = VendedoresMensajes.CreacionError;
                _logger.LogError(e.Message, e);
            }
            return response;
        }

        public async Task<ResponseHelper> Editar(Vendedor model)
        {
            ResponseHelper response = new ResponseHelper();
            try
            {
                var vendedor = await _vendedoresRepository.ObtenerPorId(model.Id);

                if(vendedor is null)
                {
                    response.Success = false;
                    response.Message = VendedoresMensajes.EdicionError;
                    _logger.LogError(response.Message);
                    return response;
                }

                vendedor = _mapper.Map(model, vendedor);

                if (await _vendedoresRepository.Editar(model) > 0)
                {
                    response.Success = true;
                    response.Message = VendedoresMensajes.EdicionExitosa;
                    _logger.LogInformation(response.Message);
                    return response;
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = VendedoresMensajes.CreacionError;
                _logger.LogError(e.Message, e);
            }
            return response;
        }

        public async Task<ResponseHelper> Eliminar(int? id)
        {
            var response = new ResponseHelper();
            try
            {
                var vendedor = await _vendedoresRepository.ObtenerPorId(id);

                if(vendedor is null)
                {
                    response.Success = false;
                    response.Message = VendedoresMensajes.EliminacionError;
                    return response;
                }

                if(await _vendedoresRepository.Eliminar(id) > 0)
                {
                    response.Success = true;
                    response.Message = VendedoresMensajes.EliminacionExitosa;
                    return response;
                }
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = VendedoresMensajes.EliminacionError;
                _logger.LogError(e.Message, e);
            }
            return response;
        }
    }
}
