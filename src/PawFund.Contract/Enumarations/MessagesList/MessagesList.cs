namespace PawFund.Contract.Enumarations.MessagesList;

public enum MessagesList
{
    [Message("This email has been registered", "auth_email_01")]
    AuthEmailExistException,

    [Message("This email has not been registered, please try again", "auth_email_02")]
    AuthEmailNotFoundException,

    [Message("Registration time has passed, please register again", "auth_noti_01")]
    AuthUserNotFoundException,

    [Message("Passwords do not match", "auth_password_01")]
    AuthPasswordNotMatchException,

    [Message("Session has expired, please log in again", "auth_noti_02")]
    AuthRefreshTokenNull,

    [Message("Registration successful, please check email for confirmation", "auth_noti_03")]
    AuthRegisterSuccess,

    [Message("Account confirmation successful", "auth_noti_04")]
    VerifyEmailSuccess,

    [Message("Cannot find user with Id: {0}", "auth06")]
    AuthUserNotFoundByIdException,

    [Message("Your OTP does not match", "auth_otp_01")]
    AuthOtpForgotPasswordNotMatchException,

    [Message("An error occurred, please try again", "auth08")]
    AuthErrorChangePasswordException,

    [Message("Please check your email to enter otp", "auth_noti_05")]
    AuthForgotPasswordEmailSuccess,

    [Message("Please fill new password to change", "auth_noti_06")]
    AuthForgotPasswordOtpSuccess,

    [Message("Your account password has been changed successfully.", "auth_noti_07")]
    AuthForgotPasswordChangeSuccess,

    [Message("This email is already registered with Google", "auth_email_03")]
    AuthGoogleEmailRegisted,

    [Message("Logout successfully", "auth_noti_08")]
    AuthLogoutSuccess,

    [Message("Login Google fail, please try again", "auth_noti_09")]
    AuthLoginGoogleFail,

    [Message("This account was registered using another method", "auth_noti_11")]
    AuthAccountRegisteredAnotherMethod,

    [Message("Please go to profile to add missing information", "auth_noti_10")]
    AuthRegisterGoogleSuccess,

    [Message("Create Adopt Application Successfully", "adopt_noti_success_01")]
    AdoptCreateAdoptApplicationSuccess,

    [Message("Update Adopt Application Successfully", "adopt_noti_success_02")]
    AdoptUpdateApplicationSuccess,

    [Message("Delete Adopt Application Successfully", "adopt_noti_success_03")]
    AdoptDeleteApplicationSuccess,

    [Message("Approve Adopt Application Successfully", "adopt_noti_success_04")]
    AdoptApproveApplicationSuccess,

    [Message("Reject Adopt Application Successfully", "adopt_noti_success_05")]
    AdoptRejectApplicationSuccess,

    [Message("All Meeting Times", "adopt_noti_success_06")]
    AdoptGetAllMeetingTimeSuccess,

    [Message("Choose Meeting Time Successfully", "adopt_noti_success_07")]
    AdoptChooseMeetingTimeSuccess,

    [Message("Update meeting time successfully", "adopt_noti_exception_07")]
    AdoptUpdateMeetingTimeSuccess,

    [Message("All Adopt Applications", "adopt_noti_success_09")]
    AdoptGetAdoptApplicationsSuccess,

    [Message("Adopt Application Found", "adopt_noti_success_10")]
    AdoptGetAdoptApplicationSuccess,

    [Message("Can not found application with ID: {0}", "adopt_noti_exception_01")]
    AdoptApplicationNotFoundException,

    [Message("This adopt application does not belong to this adopter!", "adopt_noti_exception_02")]
    AdoptApplicationNotBelongToAdopterException,

    [Message("This adopter has already registered adopt application with this cat!", "adopt_noti_exception_03")]
    AdoptAdopterHasAlreadyRegisteredWithCatException,

    [Message("Can not found any adopt applications!", "adopt_noti_exception_04")]
    AdoptApplicationEmptyException,

    [Message("This application has already approved", "adopt_noti_exception_05")]
    AdoptApplicationHasAlreadyApprovedException,

    [Message("This application has already rejected", "adopt_noti_exception_06")]
    AdoptApplicationHasAlreadyRejectedException,

    [Message("This user has already banned.", "change_status_noti_exception_01")]
    UserHasAlreadyBannedException,

    [Message("This user has already unbanned", "change_status_noti_exception_02")]
    UserHasAlreadyUnbannedException,

    [Message("Can not find any meeting time", "adopt_noti_exception_07")]
    AdoptNotFoundAnyMeetingTimeException,


    [Message("This time is not exist, Please reload the page", "adopt_noti_exception_08")]
    AdoptNotFoundMeetingTimeException,

    [Message("This time is not available (Because no staff frees for this time)", "adopt_noti_exception_09")]
    AdoptNotStaffFreeForThisMeetingTimeException,

    [Message("User update profile successfully.", "account_noti_01")]
    UserUpdateProfileSuccess,

    [Message("The payment bank has been created successfully.", "payment_noti_01")]
    PaymentSucccess,
}