using Aspire.Hosting;
using Aspire.Hosting.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace VetClinic.Tests.Fixtures;

/// <summary>
/// Фикстура для однократного поднятия оркестратора Aspire и контейнера MySQL на время выполнения интеграционных тестов
/// </summary>
public class VetClinicAspireFixture : IAsyncLifetime
{
    private DistributedApplication? _app;

    /// <summary>
    /// HTTP-клиент, предварительно настроенный на адрес запущенного VetClinic.Api.Host
    /// </summary>
    public HttpClient HttpClient { get; private set; } = null!;

    public async ValueTask InitializeAsync()
    {
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.VetClinic_AppHost>();
        _app = await appHost.BuildAsync();

        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            });
        });

        await _app.StartAsync();
        HttpClient = _app.CreateHttpClient("vetclinic-api");
    }

    public async ValueTask DisposeAsync()
    {
        HttpClient?.Dispose();
        if (_app is not null)
        {
            await _app.DisposeAsync();
        }
    }
}
