package backend.model;

public class AppUserAuth
{
  private String _UserName;
  private String _BearerToken;
  private boolean _IsAuthenticated;

  public AppUserAuth()
  {
    _UserName = "Not authorized";
    _BearerToken = "";
  }

  public void SetUserName(String username){
    this._UserName = username;
  }

  public String UserName(){
    return this._UserName;
  }

  public void SetBearerToken(String bearerToken){
    this._BearerToken = bearerToken;
  }

  public String BearerToken(){
    return this._BearerToken;
  }

  public void SetIsAuthenticated(Boolean isAuthenticated){
    this._IsAuthenticated = isAuthenticated;
  }

  public boolean IsAuthenticated(){
    return this._IsAuthenticated;
  }


  //public List<AppUserRole> Roles { get; set; }
}