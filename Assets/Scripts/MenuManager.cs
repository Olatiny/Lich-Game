using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField][Tooltip("The main menu canvas")]private GameObject mainMenuCanvas;
    [SerializeField][Tooltip("The pause menu canvas")]private GameObject pauseMenuCanvas;
    [SerializeField][Tooltip("The in-game UI canvas")]private GameObject inGameCanvas;
    [SerializeField][Tooltip("the score field in the game over canvas")]private TMP_Text inGameScoreText;
    [SerializeField][Tooltip("the game over canvas")]private GameObject gameOverCanvas;
    [SerializeField][Tooltip("the score field in the game over canvas")]private TMP_Text gameOverScoreText;


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
        gameOverCanvas.SetActive(false);
        inGameScoreText.text = "Score: 0";
    }

    public void CloseStartMenu(){
        mainMenuCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
        gameOverScoreText.text = "Score: 0";
    }

    public void OpenPauseMenu(){
        mainMenuCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(true);
        inGameCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    public void ClosePauseMenu(){
        mainMenuCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
        gameOverCanvas.SetActive(false);
    }

    public void OpenGameOverMenu(){
        mainMenuCanvas.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        inGameCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        gameOverScoreText.text = "Score: " + GameManager.Instance.GetScore();
    }

    public void UpdateScore(int score){
        inGameScoreText.text = "Score: " + score;
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
        GameManager.Instance.StartGame();
    }

    public void Die(){
        GameManager.Instance.Die();
        OpenGameOverMenu();
    }
}
