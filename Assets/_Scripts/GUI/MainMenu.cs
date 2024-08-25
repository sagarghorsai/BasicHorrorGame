using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }
    public void SettingMenu()
    {
        //
    }
    public void MainMenuScreen()
    {
        //
    }
    public void PlayMultiplayer()
    {
        SceneManager.LoadScene("ConnectToServer");

    }

    public void QuitGame()
    {
        Application.Quit();
    }




}
