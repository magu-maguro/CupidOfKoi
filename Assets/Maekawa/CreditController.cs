using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CreditController : MonoBehaviour
{
    [SerializeField] private GameObject creditPanel;
    [SerializeField] private Vector3 hidePos;
    [SerializeField] private Vector3 showPos;

    [SerializeField] private float moveDuration = 0.5f;
    private RectTransform creditPanelRectTransform;
    private Coroutine moveCoroutine;

    void Start()
    {
        creditPanelRectTransform = creditPanel.GetComponent<RectTransform>();
        creditPanel.SetActive(false);
        creditPanelRectTransform.anchoredPosition = hidePos;
    }

    private void ShowCredit()
    {
        creditPanel.SetActive(true);

        StartMoving(showPos, false);
    }

    private void HideCredit()
    {
        if (!creditPanel.activeSelf)
        {
            creditPanelRectTransform.anchoredPosition = hidePos;
            return;
        }

        StartMoving(hidePos, true);
    }

    private void StartMoving(Vector3 targetPosition, bool deactivateOnComplete)
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = StartCoroutine(MovePanel(targetPosition, deactivateOnComplete));
    }

    private IEnumerator MovePanel(Vector3 targetPosition, bool deactivateOnComplete)
    {
        Vector3 startPosition = creditPanelRectTransform.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = moveDuration <= 0f ? 1f : Mathf.Clamp01(elapsedTime / moveDuration);
            creditPanelRectTransform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        creditPanelRectTransform.anchoredPosition = targetPosition;
        moveCoroutine = null;

        if (deactivateOnComplete)
        {
            creditPanel.SetActive(false);
        }
    }

    public void ToggleCreditPanel(bool isVisible)
    {
        if (isVisible)
        {
            ShowCredit();
        }
        else
        {
            HideCredit();
        }
    }
}
