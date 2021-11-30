using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameMode
{
    AIvsHuman = 0,
    AIvsAI = 1
}
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] Text toggleModeButtonText;
    public GameMode gameMode;

    void Start() {
        pauseMenuUI.SetActive(false);
        gameMode = GameMode.AIvsHuman;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Restart() {
        // Reload the scence
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void ToggleMode() {
        if (gameMode == GameMode.AIvsHuman) {
            gameMode = GameMode.AIvsAI;
            toggleModeButtonText.text = "MODE: AI VS AI";
        } else {
            gameMode = GameMode.AIvsHuman;
            toggleModeButtonText.text = "MODE: AI VS HUMAN";
        }
    }

    public void Quit() {
        Debug.Log("QUITTING GAME");
        Application.Quit();
    }
}
