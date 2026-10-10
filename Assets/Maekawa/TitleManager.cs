using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "temp";
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject button;
    private bool canChangeScene = false;
    void Start()
    {
        text.SetActive(false);
        button.SetActive(false);
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
    }

    void OnDisable()
    {
        videoPlayer.loopPointReached -= OnVideoEnd;
    }

    public void OnClickBackgroundPanel()
    {
        if(canChangeScene)
        {
            GameManager.Instance.ChangeScene(nextSceneName);
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        canChangeScene = true;
        text.SetActive(true);
        button.SetActive(true);
    }
}
