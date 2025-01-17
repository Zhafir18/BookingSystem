﻿using BookingSystem.DataModel.Master.Location;
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
        private RoleProvider _roleProvider;

        public RoleController(RoleProvider roleProvider)
        {
            _roleProvider = roleProvider;
        }

        [HttpGet]
        public ActionResult<List<RoleDropdown>> GetRoleDropdown()
        {
            try
            {
                var dropdown = _roleProvider.GetRoleDropdown();
                return Ok(dropdown);
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
                var data = _roleProvider.GetOne(id);
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
                    _roleProvider.UpdateRole(model);
                }
                else
                {
                    _roleProvider.InsertRole(model);
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
                _roleProvider.DeleteRole(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
