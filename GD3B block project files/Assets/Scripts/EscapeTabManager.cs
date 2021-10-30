using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeTabManager : MonoBehaviour
{
    public GameManager GM;
    public void ReturnToGame()
    {
        GM.gamePaused = false;
        GM.escapeTab.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        //GM.gamePaused = false;
        GM.escapeTab.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        GM.gamePaused = false;
        GM.escapeTab.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}
