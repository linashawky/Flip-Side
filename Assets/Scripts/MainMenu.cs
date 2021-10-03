using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("level");
    }
    public void options()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("OptionsMenu");
    }
    public void QuitButton()
    {
        Application.Quit();
        //EditorApplication.Exit(0);
    }

}
