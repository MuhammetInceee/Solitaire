using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIButtonHandler : MonoBehaviour
{
    [SerializeField] private GameObject _gameEnded;
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayAgain()
    {
        _gameEnded.SetActive(false);
        SceneManager.LoadScene(1);
    }

}
