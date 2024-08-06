using BookingSystem.DataModel.Master.Location;
using BookingSystem.DataModel.Master.Role;
using BookingSystem.DataModel.Master.Room;
using BookingSystem.DataModel.Master.User;
using BookingSystem.Provider;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace BookingSystem.WEB.Controllers
{
    public class UserController : Controller
    {
        private IHttpClientFactory _httpClientFactory;
        private RoleProvider _roleProvider;

        public UserController(IHttpClientFactory httpClientFactory, RoleProvider roleProvider)
        {
            _roleProvider = roleProvider;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var model = new IndexUserVM();
            var client = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5156/api/User?page={page}";
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
                    model = JsonSerializer.Deserialize<IndexUserVM>(responseString, option);
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
            var model = new CreateEditUserVM();
            var client = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5156/api/User/{id}";
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
                        model = JsonSerializer.Deserialize<CreateEditUserVM>(responseString, option);
                    }
                    catch (Exception ex)
                    {
                        return View("Error");
                    }
                }
            }

            var locationUrl = "http://localhost:5156/api/Role";
            var locationResponse = await client.GetAsync(locationUrl);
            if (locationResponse.IsSuccessStatusCode)
            {
                var locationResponseString = await locationResponse.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                try
                {
                    model.RoleDropdown = JsonSerializer.Deserialize<List<RoleDropdown>>(locationResponseString, option);
                }
                catch (Exception ex)
                {
                    return View("Error");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(CreateEditUserVM model)
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5156/Api/User";
            var json = JsonSerializer.Serialize(model);
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
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5156/api/User?id={id}";
            var response = await client.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var option = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
