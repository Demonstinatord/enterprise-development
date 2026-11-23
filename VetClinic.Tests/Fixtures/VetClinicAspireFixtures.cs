using Aspire.Hosting;
using Aspire.Hosting.Testing;

namespace VetClinic.Tests.Fixtures;

/// <summary>
/// Фикстура для однократного поднятия оркестратора Aspire и контейнера MySQL на время выполнения интеграционных тестов
/// </summary>
public class VetClinicAspireFixtures : IAsyncLifetime
{
    private DistributedApplication? _app;

    /// <summary>
    /// HTTP-клиент, предварительно настроенный на адрес запущенного VetClinic.Api.Host
    /// </summary>
    public HttpClient HttpClient { get; private set; } = null!;

    public async ValueTask InitializeAsync()
    {
        // Создаем тестовый билдер на базе вашего AppHost
        var appHost = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.VetClinic_AppHost>();

        _app = await appHost.BuildAsync();
        await _app.StartAsync();

        // Получаем HttpClient, привязанный к имени ресурса API из AppHost.cs ("vetclinic-api")
        HttpClient = _app.CreateHttpClient("vetclinic-api");
    }

    public async Task DisposeAsync()
    {
        HttpClient?.Dispose();
        if (_app is not null)
        {
            await _app.DisposeAsync();
        }
    }
}
