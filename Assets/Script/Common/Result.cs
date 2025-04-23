namespace MvvmSample.Common.Result
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public T Value { get; }
        public string Error { get; }

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="value">対象の型</param>
        /// <param name="isSuccess">成功したかどうか？</param>
        /// <param name="error">エラーメッセージ</param>
        protected Result(T value, bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        /// <summary>
        /// 成功時の結果を返す
        /// </summary>
        /// <param name="value">結果</param>
        /// <returns>結果を返す</returns>
        public static Result<T> Success(T value)
        {
            return new Result<T>(value, true, null);
        }

        /// <summary>
        /// 失敗時の結果を返す
        /// </summary>
        /// <param name="error">エラーメッセージ</param>
        /// <returns>結果を返す</returns>
        public static Result<T> Failure(string error)
        {
            return new Result<T>(default(T), false, error);
        }
    }
}
