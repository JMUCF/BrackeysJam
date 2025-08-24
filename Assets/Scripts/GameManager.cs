using UnityEngine;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerController player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine("CheckIfPlayerVisible");
    }

    private IEnumerator CheckIfPlayerVisible()
    {
        while (true)
        {
            print(player.behindCover);

            yield return new WaitForSeconds(Random.Range(3f, 7f));
        }
    }
}
