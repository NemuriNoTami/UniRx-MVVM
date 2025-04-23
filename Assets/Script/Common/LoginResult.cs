public class LoginResult
{
    public bool IsSuccess { get; }
    public string ErrorMessage { get; }

    public LoginResult(bool isSuccess, string errorMessage = "")
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }
}