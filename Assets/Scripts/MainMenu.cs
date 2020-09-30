using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void StartTheGame ()
    {
        // The Scene must be added to the build in any case!
        // SceneManager.LoadScene(arg) could alternatively be called with :
        //   arg = 1  <--- number of the Scene you want to load
        //   arg = "Level01"  <--- name of the Scene
        // 
        SceneManager.LoadScene("animated_button");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void QuitGame ()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
