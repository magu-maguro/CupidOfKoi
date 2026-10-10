using UnityEngine;

public class GameSe : MonoBehaviour
{
   [SerializeField] private AudioSource audioSource;

    private bool? isPlaying = null;

    public void Play(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
        isPlaying = true;
    }

    private void Update()
    {
        if (!isPlaying.HasValue) return;

        if (!audioSource.isPlaying)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
