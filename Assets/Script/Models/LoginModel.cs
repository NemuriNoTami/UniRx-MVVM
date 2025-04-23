using MvvmSample.Common.Result;
using UniRx;

namespace MvvmSample.Models.LoginModel
{
    public class LoginModel
    {
        // ダミーアカウント
        private readonly string dummyEmail = "test@example.com";
        private readonly string dummyPassword = "password123";

        public Result<Unit> Authenticate(string email, string password)
        {
            if (email == dummyEmail && password == dummyPassword)
            {
                return Result<Unit>.Success(Unit.Default);
            }

            return Result<Unit>.Failure("メールアドレスまたはパスワードが間違っています。");
        }
    }
}