using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEditor.Build.Content;
using System.Collections.Generic;

public class EndingManager : MonoBehaviour
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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void OnSceneChanged()
    {
        bool isSuccess = GameManager.Instance != null ? GameManager.Instance.isCleared : true;
        StartCoroutine(PlayResultAsync(isSuccess));
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

    }

    void Start()
    {
         StartCoroutine(PlayResultAsync(true));
        //ShowInitialEnding();
    }

    void ShowInitialEnding()
    {
        bool Clear = GameManager.Instance.isCleared;
        //bool Clear = true;

        if(Clear)
        {
            goodendimage.gameObject.SetActive(true);
            goodendingkyaras[1].gameObject.SetActive(true);
            titleButton.gameObject.SetActive(true);
            GoodEndingMessage.gameObject.SetActive(true);
        }
        else
        {
            badendimage.gameObject.SetActive(true);
            badendingkyaras[2].gameObject.SetActive(true);
            titleButton.gameObject.SetActive(true);
            BadEndingMessage.gameObject.SetActive(true);
        }
    }

    public void onclicktitleButton()
    {
        GameManager.Instance.ChangeScene("Title");
    }

    public void OnSceneLoaded()
    {
        SoundManager.Instance.PlayBgm(BgmType.Result);
    }
}