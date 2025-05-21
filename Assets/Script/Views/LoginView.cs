using MvvmSample.Router;
using MvvmSample.ViewModel.Login;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MvvmSample.View.Login
{
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

        /// <summary>
        /// パスワードのマスキングをon/offにする
        /// </summary>
        [SerializeField] private Button maskingOnButton;
        [SerializeField] private Button maskingOffButton;

        [Header("テキスト関連")]
        [SerializeField] private TextMeshProUGUI errorText;

        // インスタンスを保持
        private LoginViewModel viewModel;
        private RouterController routerController;

        private void Start()
        {
            // ViewModelのインスタンス化
            viewModel = new LoginViewModel();

            // RouterControllerのインスタンス化
            routerController = new RouterController();

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
                // SceneManager.LoadScene(SceneName.Scene02_Success.ToString());
                this.routerController.ChangeScene(SceneName.Scene02_Success);
            })
            .AddTo(this);

            // エラーメッセージの表示
            viewModel.ErrorMessage
            .Subscribe(msg => errorText.text = msg)
            .AddTo(this);

            // マスキングoffボタンのイベント登録
            maskingOffButton.onClick.AddListener(OnClickMaskingOffButton);
            
            // マスキングonボタンのイベント登録
            maskingOnButton.onClick.AddListener(OnClickMaskingOnButton);
        }

        /// <summary>
        /// パスワードのマスキングをoffにする
        /// </summary>
        public void OnClickMaskingOffButton()
        {
            maskingOffButton.gameObject.SetActive(false);
            maskingOnButton.gameObject.SetActive(true);
            passwordInput.contentType = TMP_InputField.ContentType.Standard;
            passwordInput.ForceLabelUpdate();
        }

        /// <summary>
        /// パスワードのマスキングをonにする
        /// </summary>
        public void OnClickMaskingOnButton()
        {
            maskingOffButton.gameObject.SetActive(true);
            maskingOnButton.gameObject.SetActive(false);
            passwordInput.contentType = TMP_InputField.ContentType.Password;
            passwordInput.ForceLabelUpdate();
        }
    }
}
