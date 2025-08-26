using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gm;
    public bool behindCover;

    public float speed;
    public bool moving;
    public bool sprinting;
    public float moveSpeed;
    public float sprintMultiplier;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sprinting = false;
        speed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speed *= sprintMultiplier;
            sprinting = true;
        }

        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        if (!gm.inEvent)
        { 
            moving = moveDirection.sqrMagnitude > 0f;
            transform.position += moveDirection * speed * Time.deltaTime;
        }
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