using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;


namespace TestFurstWPF.Services
{
    public class DbService
    {
        private readonly ILogger<DbService> _logger;
        private string? _errorMessage = null;

        public DbService(ILogger<DbService> logger)
        {
            _logger = logger;
        }

        private static readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://192.168.100.100:5048")
        };

        public async Task<Result<List<Downtime>>> GetDownTimeAsync()
        {
            try
            {
                var requestUri = await client.GetAsync($"/api/DownTime/downtime?date={DateTime.Now:s}");
                requestUri.EnsureSuccessStatusCode();

                Console.WriteLine(await requestUri.Content.ReadAsStringAsync());
                _logger.LogInformation("Выполняется запрос к API: {RequestUri}", requestUri);

                if (requestUri.IsSuccessStatusCode)
                {
                    var jsonResponse = await requestUri.Content.ReadAsStringAsync();

                    List<Downtime>? downtimeList = JsonConvert.DeserializeObject<List<Downtime>>(jsonResponse);
                    _logger.LogInformation("Данные успешно получены и десериализованы. Количество записей: {Count}", downtimeList?.Count ?? 0);

                    if (downtimeList != null)
                    {
                        return Result.Success(downtimeList);
                    }
                    else
                    {
                        _errorMessage = "Пустой список";
                        _logger.LogInformation(_errorMessage);
                        return Result.Success(new List<Downtime>());
                    }
                }
                else if (requestUri.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    _errorMessage = "Пустой список";
                    _logger.LogInformation(_errorMessage);
                    return Result.Success(new List<Downtime>());
                }
                else
                {
                    // Логируем ошибку
                    _errorMessage = $"Ошибка HTTP-запроса: {(int)requestUri.StatusCode} - {requestUri.ReasonPhrase}";
                    _logger.LogWarning(_errorMessage);
                    return Result.Failure<List<Downtime>>(_errorMessage);
                }
            }
            catch (Exception ex)
            {
                _errorMessage = $"Исключение при выполнении запроса к API.";
                _logger.LogError(ex, _errorMessage);
                return Result.Failure<List<Downtime>>(_errorMessage);
            }
        }
    }
}
