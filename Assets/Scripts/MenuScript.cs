using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour
{
    [Header("Menu Objects")]
    [SerializeField] private GameObject  MainMenu, PauseMenu; 

    private bool isPaused,gameStarted;

    [Header("Cameras")]
    public CinemachineCamera gameCam,menuCam;

    [Header("First Selected Options")]
    [SerializeField] private GameObject mainMenuFirst, pauseMenuFirst;

    [Header("Gameplay Systems")]
    [SerializeField] private PlayerController playerController;
    private GameManager gameManager;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        GameStartUp();
        gameStarted = false;
        playerController.enabled = false;
        gameManager.enabled = false;
    }
    private void GameStartUp() 
    {
        GameCamOff();
        OpenMainMenu();
        TVPlayer.Instance.ChangeVideo(0);
    }
    #region UI Buttons
    public void StartGame()
    {
        gameManager.enabled = true;
        playerController.enabled = true;
        GameCamOn();
        CloseAllMenus();
        gameStarted = true;
        TVPlayer.Instance.ChangeVideo(1);
    }
    public void ResumeGame()
    {
        GameCamOn();
        CloseAllMenus();
        TVPlayer.Instance.ChangeVideo(1);
    }
    public void QuitToMain()
    {
        OpenMainMenu();
        TVPlayer.Instance.ChangeVideo(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
    private void Update()
    {
        if (InputManager.Instance.MenuOpenCloseInput && gameStarted)
        {
            if (!isPaused) 
            {
                TVPlayer.Instance.ChangeVideo(2);
                Pause(); 
                GameCamOff();
                OpenPauseMenu();
            }
            else 
            {
                TVPlayer.Instance.ChangeVideo(1);
                UnPause();
                GameCamOn();
                CloseAllMenus();
            }
        }
        
    }
    #region Pause Functions
    private void Pause() 
    { 
        isPaused = true;
        gameManager.enabled = false;
        playerController.enabled = false;

    }
    private void UnPause() 
    {
        isPaused = false;
        gameManager.enabled = true;
        playerController.enabled = true;
    }
    #endregion

    #region Canvas Activations
    private void OpenMainMenu()
    {
        MainMenu.SetActive(true);
        PauseMenu.SetActive(false);
        gameStarted = false;
        EventSystem.current.SetSelectedGameObject(mainMenuFirst);
    }
    private void OpenPauseMenu()
    {
        MainMenu.SetActive(false);
        PauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(pauseMenuFirst);
    }
    private void CloseAllMenus()
    {
        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
    #endregion

    #region Camera Control
    private void GameCamOff()
    {
        gameCam.Priority = 0;
        menuCam.Priority = 20;
        HandAnim.Instance.ArmAnim(true);
        HandAnim.Instance.active = true;
    }
    private void GameCamOn()
    {
        gameCam.Priority = 20;
        menuCam.Priority = 0;
        HandAnim.Instance.ArmAnim(false);
        HandAnim.Instance.active = false;
        gameManager.enabled = true;
        playerController.enabled = true;
    }
    #endregion
}
