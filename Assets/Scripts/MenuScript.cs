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


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameStartUp();
        gameStarted = false;
    }
    private void GameStartUp() 
    {
        GameCamOff();
        OpenMainMenu();
    }
    #region UI Buttons
    public void StartGame()
    {
        GameCamOn();
        CloseAllMenus();
        gameStarted = true;
    }
    public void ResumeGame()
    {
        GameCamOn();
        CloseAllMenus();
    }
    public void QuitToMain()
    {
        OpenMainMenu();
    }
    #endregion
    private void Update()
    {
        if (InputManager.Instance.MenuOpenCloseInput && gameStarted)
        {
            if (!isPaused) 
            { 
                Pause(); 
                GameCamOff();
                OpenPauseMenu();
            }
            else 
            { 
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
        Time.timeScale = 0.0f;

    }
    private void UnPause() 
    {
        isPaused = false;
        Time.timeScale = 1.0f;
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
    }
    private void GameCamOn()
    {
        gameCam.Priority = 20;
        menuCam.Priority = 0;
    }
    #endregion
}
