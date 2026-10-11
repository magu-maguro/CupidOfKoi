using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TitleManager : MonoBehaviour
{
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
            SoundManager.Instance.PlaySe(SeType.TitleStart);
            SceneChanger.Instance.ChangeScene(SceneName.Game);
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        canChangeScene = true;
        text.SetActive(true);
        button.SetActive(true);
        SoundManager.Instance.PlayBgm(BgmType.Title);
    }
}
