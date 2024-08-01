using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using BookingSystem.DataModel.Master.Room;
using BookingSystem.Provider;
using BookingSystem.DataModel;
using BookingSystem.DataModel.Master.Location;

namespace BookingSystem.WEB.Controllers
{
    public class RoomController : Controller
    {
        private IHttpClientFactory _httpClientFactory;
        private LocationProvider _locationProvider;
        public RoomController(IHttpClientFactory httpClientFactory, LocationProvider locationProvider)
        {
            _locationProvider = locationProvider;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var model = new IndexRoomVM();
            var client = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5156/api/Room?page={page}";
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
                    model = JsonSerializer.Deserialize<IndexRoomVM>(responseString, option);
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
            var model = new CreateEditRoomVM();
            var client = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5156/api/Room/{id}";
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
                        model = JsonSerializer.Deserialize<CreateEditRoomVM>(responseString, option);
                    }
                    catch (Exception ex)
                    {
                        return View("Error");
                    }
                }
            }

            var locationUrl = "http://localhost:5156/api/Location";
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
                    model.LocationDropdown = JsonSerializer.Deserialize<List<LocationDropdown>>(locationResponseString, option);
                }
                catch (Exception ex)
                {
                    return View("Error");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(CreateEditRoomVM dto)
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5156/Api/Room";
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
            var url = $"http://localhost:5156/api/Room?id={id}";
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
