using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public enum GameState
{
    Title, GameStart, Game, GameEnd, Result
}

/// <summary>
/// ゲームの進行状況など管理
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState CurrentGameState { get; private set; } = GameState.Title;
    public event Action<float> OnTimerUpdated;

    [SerializeField] private List<GameData> gameDataList; //ゲームデータのリスト

    
    private GameData selectedGameData;

    [Header("スタート演出用")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject smallPanel;
    [SerializeField] private GameObject startText;

    //[Header("終了演出用")]

    [Header("エンディングに渡すやつ")]
    public bool isCleared = false;
    public Material selectedBodyMaterial;
    public Material selectedEyeMaterial;
    public bool gameEnded = false;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        startPanel.SetActive(false);
        smallPanel.SetActive(false);
        startText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GameCoroutine()
    {
        Debug.Log("GameCoroutine Started!!!");
        CurrentGameState = GameState.GameStart;
        yield return StartCoroutine(GameStart());

        CurrentGameState = GameState.Game;
        yield return StartCoroutine(Game());

        CurrentGameState = GameState.GameEnd;
        yield return StartCoroutine(GameEnd());

        CurrentGameState = GameState.Result;
        DeactivateCarps();
        SceneManager.LoadScene("EndingScene");
    }

    private IEnumerator GameStart()
    {
        //GameData選択・スポーン処理
        selectedGameData = SelectGameData();
        GetComponent<CarpSpawner>().SpawnCarps(selectedGameData.koiCount);


        isCleared = false;
        //パネル表示、2秒待機、パネル非表示&右上小さいパネル表示&スタート表示
        startPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        startPanel.SetActive(false);
        smallPanel.SetActive(true);
        startText.SetActive(true);
        //ここ効果音
        yield return new WaitForSeconds(1f);
        startText.SetActive(false);
    }

    private IEnumerator Game()
    {
        //ゲームの進行
        float elapsedTime = 0f;
        while (elapsedTime < selectedGameData.timeLimit)
        {
            if(gameEnded)
            {
                break;
            }
            //タイマー処理
            elapsedTime += Time.deltaTime;
            float remainingTime = Mathf.Max(0f, selectedGameData.timeLimit - elapsedTime);
            OnTimerUpdated?.Invoke(remainingTime);

            //ゲームの進行処理
            yield return null;
        }
    }

    private IEnumerator GameEnd()
    {
        //ゲーム終了後の処理
        smallPanel.SetActive(false);
        yield return null;
    }

    private GameData SelectGameData()
    {
        int randomIndex = UnityEngine.Random.Range(0, gameDataList.Count);
        Debug.Log($"Selected GameData: {gameDataList[randomIndex].name}");
        return gameDataList[randomIndex];
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        if(sceneName == "MainGame")
        {
            StartCoroutine(GameCoroutine());
        }
        else if(sceneName == "Title")
        {
            ResetGame();
        }
    }

    public void SetSelectedMaterials(Material bodyMaterial, Material eyeMaterial)
    {
        selectedBodyMaterial = bodyMaterial;
        selectedEyeMaterial = eyeMaterial;
    }

    private void DeactivateCarps()
    {
        GetComponent<CarpSpawner>().ClearCarps();
    }

    public void ResetGame()
    {
        //ゲームのリセット処理
        isCleared = false;
        selectedBodyMaterial = null;
        selectedEyeMaterial = null;

        GetComponent<CarpSpawner>().ClearCarps();
    }
}
