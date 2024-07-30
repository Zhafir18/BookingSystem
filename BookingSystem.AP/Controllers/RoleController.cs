using BookingSystem.DataModel.Master.Role;
using BookingSystem.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private RoleProvider RoleProvider;

        public RoleController(RoleProvider roleProvider)
        {
            RoleProvider = roleProvider;
        }

        [HttpGet]
        public ActionResult Index(int page = 1)
        {
            try
            {
                var data = RoleProvider.GetIndexRole(page);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetOne(int id)
        {
            try
            {
                var data = RoleProvider.GetOne(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateEdit(CreateEditRoleVM model)
        {
            try
            {
                if (model.ID > 0)
                {
                    RoleProvider.UpdateRole(model);
                }
                else
                {
                    RoleProvider.InsertRole(model);
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
                RoleProvider.DeleteRole(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
