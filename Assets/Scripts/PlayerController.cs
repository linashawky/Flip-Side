using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class PlayerController : MonoBehaviour
{
    // player moving forward
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;


    // player switch lanes
    private int desiredLane = 1; // 0: leftLane 1: middleLane 2: rightLane  
    public float laneDistance = 4; // distance between 2 lanes

    public Text health;
    public int healths;
    
    public Text scoreText;
    public int score;


    // change player color
    public Material Yellow;
    public Material Blue;
    public Material Red;
    private IEnumerator SwitchPlayerColorCoroutine;

    //game over panel
    public GameObject gameOverPanel;
    public static bool gameOver;

    //pause panel
    public GameObject pausePanel;
    float tempSpeed;
    public static bool pause;

    // audio
    public AudioSource gameOverAudio;
    public AudioSource pauseAudio;
    public AudioSource bounceAudio;
    public AudioSource healthAudio;
    public AudioSource coinAudio;
    public AudioSource flipAudio;
    public AudioSource ColorAudio;


    //flip mode
    public bool normalMode;
    public Light directlight ;
    int angle;
    public Text flipMode;

   

    //======================================== Start ====================================================================//

    void Start()
    {
        controller = GetComponent<CharacterController>();
        healths = 3;
        health.text = "Healths: 3";
        score = 0;
        scoreText.text = "Score: 0";
        normalMode = true;
        pause = false;
        gameOver = false;
        flipMode.text = "FLipped Mode: off";

        //transform.Translate(0, 0.4f, 0);
        forwardSpeed = 10;

        SwitchPlayerColorCoroutine = SwitchPlayerColor(15.0f);
        StartCoroutine(SwitchPlayerColorCoroutine);

        directlight.transform.Rotate(72, 0, 0);



        // Debug.Log("normal mode->>"+normalMode);



    }

    //===============================  PLAYER SWITCH COLORS  ==================================================================//

    IEnumerator SwitchPlayerColor(float waitTime)
    {
        while (true)
        {

            yield return new WaitForSeconds(waitTime);
            ColorAudio.Play();
            GetComponent<Renderer>().material = Yellow;
            yield return new WaitForSeconds(waitTime);
            ColorAudio.Play();
            GetComponent<Renderer>().material = Red;
            yield return new WaitForSeconds(waitTime);
            ColorAudio.Play();
            GetComponent<Renderer>().material = Blue;

        }
    }

    //======================================== FixedUpdate ====================================================================//

    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);

    }

    //======================================== on trigger ====================================================================//

    public void OnTriggerEnter(Collider other)
    {
        //===============================  OBSTACLES  =========================================================================//

        if (other.transform.tag == "Obstacle")
        {
            bounceAudio.Play();
            if (healths > 0)
                healths--;
            health.text = "Health: " + healths;
            other.gameObject.SetActive(false);

            if (healths == 0)
            {
                gameOverPanel.gameObject.SetActive(true);
                gameOver = true;
                forwardSpeed = 0;
                gameOverAudio.Play();
            }

        }
      
        
        //=============================== HEALTH COLLECTABLES  ================================================================//

        else
        {
            if (other.transform.tag == "HealthOrb")
            {
                healthAudio.Play();
                if (healths < 3)
                    healths++;
                health.text = "Health: " + healths;
                other.gameObject.SetActive(false);


            }
          
         //=============================== SCORE COLLECTABLES  ================================================================//

            else
            {

                if (normalMode == true)
                {
                    if (other.transform.tag == "CollectableBlue")
                    {
                        // Debug.Log(other.transform.tag);
                        //  Debug.Log(GetComponent<Renderer>().material);

                        if (GetComponent<Renderer>().material.color == Blue.color)
                        {
                            coinAudio.Play();
                            score += 10;
                            if (score % 50 == 0)
                            {
                                forwardSpeed += 5;
                                Debug.Log(forwardSpeed);

                            }
                        }
                        else
                        { // Debug.Log(" nooott BLUEEEE");
                            bounceAudio.Play();
                            score -= 5;
                        }
                        scoreText.text = "Score: " + score;
                        other.gameObject.SetActive(false);
                    }
                    else
                    {
                        if (other.transform.tag == "CollectableYellow")
                        {
                            if (GetComponent<Renderer>().material.color == Yellow.color)
                            {
                                coinAudio.Play();

                                score += 10;
                                if (score % 50 == 0)
                                {
                                    forwardSpeed += 5;
                                    Debug.Log(forwardSpeed);

                                }
                            }
                            else { score -= 5; bounceAudio.Play(); }
                            scoreText.text = "Score: " + score;
                            other.gameObject.SetActive(false);
                        }
                        else
                        {
                            if (other.transform.tag == "CollectableRed")
                            {
                                if (GetComponent<Renderer>().material.color == Red.color)
                                {
                                    coinAudio.Play();

                                    score += 10;
                                    if (score % 50 == 0)
                                    {
                                        forwardSpeed += 5;
                                        Debug.Log(forwardSpeed);

                                    }
                                }
                                else { score -= 5; bounceAudio.Play(); }
                                scoreText.text = "Score: " + score;
                                other.gameObject.SetActive(false);
                            }
                        }

                    }

                }
                else
                {
                    if (other.transform.tag == "CollectableBlue")
                    {
                        // Debug.Log(other.transform.tag);
                        //  Debug.Log(GetComponent<Renderer>().material);

                        if (GetComponent<Renderer>().material.color != Blue.color)
                        {
                            coinAudio.Play();

                            score += 10;
                            if (score % 50 == 0)
                            {
                                forwardSpeed += 5;
                                Debug.Log(forwardSpeed);

                            }
                        }
                        else
                        { // Debug.Log(" nooott BLUEEEE");
                            score -= 5; bounceAudio.Play();
                        }
                        scoreText.text = "Score: " + score;
                        other.gameObject.SetActive(false);
                    }
                    else
                    {
                        if (other.transform.tag == "CollectableYellow")
                        {
                            if (GetComponent<Renderer>().material.color != Yellow.color)
                            {
                                coinAudio.Play();

                                score += 10;
                                if (score % 50 == 0)
                                {
                                    forwardSpeed += 5;
                                    Debug.Log(forwardSpeed);

                                }
                            }
                            else { score -= 5; bounceAudio.Play(); }
                            scoreText.text = "Score: " + score;
                            other.gameObject.SetActive(false);
                        }
                        else
                        {
                            if (other.transform.tag == "CollectableRed")
                            {
                                if (GetComponent<Renderer>().material.color != Red.color)
                                {
                                    coinAudio.Play();

                                    score += 10;
                                    if (score % 50 == 0)
                                    {
                                        forwardSpeed += 5;
                                        Debug.Log(forwardSpeed);

                                    }
                                }
                                else { score -= 5; bounceAudio.Play(); }
                                scoreText.text = "Score: " + score;
                                other.gameObject.SetActive(false);
                            }
                        }

                    }

                }

            }

        }

    }

    //======================================== Update ====================================================================//

    void Update()
    {
        //===============================  PLAYER MOVEMENT ==================================================================//

        // for player forward move
        direction.z = forwardSpeed;

        // gather inputs on which lane we should be
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || SwipeManager.swipeRight ) && gameOver == false && pause == false)
        {
            if ( normalMode == true)
            {
                desiredLane++;
                if (desiredLane == 3)
                    desiredLane = 2;
            }
            else
            {
                desiredLane--;
                if (desiredLane == -1)
                    desiredLane = 0;
            }

        }

        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || SwipeManager.swipeLeft) && gameOver == false && pause == false)
        {
            if (normalMode == true)
            {
                desiredLane--;
                if (desiredLane == -1)
                    desiredLane = 0;
            }
            else
            {
                desiredLane++;
                if (desiredLane == 3)
                    desiredLane = 2;
            }
        }

        //----------------------------------------------------------------------------------------//
        // calculate where we should be in the future

        Vector3 flip = new Vector3(0, CameraToggle.playerFlipY, 0);
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * flip;

        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
          targetPosition += Vector3.right * laneDistance;
        }


        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir;
        if (normalMode)
        {  moveDir = diff.normalized * 5 * Time.deltaTime; }
        else
        {  moveDir = diff.normalized * 80 * Time.deltaTime; }

        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);

        //===============================  PAUSE ===========================================================================//

        if( (Input.GetKeyDown(KeyCode.Escape) || SwipeManager.swipeDown) && gameOver == false)
        {
            if (pause == false)
            {
                pausePanel.gameObject.SetActive(true);
                tempSpeed = forwardSpeed;
                forwardSpeed = 0;
                pause = true;
                pauseAudio.Play();
            }
            else
            {
                pausePanel.gameObject.SetActive(false);
                forwardSpeed = tempSpeed;
                tempSpeed = 0;
                pause = false;

            }
        }


        //===============================  CHEATS =========================================================================//

        if (Input.GetKeyDown(KeyCode.E) && gameOver == false && pause == false)
        {
            Debug.Log("helloo");
            if (healths < 3)
                healths++;
            healthAudio.Play();
            health.text = "Health: " + healths;
            
        }


        if (Input.GetKeyDown(KeyCode.Q) && gameOver == false && pause == false)
        {
            score += 10;
            coinAudio.Play();
            if (score % 50 == 0)
            {
                forwardSpeed += 5;
                Debug.Log(forwardSpeed);


            }
            scoreText.text = "Score: " + score;
        }


        if (Input.GetKeyDown(KeyCode.R) && gameOver == false && pause == false)
        {
            Debug.Log("hiiii");

            if (GetComponent<Renderer>().material.color == Blue.color)
                GetComponent<Renderer>().material = Yellow;
            else
            {
                if (GetComponent<Renderer>().material.color == Yellow.color)
                    GetComponent<Renderer>().material = Red;
                else
                {
                    if (GetComponent<Renderer>().material.color == Red.color)
                        GetComponent<Renderer>().material = Blue;
                }
            }
        }

      
        //===========================  SWITCH PLATFORM  ==================================================================//

        if (CameraToggle.flipped == false)
        {
            normalMode = true;
            flipAudio.Play();
            flipMode.text = "FLipped Mode: Off";
         //   transform.Translate(0, 0, CameraToggle.playerFlipZ);
           // transform.Translate(0, CameraToggle.playerFlipY, 0);
        }
        else
        {
            if (CameraToggle.flipped == true)
            {
                normalMode = false;
                flipAudio.Play();
                flipMode.text = "FLipped Mode: On";
                //   transform.Translate(0, 0, CameraToggle.playerFlipZ);
                //    transform.Translate(0, CameraToggle.playerFlipY, 0);
            }
        }

    }




}