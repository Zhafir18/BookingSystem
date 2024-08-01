using BookingSystem.DataModel.Master.BookingCode;
using BookingSystem.DataModel.Master.Location;
using BookingSystem.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private LocationProvider LocProvider;
        public LocationController(LocationProvider LocProvider)
        {
            this.LocProvider = LocProvider;

        }

        [HttpGet]
        public ActionResult<List<LocationDropdown>> GetLocationDropdown()
        {
            try
            {
                var dropdown = LocProvider.GetLocationDropdown();
                return Ok(dropdown);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateEdit(CreatEditLVM model)
        {
            try
            {
                if (model.ID > 0)
                {
                    LocProvider.UpdateLoc(model);
                }
                else
                {
                    LocProvider.InsertLoc(model);
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
                LocProvider.DeleteLoc(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
