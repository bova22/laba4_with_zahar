using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models; // Проверь, чтобы namepsace совпадал с твоим
using System.Text.Json;

namespace OnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeController()
        {
            // ВАЖНО: Вместо 5001 нужно поставить порт, на котором запускается бэкенд Олега
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:5001/") }; 
        }

        // Этот метод срабатывает при открытии главной страницы
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/products");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var products = JsonSerializer.Deserialize<List<Product>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return View(products);
                }
            }
            catch (Exception)
            {
                // Если бэкенд Олега еще не запущен, вернем пустой список, чтобы сайт не падал
                return View(new List<Product>());
            }
            return View(new List<Product>());
        }
    }
}
