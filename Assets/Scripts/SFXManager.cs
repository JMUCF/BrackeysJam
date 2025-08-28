using UnityEngine;
[RequireComponent (typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip clip, float vol)
    {
        audioSource.volume = vol;
        audioSource.PlayOneShot(clip);
    }
}
