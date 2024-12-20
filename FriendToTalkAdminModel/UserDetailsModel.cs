namespace FriendToTalkAdminModel;

public class UserDetailModel: StatusModel
{
    public string EmailId { get; set; }
    public string UserId { get; set; }
    public string Id { get; set; }
    public string UserName { get; set; }
    public string FName { get; set; }
    public string LName { get; set; }
    public string Mobile { get; set; }
    public string LoginToken { get; set; }
    public bool? FirstTimeLogin { get; set; }
    public string RoleCode { get; set; }
    public int CallbackStatusId { get; set; }
}