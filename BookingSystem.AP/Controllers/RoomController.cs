using BookingSystem.DataModel.Master.BookingCode;
using BookingSystem.DataModel.Master.Room;
using BookingSystem.Provider;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : Controller
    {
        private RoomProvider roomProvider;
        public RoomController(RoomProvider roomProvider)
        {
            this.roomProvider = roomProvider;

        }
        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            try
            {
                var data = roomProvider.GetIndexBC(page);
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
                var data = roomProvider.GetOne(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult CreateEdit(CreateEditRoomVM model)
        {
            try
            {
                if (model.ID > 0)
                {
                    roomProvider.UpdateRoom(model);
                }
                else
                {
                    roomProvider.InsertRoom(model);
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
                roomProvider.DeleteRoom(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
