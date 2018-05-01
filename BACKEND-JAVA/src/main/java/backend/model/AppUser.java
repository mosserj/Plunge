package backend.model;

public class AppUser {
    private String _userName;
    private String _password;

    public void SetUserName(String username) {
        this._userName = username;
    }

    public String UserName() {
        return this._userName;
    }

    public void SetPassword(String password) {
        this._password = password;
    }

    public String Password() {
        return this._password;
    }
}