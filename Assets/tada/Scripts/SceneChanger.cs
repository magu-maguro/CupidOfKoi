using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



using UnityEngine.InputSystem;

public class SceneChanger : MonoBehaviour
{
    private static SceneChanger instance;
    public static SceneChanger Instance => instance;

    private void Awake()
    {
        // すでにあるなら自身を削除
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;

        // シーンをまたぐので残しておく
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

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

        // フェードアウト前イベント

        // フェードアウト
        yield return fade.Fadeout(fadeType);

        // フェードアウト後イベント

        // 次のシーンへ遷移
        yield return SceneManager.LoadSceneAsync(sceneName);

        currentSceneName = sceneName;

        // フェードイン前イベント

        // フェードイン
        yield return fade.Fadein(fadeType);

        // フェードイン後イベント
    }
}
