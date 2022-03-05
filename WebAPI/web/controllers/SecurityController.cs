using Microsoft.AspNetCore.Mvc;
using web.authorization;
using web.models.users;
using web.authorization.abstractions;
using web.mappers.abstractions;

namespace web.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IAuthentication _authentication;
        private readonly IAuthenticateRequestMapper _authenticateRequestMapper;

        private readonly IAuthenticateResponseMapper _authenticateResponseMapper;
        private readonly IJwtUtils _jwtUtils;

        public SecurityController(IAuthentication authentication,
            IAuthenticateRequestMapper authenticateRequestMapper,
            IAuthenticateResponseMapper authenticateResponseMapper,
            IJwtUtils jwtUtils)
        {
            _authentication = authentication;
            _authenticateRequestMapper = authenticateRequestMapper;
            _authenticateResponseMapper = authenticateResponseMapper;
            _jwtUtils = jwtUtils;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<AuthenticateResponse>> Post(AuthenticateRequest authenticateRequest)
        {
            try
            {
                var user = _authenticateRequestMapper.Map(authenticateRequest);
                var auth = await _authentication.Login(user);

                if (auth != null)
                {
                    var jwtToken = _jwtUtils.GenerateJwtToken(auth);

                    return Ok(_authenticateResponseMapper.Map(auth, jwtToken));
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
