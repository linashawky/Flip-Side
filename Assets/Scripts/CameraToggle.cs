using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    Camera main;
    public Camera side;

    public int rotationSpeed;

    public static bool flipped;
   // public  float playerFlipZ;
    public static float playerFlipY;

 
    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
        main.enabled = true;
        side.enabled = false;

        flipped = false;
        playerFlipY = 0.4f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            cameraSwitch();
        }


        //===========================  SWITCH PLATFORM  ==============================================================//


        if (Input.GetKeyDown(KeyCode.Space) && PlayerController.gameOver == false && PlayerController.pause == false)
        {

            flip();

        }

    }


    public void flip()
    {
        flipped = !flipped;
        main.transform.Rotate(0, 0, -180);

        if (flipped == true)
        {
            playerFlipY = 8.4f;
            //  playerFlipZ = 15.0f;
        }
        else
        {
            playerFlipY = 0.4f;
            //  playerFlipZ = -15.0f;
        }

        Debug.Log("platformSwitch");
    }

    public void cameraSwitch()
    {
        main.enabled = !main.enabled;
        side.enabled = !side.enabled;
        print("camera switch");

    }


}
