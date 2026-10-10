using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum FadeType
{
    Alpha,
}

public class FadeManager : MonoBehaviour
{
    private static FadeManager instance;
    public static FadeManager Instance => instance;

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

    [SerializeField] private GameObject root;
    [SerializeField] private Graphic alphaImage;

    [SerializeField] private float fadeDuration = 0.3f;

    private void Start()
    {
        root.SetActive(false);
    }

    public IEnumerator Fadeout(FadeType fadeType = FadeType.Alpha)
    {
        Debug.Log("フェードアウト開始");
        root.SetActive(true);
        yield return Fade(fadeType, 0f, 1f);
        Debug.Log("フェードアウト終了");
    }

    public IEnumerator Fadein(FadeType fadeType = FadeType.Alpha)
    {
        Debug.Log("フェードイン開始");
        yield return Fade(fadeType, 1f, 0f);
        root.SetActive(false);
        Debug.Log("フェードイン終了");
    }

    private IEnumerator Fade(FadeType fadeType, float fromAlpha, float toAlpha)
    {
        var graphic = GetGraphic(fadeType);

        var elapsedTime = 0f;
        while(true)
        {
            elapsedTime += Time.deltaTime;

            var color = graphic.color;
            color.a = Mathf.Lerp(fromAlpha, toAlpha, elapsedTime / fadeDuration);
            graphic.color = color;

            if (elapsedTime >= fadeDuration) break;

            yield return null;
        }
    }

    private Graphic GetGraphic(FadeType fadeType)
    {
        return fadeType switch
        {
            FadeType.Alpha => alphaImage,
            _ => null,
        };
    }
}
