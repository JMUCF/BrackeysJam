using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public GameManager gm;
    public bool behindCover;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cover"))
        {
            behindCover = true;
            print("entered cover");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cover"))
        {
            behindCover = false;
            print("exited cover");
        }
    }
}
