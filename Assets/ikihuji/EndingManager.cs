using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Build.Content;
using System.Collections.Generic;

public class EndingManager : MonoBehaviour
{
    public List<GameObject> goodendingkyaras;
    public List<GameObject> badendingkyaras;
    public GameObject goodendimage;
    public GameObject badendimage;

    public GameObject titleButton;

    public TextMeshProUGUI GoodEndingMessage;
    public TextMeshProUGUI BadEndingMessage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        titleButton.gameObject.SetActive(false);

        goodendimage.gameObject.SetActive(false);
        badendimage.gameObject.SetActive(false);

        goodendingkyaras[0].gameObject.SetActive(false);
        badendingkyaras[0].gameObject.SetActive(false);

        ShowInitialEnding();

    }

    void ShowInitialEnding()
    {
        //bool Clear = GameManager.Instance.isCleared;
        bool Clear = true;

        if(Clear)
        {
            goodendimage.gameObject.SetActive(true);
            goodendingkyaras[0].gameObject.SetActive(true);
            titleButton.gameObject.SetActive(true);
            GoodEndingMessage.gameObject.SetActive(true);
        }
        else
        {
            badendimage.gameObject.SetActive(true);
            badendingkyaras[0].gameObject.SetActive(true);
            titleButton.gameObject.SetActive(true);
            BadEndingMessage.gameObject.SetActive(true);
        }
    }

    public void onclicktitleButton()
    {
        GameManager.Instance.ChangeScene("Title");
    }
}