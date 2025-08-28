using UnityEngine;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public static GameManager Instance;
    private MenuScript MenuScript;
    private bool playerCaught;
    public bool inEvent = false;
    private int currentLevel;
    [SerializeField] private AudioClip deathSFX;
    
    private float suspicionLevel;
    //public TMP_Text suspicionText;
    private int suspicionThreshold = 25;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        MenuScript = GetComponent<MenuScript>();
        currentLevel = 1;
    }
    void Update()
    {
        if(!inEvent)
        {
            if (playerCaught == false && player.moving)
            {
                suspicionLevel += player.speed * Time.deltaTime;
                //suspicionText.text = Mathf.FloorToInt(suspicionLevel).ToString();
            }
            else if (!playerCaught)
            {
                suspicionLevel += 1 * Time.deltaTime;
                //suspicionText.text = Mathf.FloorToInt(suspicionLevel).ToString();
            }

            if (suspicionLevel >= suspicionThreshold)
            {
                HandAnim.Instance.Check();
                suspicionLevel = 0;
            }
        }
    }

    public void CheckIfPlayerVisible()
    {
        print(player.behindCover);
        if(player.behindCover == false)
        {
            Caught();
            SFXManager.Instance.PlayAudio(deathSFX,.1f);
            return;
        }
        else
            HandAnim.Instance.ResetState();
        return;
    }

    private void Caught()
    {
        print("You got caught!");
        HandAnim.Instance.PlayerCaught();
        MenuScript.GameEndCam();
    }

    public void Won()
    {
        currentLevel++;
        print("You got the biscuit!");
    }

}