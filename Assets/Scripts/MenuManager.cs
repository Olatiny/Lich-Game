using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField][Tooltip("The main menu canvas")]private GameObject mainMenuCanvas;
    [SerializeField][Tooltip("The pause menu canvas")]private GameObject pauseMenuCanvas;
    [SerializeField][Tooltip("The in-game UI canvas")]private GameObject inGameCanvas;

    // Start is called before the first frame update
    void Start()
    {
        OpenStartMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenStartMenu(){
        mainMenuCanvas.SetActive(true);
        pauseMenuCanvas.SetActive(false);
        inGameCanvas.SetActive(false);
    }

    public void CloseStartMenu(){
        mainMenuCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
    }

    public void OpenPauseMenu(){
        mainMenuCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);
        inGameCanvas.SetActive(false);
    }

    public void ClosePauseMenu(){
        mainMenuCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
    }

    public void ResumeGame(){
        GameManager.Instance.UnPause();
    }

    public void PauseGame(){
        GameManager.Instance.Pause();
    }

    public void QuitToMenu(){
        GameManager.Instance.ResetScene();
        OpenStartMenu();
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void StartGame(){
        CloseStartMenu();
        GameManager.Instance.StartFalling();
    }
}
