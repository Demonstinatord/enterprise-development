using VetClinic.Application.Contracts.Breeds;

namespace VetClinic.Api.Host.Controllers;

/// <summary>
/// Контроллер для управления справочником пород животных
/// </summary>
/// <param name="service">Сервис управления породами</param>
/// <param name="logger">Логгер контроллера пород</param>
public class BreedsController(IBreedAppService service, ILogger<BreedsController> logger)
    : CrudControllerBase<BreedDto, BreedCreateUpdateDto, int>(service, logger);