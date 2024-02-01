using karavancidan.Model.ViewModel.Login;
using karavancidan.Services.Abstract.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace karavancidan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        private readonly ILoginServices _loginServices;
        public LoginController(ILoginServices loginServices)
        {
            _loginServices = loginServices;

        }
        [HttpPost]
        public async Task<ActionResult<UserLoginResponseViewModel>> LoginUserAsync([FromBody] UserLoginRequestViewModel request)
        {

            var result = await _loginServices.LoginUserAsync(request);

            return Ok(result);


        }
    }
}
