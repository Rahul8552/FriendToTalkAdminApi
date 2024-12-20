using Microsoft.AspNetCore.Mvc;
using AspNetCore.Totp;
using FriendToTalkAdminApi.Services;
using FriendToTalkAdminBAL;
using FriendToTalkAdminModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FriendToTalkAdminApi.APIControllers;

[ApiController]
[Route("[controller]/[action]")]
public class LoginController(LoginBAL ls, EncryptDecryptService eds) : ControllerBase
{
    private readonly LoginBAL _ls = ls ?? throw new ArgumentNullException(nameof(ls));
    private readonly EncryptDecryptService _eds = eds ?? throw new ArgumentNullException(nameof(eds));

    [HttpPost]
    public IActionResult LoginAuth(UserDetailModel user)
    {
        OTPAuth ud = new OTPAuth();
        try
        {
            // string ip = Common.GetIpAddress();
            if (string.IsNullOrEmpty(user.EmailId))
            {
                ud.ErrorMessage = "Email Not Provided";
                ud.ErrorCode = "2";
                ud.Result = "Failed";
            }
            else
            {
                if (ud.ErrorCode != "5")
                {
                    var totpSetupGenerator = new TotpSetupGenerator();
                    var totpSetup = totpSetupGenerator.Generate("CRMCASE", user.EmailId, ud.Result, 300, 300);
                    string qrCodeImageUrl = totpSetup.QrCodeImage;
                    ud.qrCodeImageUrl = qrCodeImageUrl;
                }

                ud.Mobile = "";
            }

            return Ok(ud);
        }
        catch (Exception ex)
        {
            StatusModel res = new StatusModel
            {
                ErrorMessage = "Error occure please try again latter",
                ErrorCode = "0",
                Result = "Failed"
            };
            return Ok(res);
        }
    }

    [HttpPost]
    public IActionResult UserLogin(LoginModel param)
    {
        StatusModel ud = new StatusModel();
        try
        {
            if (string.IsNullOrEmpty(param.Password))
            {
                ud.ErrorMessage = "Password Not Provided";
                ud.ErrorCode = "2";
                ud.Result = "Failed";
            }
            else if (string.IsNullOrEmpty(param.Email))
            {
                ud.ErrorMessage = "Email Not Provided";
                ud.ErrorCode = "2";
                ud.Result = "Failed";
            }
            else if (string.IsNullOrEmpty(param.OTPNumber))
            {
                ud.ErrorMessage = "OTP Not Provided";
                ud.ErrorCode = "2";
                ud.Result = "Failed";
            }
            else
            {
                {
                    // var generator = new TotpGenerator();
                    // var totpValidator = new TotpValidator(generator);
                    // bool isCorrectPIN = totpValidator.Validate(param.SecretKey, Convert.ToInt32(param.OTPNumber));
                    // if (isCorrectPIN != true)
                    // {
                    //     StatusModel res = new StatusModel
                    //     {
                    //         ErrorMessage = "Invalid OTP",
                    //         ErrorCode = "0",
                    //         Result = "Failed"
                    //     };
                    //     return Ok(res);
                    // }0

                    dynamic dict = new JObject();
                    param.Password = eds.Encrypt(param.Password);
                    UserDetailModel? uDetails = _ls.UserLogin(param.Email, param.Password, param.SecretKey);
                    if (uDetails?.ErrorCode != "1")
                        return Ok(Common.CreateStatusModel(uDetails?.ErrorMessage, uDetails?.ErrorCode));
                    dict.UserId = uDetails.UserId;
                    dict.FName = uDetails.FName;
                    dict.LName = uDetails.LName;
                    dict.LoginToken = uDetails.LoginToken;
                    if (uDetails.FirstTimeLogin != null) dict.FirstTimeLogin = uDetails.FirstTimeLogin;
                    dict.RoleCode = uDetails.RoleCode;
                    dict.CallbackStatusId = uDetails.CallbackStatusId;
                    if (uDetails.ErrorMessage != null) dict.ErrorMessage = uDetails.ErrorMessage;
                    dict.ErrorCode = uDetails.ErrorCode;
                    dict.Result = uDetails.Result;

                    return Ok(dict);
                    // Super Admin
                }
            }

            return Ok(ud);
        }
        catch (Exception ex)
        {
            return Ok(Common.CreateStatusModel(ex.Message, "2"));
        }
    }
}