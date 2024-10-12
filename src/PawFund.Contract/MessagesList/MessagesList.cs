namespace PawFund.Contract.MessagesList;

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

    [Message("Logout successfully", "auth_noti_08")]
    AuthLogoutSuccess,
}