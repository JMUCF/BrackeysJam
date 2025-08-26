using UnityEngine;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    private bool playerCaught;
    public bool inEvent = false;
    
    private float suspicionLevel;
    public TMP_Text suspicionText;
    private int suspicionThreshold = 25;

    void Update()
    {
        if(!inEvent)
        {
            if (playerCaught == false && player.moving)
            {
                suspicionLevel += player.speed * Time.deltaTime;
                suspicionText.text = Mathf.FloorToInt(suspicionLevel).ToString();
            }
            else if (!playerCaught)
            {
                suspicionLevel += 1 * Time.deltaTime;
                suspicionText.text = Mathf.FloorToInt(suspicionLevel).ToString();
            }

            if (suspicionLevel >= suspicionThreshold)
            {
                CheckIfPlayerVisible();
                suspicionLevel = 0;
            }
        }
    }

    private void CheckIfPlayerVisible()
    {
        print(player.behindCover);
        if(player.behindCover == false)
        {
            Caught();
            return;
        }
        else
            return;
    }

    private void Caught()
    {
        print("You got caught!");
    }

}