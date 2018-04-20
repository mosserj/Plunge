using System.Collections.Generic;

namespace backend.Core.Model
{
  public class AppUserAuth
  {
    public AppUserAuth() : base()
    {
      UserName = "Not authorized";
      BearerToken = string.Empty;
    }

    public string UserName { get; set; }
    public string BearerToken { get; set; }
    public bool IsAuthenticated { get; set; }

    public List<AppUserRole> Roles { get; set; }
  }
}
