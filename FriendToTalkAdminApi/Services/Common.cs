using FriendToTalkAdminModel;

namespace FriendToTalkAdminApi.Services;


public class Common
{
    public static StatusModel CreateStatusModel(string? errorMessage, string? errorCode, string? id = default(string)
        // Guid id = default(Guid)
    )
    {
        string result = (errorCode == "1") ? "Success" : "Failed";
        return new StatusModel
        {
            Result = result,
            ErrorMessage = errorMessage,
            ErrorCode = errorCode,
            //Id = (id == default(Guid)) ? Guid.Empty : id
            Id = (id == default(string)) ? string.Empty : id
        };
        // StatusModel response1 = CreateStatusModel(); // uses default values for all parameters and sets the Id property to Guid.Empty
        // StatusModel response2 = CreateStatusModel(value: 1); // sets the Result property to "Success" and sets the Id property to Guid.Empty
        // StatusModel response3 = CreateStatusModel(errorCode: "123"); // sets the ErrorCode property to "123" and sets the Id property to Guid.Empty
        // StatusModel response4 = CreateStatusModel(value: 1, errorMessage: "Something went wrong"); // sets the Result property to "Success", sets the ErrorMessage property to "Something went wrong", and sets the Id property to Guid.Empty
        // StatusModel response5 = CreateStatusModel(id: Guid.NewGuid()); // sets the Id property to the specified Guid value and uses default values for all other parameters
    }
}