using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public void howToPlay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("HowToPlay");
    }

    public void Credits()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Credits");
    }

    public void Mute()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Mute");
    }

    public void back()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

}
