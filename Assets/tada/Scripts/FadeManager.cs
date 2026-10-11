using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum FadeType
{
    Alpha,
}

public class FadeManager : SingletonMonoBehaviour<FadeManager>
{
    [SerializeField] private GameObject root;
    [SerializeField] private Graphic alphaImage;

    [SerializeField] private float fadeDuration = 0.3f;

    private void Start()
    {
        root.SetActive(false);
    }

    public IEnumerator Fadeout(FadeType fadeType = FadeType.Alpha)
    {
        root.SetActive(true);
        yield return Fade(fadeType, 0f, 1f);
    }

    public IEnumerator Fadein(FadeType fadeType = FadeType.Alpha)
    {
        yield return Fade(fadeType, 1f, 0f);
        root.SetActive(false);
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
