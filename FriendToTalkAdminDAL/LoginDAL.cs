using FriendToTalkAdminModel;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data.SqlClient;

namespace FriendToTalkAdminDAL;

public class LoginDAL(string? connectionString)
{
    public UserDetailModel? UserLogin(string email, string password, string SecretKey)
    {
        using var connection = new SqlConnection(connectionString);
        var parameters = new DynamicParameters();
        parameters.Add("@email", email, dbType: DbType.String);
        parameters.Add("@password", password, dbType: DbType.String);
        parameters.Add("@SecretKey", SecretKey, dbType: DbType.String);

        var userDetail = connection.QueryFirstOrDefault<UserDetailModel>(
            @"A_AdminUserLogin",
            parameters,
            commandType: CommandType.StoredProcedure);

        return userDetail;
    }

    public StatusModel CheckToken(string userId, string token, string SiteCode)
    {
       // StatusModel rm;
        using var connection = new SqlConnection(connectionString);
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", userId);
        parameters.Add("@Token", token);
        parameters.Add("@UserType", "ADMIN");
        parameters.Add("@SiteCode", IfNull(SiteCode));

        var userDetail = connection.QueryFirstOrDefault<UserDetailModel>(
            @"CheckToken",
            parameters,
            commandType: CommandType.StoredProcedure);
           // rm = connection.Query<StatusModel>(userDetail, parameters).FirstOrDefault();
          // userDetail = connection.Query<StatusModel>(SqlQuery, parameters).FirstOrDefault();
        
        return userDetail;
    }

    public StatusModel CheckRole(string userId, string sitepath)
    {
        StatusModel rm = new StatusModel();
        using var connection = new SqlConnection(connectionString);
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", userId);
        parameters.Add("@ControllerAndActionName", sitepath);

        var userDetail = connection.QueryFirstOrDefault<UserDetailModel>(
            @"A_CheckRole",
            parameters,
            commandType: CommandType.StoredProcedure);
        // rm = connection.Query<StatusModel>(userDetail, parameters).FirstOrDefault();
        // userDetail = connection.Query<StatusModel>(SqlQuery, parameters).FirstOrDefault();

        return userDetail;

    }
    
    #region Common Function

    private static object IfNull(string Data)
    {
        return string.IsNullOrEmpty(Data) ? (object)DBNull.Value : Data;
    }

    private static object IfNull(int Data)
    {
        return Data == 0 ? (object)DBNull.Value : Data;
    }

    public object IfNull(Guid Data)
    {
        return Data == Guid.Empty ? (object)DBNull.Value : Data;
    }

    public object IfNull(bool? Data)
    {
        return Data ?? (object)DBNull.Value;
    }

    private object IfNull(DateTime? Data)
    {
        if (Data == DateTime.MinValue)
            return (object)DBNull.Value;
        if (Data == null)
            return (object)DBNull.Value;
        return Data;
    }

    #endregion�Common�Function
}