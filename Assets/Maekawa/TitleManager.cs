using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName = "temp";
    void Start()
    {
        
    }

    void Update()
    {
        //マウスクリックでシーン遷移
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
