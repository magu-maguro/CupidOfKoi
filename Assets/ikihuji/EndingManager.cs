using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EndingManager : MonoBehaviour, ISceneChanged, ISceneLoaded, ISceneLoading
{
    public GameObject fish;
    public GameObject smoke;

    public List<GameObject> goodendingkyaras;
    public List<GameObject> badendingkyaras;
    public GameObject goodendimage;
    public GameObject badendimage;

    public GameObject titleButton;

    public GameObject GoodEndingMessage;
    public GameObject BadEndingMessage;

    public bool isDummySuccess = true;

    public void OnSceneLoaded()
    {
        SoundManager.Instance.PlayBgm(BgmType.Result);

        Initialize();

    }

    private void Initialize()
    {
        fish.SetActive(false);
        smoke.SetActive(false);
        goodendingkyaras.ForEach(k => k.SetActive(false));
        badendingkyaras.ForEach(k => k.SetActive(false));
        goodendimage.SetActive(false);
        badendimage.SetActive(false);
        titleButton.SetActive(false);
        GoodEndingMessage.SetActive(false);
        BadEndingMessage.SetActive(false);
    }

    public void OnSceneChanged()
    {
        bool isSuccess = GameManager.Instance != null ? GameManager.Instance.isCleared : isDummySuccess;
        StartCoroutine(PlayResultAsync(isSuccess));
    }

    public void OnSceneLoading()
    {
        titleButton.SetActive(false);
    }

    private IEnumerator PlayResultAsync(bool isSuccess)
    {

        fish.SetActive(true);
        yield return new WaitForSeconds(1f);
        smoke.SetActive(true);
        yield return new WaitForSeconds(1f);
        goodendingkyaras[0].SetActive(isSuccess);
        badendingkyaras[0].SetActive(!isSuccess);
        fish.SetActive(false);
        titleButton.gameObject.SetActive(true);
        GoodEndingMessage.SetActive(isSuccess);
        BadEndingMessage.SetActive(!isSuccess);
        SoundManager.Instance.PlaySe(isSuccess
        ? SeType.ResultSuccess
        : SeType.ResultFailed);

        titleButton.SetActive(true);
    }

    public void OnClickTitleButton()
    {
        SceneChanger.Instance.ChangeScene(SceneName.Title);
    }
}