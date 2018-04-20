using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend.Services;
using backend.Core.Model;
using backend.Core.Interfaces;

namespace backend.Controllers
{
  [Route("api/[controller]")]
  public class SecurityController : Controller
  {
    private JwtSettings _settings = null;
    private readonly IEntityFrameworkApplicationRepository _entityRepository;

    public SecurityController(JwtSettings settings, IEntityFrameworkApplicationRepository entityRepository)
    {
      _settings = settings;
      _entityRepository = entityRepository;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody]AppUser user)
    {
      IActionResult ret = null;
      AppUserAuth auth = new AppUserAuth();
      SecurityManager mgr = new SecurityManager(_settings, _entityRepository);

      auth = mgr.ValidateUser(user);
      if (auth.IsAuthenticated)
      {
        ret = StatusCode(StatusCodes.Status200OK, auth);
      }
      else
      {
        ret = StatusCode(StatusCodes.Status404NotFound,
                         "Invalid User Name/Password.");
      }

      return ret;
    }
  }
}
