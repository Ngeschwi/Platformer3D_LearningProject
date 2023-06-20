using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    private bool _isPaused = false;
    public GameObject _menuPanel;
    public GameObject _miniMap;
    
    public void Update() {
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (_isPaused)
                UnpauseGame();
            else
                PauseGame();
        }
    }

    public void PauseGame() {
        
        _isPaused = true;
        _menuPanel.SetActive(true);
        Time.timeScale = 0f;
        ObjectivesScript._InstanceObjectivesScript.RefreshObjectives();
        _miniMap.SetActive(true);
    }
    
    public void UnpauseGame() {
        
        _isPaused = false;
        _menuPanel.SetActive(false);
        Time.timeScale = 1f;
        _miniMap.SetActive(false);
    }

    public void RestartGame() {
        
        UnpauseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void QuitGame() {
        
        Application.Quit();
    }
}