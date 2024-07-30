using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using BookingSystem.DataModel.Master.Resource;
using System.Net.Http.Headers;
using System.Net.Http;

namespace BookingSystem.WEB.Controllers
{
    public class ResourceController : Controller
    {
        private IHttpClientFactory _httpClientFactory;
        public ResourceController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var model = new IndexResVM();
            var client = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5156/api/Resource?page={page}";
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
                    model = JsonSerializer.Deserialize<IndexResVM>(responseString, option);
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
            var model = new CreateEditResVM();
            var client = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5156/Api/Resource/{id}";
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
                        model = JsonSerializer.Deserialize<CreateEditResVM>(responseString, option);
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
        public async Task<IActionResult> Upsert(CreateEditResVM dto)
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5156/Api/Resource";

            using (var multipartContent = new MultipartFormDataContent())
            {
                multipartContent.Add(new StringContent(dto.ID.ToString()), nameof(dto.ID));
                multipartContent.Add(new StringContent(dto.Name), nameof(dto.Name));
                multipartContent.Add(new StringContent(dto.Status.ToString()), nameof(dto.Status));
                multipartContent.Add(new StringContent(dto.Icon ?? string.Empty), nameof(dto.Icon));

                if (dto.file != null)
                {
                    var fileContent = new StreamContent(dto.file.OpenReadStream());
                    fileContent.Headers.ContentType = new MediaTypeHeaderValue(dto.file.ContentType);
                    multipartContent.Add(fileContent, nameof(dto.file), dto.file.FileName);
                }

                if (dto.code != null)
                {
                    for (int i = 0; i < dto.code.Count; i++)
                    {
                        multipartContent.Add(new StringContent(dto.code[i].ID.ToString()), $"code[{i}].ID");
                        multipartContent.Add(new StringContent(dto.code[i].ResourceCode), $"code[{i}].ResourceCode");
                        multipartContent.Add(new StringContent(dto.code[i].IsActive.ToString()), $"code[{i}].Status");
                    }
                }

                var response = await client.PostAsync(url, multipartContent);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var url = $"http://localhost:5156/api/Resource?id={id}";
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
