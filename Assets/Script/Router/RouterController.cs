using UnityEngine.SceneManagement;

namespace MvvmSample.Router
{
    /// <summary>
    /// 画面遷移やエラー表示・ローディング表示など画面表示の根幹部分を担当
    /// </summary>
    public class RouterController
    {
        /// <summary>
        /// 画面遷移を行うためのメソッド
        /// </summary>
        /// <param name="sceneName">遷移するSCENE名</param>
        public void ChangeScene(SceneName sceneName)
        {
            SceneManager.LoadScene(sceneName.ToString());
        }
    }
}
