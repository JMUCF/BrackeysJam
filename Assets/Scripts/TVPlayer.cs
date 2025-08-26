using UnityEngine;
using UnityEngine.Video;

[RequireComponent (typeof(AudioSource))]
public class TVPlayer : MonoBehaviour
{

    public static TVPlayer Instance;
    public VideoClip mainMenu,transition,looping,pauseMenu;
    private VideoPlayer videoPlayer;
    private int movieShown;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        videoPlayer = GetComponent<VideoPlayer>();
    }
   public void ChangeVideo(int screenType)
   {
        movieShown = screenType;
        videoPlayer.clip = transition;
        Invoke("PlayClip", .5f);
    }
  
    private void PlayClip()
    {
        switch (movieShown)
        {
            case 0:
                videoPlayer.clip = mainMenu;
                break;
            case 1:
                videoPlayer.clip = looping;
                break;
            case 2:
                videoPlayer.clip = pauseMenu;
                break;
        }
    }
}
