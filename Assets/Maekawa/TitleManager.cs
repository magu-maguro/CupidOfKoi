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
    }

    public void OnClickBackgroundPanel()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
