using MvvmSample.router;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginView : MonoBehaviour
{
    /// <summary>
    /// ゲームオブジェクトを格納
    /// </summary>
    [Header("入力関連")]
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField passwordInput;
    [Header("ボタン関連")]
    [SerializeField] private Button loginButton;
    [Header("テキスト関連")]
    [SerializeField] private TextMeshProUGUI errorText;

    private LoginViewModel viewModel;

    private void Start()
    {
        // ViewModelのインスタンス化
        viewModel = new LoginViewModel();

        /// <summary>
        /// メールアドレス入力 → ViewModel
        /// </summary>
        emailInput
        .onValueChanged
        .AsObservable()
        .Subscribe(viewModel.EmailInput)
        .AddTo(this);

        /// <summary>
        /// パスワード入力 → ViewModel
        /// </summary>
        passwordInput
        .onValueChanged
        .AsObservable()
        .Subscribe(viewModel.PasswordInput)
        .AddTo(this);

        /// <summary>
        /// ViewModel → ボタンの活性化
        /// </summary>
        viewModel.CanLogin
        .Subscribe(isEnabled => loginButton.interactable = isEnabled)
        .AddTo(this);

        // ログインボタン押下時
        loginButton
        .OnClickAsObservable()
        .Subscribe(_ => viewModel.Login())
        .AddTo(this);

        // ログイン成功時に画面遷移
        viewModel.OnLoginSuccess
        .Subscribe(_ =>
        {
            SceneManager.LoadScene(SceneName.Scene02_Success.ToString());
        })
        .AddTo(this);


        // エラーメッセージの表示
        viewModel.ErrorMessage
        .Subscribe(msg => errorText.text = msg)
        .AddTo(this);

    }
}
