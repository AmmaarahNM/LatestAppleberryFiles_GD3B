using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    int n;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void onPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void onExitButton()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
