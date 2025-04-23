using System;
using MvvmSample.Models.LoginModel;
using UniRx;
using UnityEngine;

public class LoginViewModel
{
    //入力を受け取るSubject
    private readonly ReactiveProperty<string> email = new ReactiveProperty<string>(string.Empty);
    private readonly ReactiveProperty<string> password = new ReactiveProperty<string>(string.Empty);

    // ログインボタンの活性化状態を保持するプロパティ
    public IReadOnlyReactiveProperty<bool> CanLogin { get; }

    // エラーメッセージのプロパティ
    public ReactiveProperty<string> ErrorMessage { get; } = new ReactiveProperty<string>(string.Empty);

    // ログイン成功を通知するイベント
    private readonly Subject<Unit> loginSuccessSubject = new Subject<Unit>();
    public IObservable<Unit> OnLoginSuccess => loginSuccessSubject.AsObservable();

    /// <summary>
    /// LoginModelのインスタンス
    /// </summary> 
    private readonly LoginModel loginModel = new LoginModel();

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public LoginViewModel()
    {
        // 入力値の変更を購読してログインボタンの状態を判定
        CanLogin = email.CombineLatest(password, (e, p) =>
        IsValidEmai(e) && IsValidPassword(p)
        )
        .ToReactiveProperty();

    }

    /// <summary>
    /// メールアドレスの入力受付
    /// </summary>
    public void EmailInput(String value)
    {
        email.Value = value;
    }

    /// <summary>
    /// パスワードの入力受付
    /// </summary>
    public void PasswordInput(String value)
    {
        password.Value = value;
    }

    /// <summary>
    /// ログイン処理
    /// </summary>
    public void Login()
    {
        var result = loginModel.Authenticate(email.Value, password.Value);

        if (result.IsSuccess)
        {
            ErrorMessage.Value = string.Empty;
            loginSuccessSubject.OnNext(Unit.Default);
            Debug.Log("ログイン成功");

        }
        else
        {
            ErrorMessage.Value = "メールアドレスまたはパスワードが正しくありません。";

        }
    }


    // バリデーションチェック
    private bool IsValidEmai(string email)
    {
        return !string.IsNullOrEmpty(email) && email.Contains("@");
    }

    private bool IsValidPassword(string password)
    {
        return !string.IsNullOrEmpty(password) && password.Length >= 6;
    }

}
