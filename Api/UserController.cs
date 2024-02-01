using karavancidan.Model.Results;
using karavancidan.Model.ViewModel.Users;
using karavancidan.Services.Abstract.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Security.Claims;

namespace karavancidan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserServices _userServices;
        private readonly IHttpContextAccessor _accessor;
        public UserController(IUserServices userServices, IHttpContextAccessor accessor)
        {
            _userServices = userServices;
            _accessor = accessor;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserQueryModel>>> UserList()
        {
            var data = await _userServices.GetUser();

            return Ok(data);

        }
    }
}
