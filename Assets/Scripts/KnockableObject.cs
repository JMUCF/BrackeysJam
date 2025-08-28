using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class KnockableObject : MonoBehaviour
{
    private GameManager gm;
    private PlayerController player;

    private List<string> sequence;   
    private int index;               
    private bool eventActive;
    private bool waitingForRelease;  
    private bool waitingForInitialRelease; 

    public Sprite leftSprite;
    public Sprite rightSprite;
    public Image arrowUI;
    public float pulseScale = 1.2f;
    public float pulseDuration = 0.15f;
    private Vector3 originalScale;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        originalScale = arrowUI.rectTransform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && player.speed >= 6)
        {
            Debug.Log("Player Collided!");
            arrowUI.enabled = true;
            KnockedOver();
        }
    }

    private void KnockedOver()
    {
        gm.inEvent = true;
        eventActive = true;
        index = 0;
        waitingForRelease = false;
        waitingForInitialRelease = true;

        sequence = new List<string>();
        for (int i = 0; i < 6; i++)
        {
            sequence.Add(Random.value > 0.5f ? "Left" : "Right");
        }

        Debug.Log("Sequence generated: " + string.Join(", ", sequence));
        UpdateArrowUI();
    }

    void Update()
    {
        if (!eventActive || !gm.inEvent) return;

        float input = Input.GetAxisRaw("Horizontal");

        if (waitingForInitialRelease)
        {
            if (input == 0f)
            {
                waitingForInitialRelease = false;
                Debug.Log("Input released. Event is now active.");
            }
            return;
        }

        if (!waitingForRelease)
        {
            if (input != 0f)
            {
                string expected = sequence[index];
                string actual = (input < 0f) ? "Left" : "Right";

                if (actual == expected)
                {
                    Debug.Log("Correct: " + actual);
                    StartCoroutine(PulseArrow());

                    index++;
                    waitingForRelease = true;

                    if (index >= sequence.Count)
                    {
                        Debug.Log("Sequence complete! Event cleared.");
                        EventWin();
                    }
                    else
                        UpdateArrowUI();
                }
                else
                {
                    Debug.Log("Wrong input! Event failed.");
                    EventLose();
                }
            }
        }
        else
        {
            if (input == 0f)
            {
                waitingForRelease = false;
            }
        }
    }

    private void UpdateArrowUI()
    {
        string expected = sequence[index];
        arrowUI.sprite = expected == "Left" ? leftSprite : rightSprite;
    }

    private void EventWin()
    {
        EndEvent();
    }

    private void EventLose()
    {

        EndEvent();
    }

    private void EndEvent()
    {
        gm.inEvent = false;
        eventActive = false;
        sequence = null;
        index = 0;
        waitingForRelease = false;
        waitingForInitialRelease = false;
        arrowUI.enabled = false;
    }

    private IEnumerator PulseArrow()
    {
        RectTransform rect = arrowUI.rectTransform;

        float elapsed = 0f;
        while (elapsed < pulseDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / pulseDuration;
            rect.localScale = Vector3.Lerp(originalScale, originalScale * pulseScale, t);
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < pulseDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / pulseDuration;
            rect.localScale = Vector3.Lerp(originalScale * pulseScale, originalScale, t);
            yield return null;
        }

        rect.localScale = originalScale;
    }
}
