using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject credits;
    public GameObject MainMenuButtons;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    public void CreditsScene()
    {
        MainMenuButtons.SetActive(false);
        credits.SetActive(true);
    }
    public void Back()
    {
        credits.SetActive(false);
        MainMenuButtons.SetActive(true);
    }

    public void QuitApplication()
    {
        Application.Quit();
        print("Quitted");
    }
}
