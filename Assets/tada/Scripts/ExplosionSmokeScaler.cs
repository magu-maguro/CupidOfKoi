using UnityEngine;
using System.Collections;

public class ExplosionSmokeScaler : MonoBehaviour
{
    [SerializeField] private float duration = 1.0f;
    [SerializeField] private Vector3 startScale = Vector3.zero;
    [SerializeField] private Vector3 targetScale = new Vector3(3f, 3f, 3f);
    [SerializeField] private AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    private Coroutine scaleCoroutine;

    private void OnEnable()
    {
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }

        scaleCoroutine = StartCoroutine(AnimateScaleCoroutine());
    }

    private void OnDisable()
    {
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
            scaleCoroutine = null;
        }
    }

    private IEnumerator AnimateScaleCoroutine()
    {
        float elapsedTime = 0f;

        transform.localScale = startScale;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / duration);
            float curveValue = scaleCurve.Evaluate(t);
            transform.localScale = Vector3.LerpUnclamped(startScale, targetScale, curveValue);

            yield return null;
        }

        transform.localScale = targetScale;
        
        scaleCoroutine = null;

        // 自身を非表示にする
        this.gameObject.SetActive(false);
    }
}
