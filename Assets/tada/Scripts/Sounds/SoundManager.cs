using System.Collections;
using UnityEngine;

public enum SeType
{
    // TODO SE種類を登録
    SelectFish,
    ResultSuccess,
    ResultFailed,
}

public enum BgmType
{
    // TODO BGM種類を登録
    Silent,
    Title,
    Game,
    Result,
}

public class SoundManager : MonoBehaviour
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
    [SerializeField] private AudioClip seSelectFish;
    [SerializeField] private AudioClip seResultSuccess;
    [SerializeField] private AudioClip seResultFailed;

    private static SoundManager instance;
    public static SoundManager Instance => instance;

    private BgmType? currentBgmType = null;
    private Coroutine plyaingBgmCoroutine = null;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    // 使い方：SouncManager.Instance.PlayBgm(BgmType.Title);
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

    // 使い方：SouncManager.Instance.PlaySe(SeType.SelectFish);
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
            SeType.SelectFish => seSelectFish,
            SeType.ResultSuccess => seResultSuccess,
            SeType.ResultFailed => seResultFailed,
            _ => null,
        };
    }
}
