using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance => instance;

    protected virtual void Awake()
    {
        // すでにあるなら自身を削除
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        // ジェネリック型にキャストして登録
        instance = this as T;

        // シーンをまたぐので残しておく
        DontDestroyOnLoad(this.gameObject);
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
