using FriendToTalkAdminDAL;
using FriendToTalkAdminModel;

namespace FriendToTalkAdminBAL;

public class LoginBAL
{
    private readonly LoginDAL _lr; // Define a field to hold the injected instance

    // Constructor that accepts LoginDAL and assigns it to the field
    public LoginBAL(LoginDAL lr)
    {
        _lr = lr ?? throw new ArgumentNullException(nameof(lr));
    }


    public UserDetailModel? UserLogin(string Email, string password, string SecretKey)
    {
        try
        {
            return _lr.UserLogin(Email, password, SecretKey);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public StatusModel CheckToken(string userId, string token, string SiteCode)
    {
        try
        {
            return _lr.CheckToken(userId, token, SiteCode);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public StatusModel CheckRole(string userId, string sitepath)
    {
        try
        {
            return _lr.CheckRole(userId, sitepath);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}