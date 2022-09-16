using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void LoadSelectGameModeScene()
    {
        SceneManager.LoadScene("SelectGameModeScene");
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void LoadMapScene()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void LoadLevelScene()
    {

    }

    public void LoadEndlessLevelScene()
    {

    }

    public void LoadTutorialLevelScene()
    {

    }
}