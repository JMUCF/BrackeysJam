using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class HandAnim : MonoBehaviour
{
    public static HandAnim Instance;
    private Animator _animator;
    private AudioSource audioSource;
    public bool active;
    // Update is called once per frame
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        active = true;
    }
    
    private void Update()
    {
        if (InputManager.Instance.MenuInput && active)
        {
            Click();
        }
    }
    public void Click()
    {
        if(_animator != null)
        {
            _animator.SetTrigger("Click");
            audioSource.Play();
        }
    }
    public void ArmAnim(bool open)
    {
        
        if (_animator != null)
        {
            _animator.SetBool("MenuOpen", open);
        }
    }
    public void PlayerCaught()
    {
        if (_animator != null)
        {
            _animator.SetBool("Caught", true);
        }
    }
    public void CheckIfVisible()
    {
        GameManager.Instance.CheckIfPlayerVisible();
    }
    public void Check()
    {
        if (_animator != null)
        {
            _animator.SetBool("Look",true);
        }
    }
    public void ResetState()
    {
        _animator.SetBool("Look", false);
    }
}
