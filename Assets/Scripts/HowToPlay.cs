using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    public void back()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("OptionsMenu");
    }
}
