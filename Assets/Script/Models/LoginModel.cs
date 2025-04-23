using MvvmSample.Common;
using UniRx;

namespace MvvmSample.Models.LoginModel
{
    public class LoginModel
    {
        // ダミーアカウント
        private readonly string dummyEmail = "test@example.com";
        private readonly string dummyPassword = "password123";

        public LoginResult Authenticate(string email, string password)
        {
            if (email == dummyEmail && password == dummyPassword)
            {
                return new LoginResult(true);
            }

            return new LoginResult(false, "メールアドレスまたはパスワードが間違っています。");
        }
    }
}

