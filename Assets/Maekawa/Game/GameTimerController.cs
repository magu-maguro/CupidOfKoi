using UnityEngine;
using TMPro;

public class GameTimerController : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    private bool isSubscribed;

    private void OnEnable()
    {
        SubscribeToTimer();
    }

    private void Start()
    {
        SubscribeToTimer();
    }

    private void OnDisable()
    {
        if (isSubscribed && GameManager.Instance != null)
        {
            GameManager.Instance.OnTimerUpdated -= UpdateTimerText;
            isSubscribed = false;
        }
    }

    private void SubscribeToTimer()
    {
        if (!isSubscribed && GameManager.Instance != null)
        {
            GameManager.Instance.OnTimerUpdated += UpdateTimerText;
            isSubscribed = true;
        }
    }

    public void UpdateTimerText(float time)
    {
        timerText.text = Mathf.CeilToInt(time).ToString();
    }
}
