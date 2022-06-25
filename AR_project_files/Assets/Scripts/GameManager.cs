using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    float countdownTime = 3;
    public Text countdownDisplay;

    public bool carSpawned;
    public bool raceTime;
    public bool gameOver;
    public GameObject placementUI;
    public GameObject movementUI;
    public GameObject raceUI;
    public GameObject gameOverUI; 

    public CameraShake CameraShake;
    public AudioSource crash;
    public Timer Timer;
    public Text finalTime;

    int health = 3;
    public GameObject[] healthIcon;

    public AudioSource lose;


 
    // Start is called before the first frame update
    void Start()
    {
        carSpawned = false;
        raceTime = false;
        gameOver = false;
        finalTime.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        placementUI.SetActive(!carSpawned && !gameOver);
        
        movementUI.SetActive(carSpawned && !raceTime && !gameOver);
        
        if (movementUI.activeSelf == true )
        {
            /*StartCoroutine(RaceUI());

            if (countdownTime > 0 && raceUI.activeSelf == true)
            {
                countdownTime -= 1 * Time.deltaTime;
                countdownDisplay.text = countdownTime.ToString("0");
            }

            else 
            {
                StartCoroutine(RaceCountdown());

            }*/
            StartCoroutine(RaceCountdown());
            

        }

        




    }

    IEnumerator RaceUI()
    {
        yield return new WaitForSeconds(2);
        raceUI.SetActive(true);
    }

    /*IEnumerator RaceReady()
    {
        yield return new WaitForSeconds(2);
        raceUI.SetActive(true);
        StartCoroutine(RaceCountdown());
        
        //yield return new WaitForSeconds(1);
        //raceTime = true;
        //raceUI.SetActive(false);
    }*/

    IEnumerator RaceCountdown()
    {
        yield return new WaitForSeconds(2);
        raceUI.SetActive(true);

        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownDisplay.text = "GO!";

        yield return new WaitForSeconds(1f);

        raceTime = true;
        raceUI.SetActive(false);
        Timer.BeginTimer();

    }

    public void Collided()
    {
        CameraShake.ShakeTrigger();
        health--;
        healthIcon[health].SetActive(false);
        crash.Play();

        if (health <= 0)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        gameOver = true;
        Timer.EndTimer();
        gameOverUI.SetActive(true);
        
        finalTime.text = Timer.timeCounter.text;
        finalTime.enabled = true;
        Timer.timeCounter.enabled = false;
        lose.Play();

        //stop timer
        //audio

    }

    

    
}
