using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour

{
    public int healths;
    public static bool gameOver;
  //  public GameObject gameOverPanel;
    //public Text score;


    void Start()
    {
        gameOver = false;


    }

    // Update is called once per frame
    void Update()

    {
        if (healths <= 0)
        {
            gameOver = true;
        }
        if (gameOver)
        {
            Time.timeScale = 0;
          //  gameOverPanel.SetActive(true);
        }
    }

}
