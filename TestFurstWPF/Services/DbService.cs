using Newtonsoft.Json;
using System.Net.Http;

namespace TestFurstWPF.Services
{
    class DbService
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5048")
        };

        public async Task<List<Downtime>> GetDownTimeAsync()
        {
            try
            {
                // Выполняем GET-запрос
                var response = await _httpClient.GetAsync($"/api/DownTime/downtime?date={DateTime.Now.ToString("s")}");

                // Проверяем, успешен ли запрос
                if (response.IsSuccessStatusCode)
                {
                    // Считываем контент ответа как строку
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    // Десериализуем JSON в список объектов Downtime
                    var downtimeList = JsonConvert.DeserializeObject<List<Downtime>>(jsonResponse);

                    return downtimeList ?? new List<Downtime>();
                }
                else
                {
                    // Логируем ошибку
                    Console.WriteLine($"Ошибка HTTP-запроса: {(int)response.StatusCode} - {response.ReasonPhrase}");
                    return new List<Downtime>();
                }
            }
            catch (Exception ex)
            {
                // Логируем исключение
                Console.WriteLine($"Исключение при запросе данных: {ex.Message}");
                return new List<Downtime>();
            }
        }
    }
}
