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

    [Message("Your OTP does not match", "auth07")]
    AuthOtpForgotPasswordNotMatchException,

    [Message("An error occurred, please try again", "auth08")]
    AuthErrorChangePasswordException,
}