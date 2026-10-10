using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : SingletonMonoBehaviour<SceneChanger>
{
    [SerializeField] private FadeManager fade;

    private string currentSceneName = "";

    public void ChangeScene(string sceneName, FadeType fadeType = FadeType.Alpha)
    {
        StartCoroutine(ChangeSceneAsync(sceneName, fadeType));
    }

    private IEnumerator ChangeSceneAsync(string sceneName, FadeType fadeType)
    {
        // 同じシーンの場合は何もしない
        if (sceneName == currentSceneName) yield break;

        // 「今からシーン遷移（フェードアウト）を始めるよ」という通知
        Notify<ISceneChanging>(x => x.OnSceneChanging());

        // フェードアウト
        yield return fade.Fadeout(fadeType);

        // 「画面が暗くなったので、今から裏で実際のシーン読み込みをするよ」という通知
        // 例）重いメモリの解放
        Notify<ISceneLoading>(x => x.OnSceneLoading());

        // 次のシーンへ遷移
        yield return SceneManager.LoadSceneAsync(sceneName);

        currentSceneName = sceneName;

        // 「シーンの読み込みが終わって、今からフェードインするよ」という通知
        // 例：新しいシーンの初期化処理、新しいBGMの再生開始、カメラの初期位置セット
        Notify<ISceneLoaded>(x => x.OnSceneLoaded());

        // フェードイン
        yield return fade.Fadein(fadeType);

        // 「完全にシーンが切り替わって、画面も明るくなったよ」という通知
        // 例：タイマーなどの進行開始
        Notify<ISceneChanged>(x => x.OnSceneChanged());
    }

    private void Notify<T>(Action<T> action) where T : class
    {
        // MEMO: 今回は1画面1シーンなので、シーン内の全MonoBehaviourから抽出
        var targets = UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                                        .OfType<T>();

        foreach (var target in targets)
        {
            action(target);
        }
    }
}
