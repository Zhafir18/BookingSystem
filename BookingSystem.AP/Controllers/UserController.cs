using BookingSystem.DataModel.Master.User;
using BookingSystem.Provider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserProvider userProvider;

        public UserController(UserProvider userProvider)
        {
            this.userProvider = userProvider;
        }

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                var data = userProvider.GetIndexUser();
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
                var data = userProvider.GetOne(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult CreateEdit(CreateEditUserVM model)
        {
            try
            {
                if (model.UserId > 0)
                {
                    userProvider.UpdateUser(model);
                }
                else
                {
                    userProvider.InsertUser(model);
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
                userProvider.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("userrole")]
        public ActionResult UserRole()
        {
            try
            {
                var data = userProvider.GetUserRoleIndex();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
