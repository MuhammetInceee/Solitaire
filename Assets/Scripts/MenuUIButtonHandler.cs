using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIButtonHandler : MonoBehaviour
{
    public void Easy()
    {
        DeckManager._levelSelector = 1;
        SceneManager.LoadScene(1);
    }
    public void Medium()
    {
        DeckManager._levelSelector = 2;
        SceneManager.LoadScene(1);
    }
    public void Hard()
    {
        DeckManager._levelSelector = 3;
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
