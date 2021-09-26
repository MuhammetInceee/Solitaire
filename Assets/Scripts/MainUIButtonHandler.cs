using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject _gameEnded;
    [SerializeField] private GameObject _pauseScreem;
    [SerializeField] private GameObject _gameplayButtons;
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayAgain()
    {
        _gameEnded.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void Pause()
    {
        Counter._isPaused = true;
        _pauseScreem.SetActive(true);
        _gameplayButtons.SetActive(false);
    }

    public void Continue()
    {
        Counter._isPaused = false;
        _pauseScreem.SetActive(false);
        _gameplayButtons.SetActive(true);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Counter._score = 0;
        Counter._isPaused = false;
    }
}
