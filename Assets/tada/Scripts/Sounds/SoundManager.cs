using System.Collections;
using UnityEngine;

public enum SeType
{
    TitleStart,        // タイトル画面：開始
    SelectFish,        // 魚を選択
    Smoke,             // 魚が人にかわるときの煙
    ResultSuccess,     // 結果画面：成功
    ResultFailed,      // 結果画面：失敗
}

public enum BgmType
{
    Silent,    // 無音
    Title,     // タイトル画面
    Game,      // ゲーム中画面
    Result,    // 結果画面
}

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [Header("BGM")]
    [SerializeField] private AudioSource bgmAudioSource;
    [SerializeField] private float fadeDuration = 0.3f;
    [SerializeField] private float maxVolume = 0.2f;

    [Header("BGMファイル")]
    [SerializeField] private AudioClip bgmTitle;
    [SerializeField] private AudioClip bgmGame;
    [SerializeField] private AudioClip bgmResult;

    [Header("SE")]
    [SerializeField] private Transform seAudioSourceParent;
    [SerializeField] private GameObject seAudioSourcePrefab;

    [Header("SEファイル")]
    [SerializeField] private AudioClip seTitleStart;
    [SerializeField] private AudioClip seSelectFish;
    [SerializeField] private AudioClip seSmoke;
    [SerializeField] private AudioClip seResultSuccess;
    [SerializeField] private AudioClip seResultFailed;

    private BgmType? currentBgmType = null;
    private Coroutine plyaingBgmCoroutine = null;

    // 使い方：SoundManager.Instance.PlayBgm(BgmType.Title);
    public void PlayBgm(BgmType bgmType)
    {
        if (currentBgmType == bgmType) return;

        if (plyaingBgmCoroutine != null) StopCoroutine(plyaingBgmCoroutine);

        plyaingBgmCoroutine = StartCoroutine(PlayBgmAsync(bgmType));
    }

    private IEnumerator PlayBgmAsync(BgmType bgmType)
    {
        if (currentBgmType.HasValue)
        {
            var fromVolume = maxVolume;
            var toVolume = 0f;
            var elapsedTime = 0f;
            while(true)
            {
                elapsedTime += Time.deltaTime;
                bgmAudioSource.volume = Mathf.Lerp(fromVolume, toVolume, elapsedTime / fadeDuration);
                if (elapsedTime >= fadeDuration) break;
                yield return null;
            }
        }

        currentBgmType = bgmType;

        var clip = GetAudioClip(bgmType);
        if (clip != null)
        {
            bgmAudioSource.clip = clip;
            bgmAudioSource.Play();

            var fromVolume = 0f;
            var toVolume = maxVolume;
            var elapsedTime = 0f;
            while(true)
            {
                elapsedTime += Time.deltaTime;
                bgmAudioSource.volume = Mathf.Lerp(fromVolume, toVolume, elapsedTime / fadeDuration);
                if (elapsedTime >= fadeDuration) break;
                yield return null;
            }
        }

        plyaingBgmCoroutine = null;
    }

    private AudioClip GetAudioClip(BgmType bgmType)
    {
        return bgmType switch
        {
            BgmType.Title => bgmTitle,
            BgmType.Game => bgmGame,
            BgmType.Result => bgmResult,
            _ => null,
        };
    }

    // 使い方：SoundManager.Instance.PlaySe(SeType.SelectFish);
    public void PlaySe(SeType seType)
    {
        var clip = GetAudioClip(seType);
        if (clip == null) return;

        var gameSe = GameObject.Instantiate(seAudioSourcePrefab, seAudioSourceParent).GetComponent<GameSe>();
        gameSe.Play(clip);
    }

    private AudioClip GetAudioClip(SeType seType)
    {
        return seType switch
        {
            SeType.TitleStart => seTitleStart,
            SeType.SelectFish => seSelectFish,
            SeType.Smoke => seSmoke,
            SeType.ResultSuccess => seResultSuccess,
            SeType.ResultFailed => seResultFailed,
            _ => null,
        };
    }
}
