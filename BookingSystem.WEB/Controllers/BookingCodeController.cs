using BookingSystem.DataModel.Master.BookingCode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace BookingSystem.WEB.Controllers
{
    public class BookingCodeController : Controller
    {
        private IHttpClientFactory _httpClientFactory;
        public BookingCodeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var model = new IndexBCVM();
            var client = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5156/Api/BookingCode?page={page}";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                try
                {
                    model = JsonSerializer.Deserialize<IndexBCVM>(responseString, option);
                }
                catch (Exception ex)
                {
                    return View("Error");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Upsert(int id)
        {
            var model = new CreateEditBCVM();
            var client = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5156/Api/BookingCode/{id}";
            if (id > 0)
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var option = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    try
                    {
                        model = JsonSerializer.Deserialize<CreateEditBCVM>(responseString, option);
                    }
                    catch (Exception ex)
                    {
                        return View("Error");
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Upsert(CreateEditBCVM dto)
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5156/Api/BookingCode";
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                return RedirectToAction("Index");
            }
            return View(dto);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5156/api/BookingCode?id={id}";
            var response = await client.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
