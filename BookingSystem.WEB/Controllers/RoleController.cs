using BookingSystem.DataModel.Master.Role;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace BookingSystem.WEB.Controllers
{
    public class RoleController : Controller
    {
        private IHttpClientFactory _httpClientFactory;

        public RoleController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var model = new IndexRoleVM();
            var client = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5156/Api/Role?page={page}";
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
                    model = JsonSerializer.Deserialize<IndexRoleVM>(responseString, option);
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
            var model = new CreateEditRoleVM();
            var client = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5156/Api/Role/{id}";
            if (id > 0)
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var option = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    try
                    {
                        model = JsonSerializer.Deserialize<CreateEditRoleVM>(responseString, option);
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
        public async Task<IActionResult> Upsert(CreateEditRoleVM model)
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5156/Api/Role";
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
