using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    [SerializeField] private List<GameData> gameDataList; //ゲームデータのリスト

    public bool isCleared = false;
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
        SceneManager.LoadScene("EndingScene");
    }

    private IEnumerator GameStart()
    {
        isCleared = false;
        //何かしらの演出
        yield return null;
    }

    private IEnumerator Game()
    {
        //ゲームの進行
        //GameData選択
        GameData selectedGameData = SelectGameData();

        float elapsedTime = 0f;
        while (elapsedTime < selectedGameData.timeLimit)
        {
            elapsedTime += Time.deltaTime;
            //ゲームの進行処理
            yield return null;
        }
    }

    private IEnumerator GameEnd()
    {
        //ゲーム終了後の処理
        yield return null;
    }

    private GameData SelectGameData()
    {
        int randomIndex = Random.Range(0, gameDataList.Count);
        return gameDataList[randomIndex];
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        if(sceneName == "MainGame")
        {
            StartCoroutine(GameCoroutine());
        }
    }
}
