using BookingSystem.DataModel.Master.BookingCode;
using BookingSystem.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingCodeController : ControllerBase
    {

        private BookingCodeProvider BCProvider;
        public BookingCodeController(BookingCodeProvider BCProvider)
        {
            this.BCProvider = BCProvider;

        }
        [HttpGet]
        public ActionResult Index(int page=1)
        {
            try
            {
                var data = BCProvider.GetIndexBC(page);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var data = BCProvider.GetOne(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult CreateEdit(CreateEditBCVM model)
        {
            try
            {
                if (model.ID > 0)
                {
                    BCProvider.UpdateBc(model);
                }
                else
                {
                    BCProvider.InsertBc(model);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                BCProvider.DeleteBC(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
