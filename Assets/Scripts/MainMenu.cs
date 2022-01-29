using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(int n)
    {
        SceneManager.LoadScene(n /*Cambiar escena para el juego nuevo*/);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
